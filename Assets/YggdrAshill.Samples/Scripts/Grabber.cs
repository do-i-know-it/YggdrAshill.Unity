using System;
using UnityEngine;
using YggdrAshill.Nuadha;
using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Unity;

namespace YggdrAshill.Samples
{
    internal sealed class Grabber : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask = ~0;

        [SerializeField] private LineRenderer lineRenderer;

        private bool isGrabbing;

        private IDisposable disposable;

        private void OnEnable()
        {
            disposable
                = CancellationSource.Default
                .Synthesize(DeviceManagement.PulsatedHeadMountedDisplay.Hardware.RightHand.HandGrip.Pull.Detect(PulseHas.Enabled).Produce(() => isGrabbing = true))
                .Synthesize(DeviceManagement.PulsatedHeadMountedDisplay.Hardware.RightHand.HandGrip.Pull.Detect(PulseHas.Disabled).Produce(() => isGrabbing = false))
                .Build()
                .ToDisposable();
        }

        private void OnDisable()
        {
            disposable.Dispose();
            disposable = null;
        }

        private Grabbable grabbable;

        private void LateUpdate()
        {
            if (!Physics.Raycast(transform.position, transform.forward, out var info, float.MaxValue, layerMask))
            {
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, transform.position + transform.forward);

                return;
            }

            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, info.point);

            if (!isGrabbing)
            {
                if (grabbable != null)
                {
                    grabbable.Release(transform);
                    grabbable = null;
                }

                return;
            }

            if (grabbable != null)
            {
                // gragging?
                return;
            }

            for (var candidate = info.transform; candidate.parent != null; candidate = candidate.parent)
            {
                if (candidate.TryGetComponent(out grabbable))
                {
                    grabbable.Grab(transform);

                    return;
                }
            }
        }
    }
}
