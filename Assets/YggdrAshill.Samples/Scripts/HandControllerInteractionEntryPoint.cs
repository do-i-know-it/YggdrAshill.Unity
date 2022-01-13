using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using YggdrAshill.Nuadha;
using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Units;
using YggdrAshill.Nuadha.Unity;

namespace YggdrAshill.Samples
{
    public sealed class HandControllerInteractionEntryPoint :
        IStartable,
        IDisposable,
        IPostTickable
    {
        private readonly IPoseTrackerHardware pose;

        private readonly IPulsatedHandControllerHardware handController;

        private readonly LayerMask layerMask;

        private readonly LineRenderer lineRenderer;

        private IDisposable disposable;

        [Inject]
        public HandControllerInteractionEntryPoint(IPoseTrackerHardware pose, IPulsatedHandControllerHardware handController, LayerMask layerMask, LineRenderer lineRenderer)
        {
            this.pose = pose;
            this.handController = handController;
            this.layerMask = layerMask;
            this.lineRenderer = lineRenderer;
        }

        public void Start()
        {
            disposable
                = CancellationSource.Default
                .Synthesize(pose.Position.Produce(signal => position = signal.ToVector()))
                .Synthesize(pose.Rotation.Produce(signal => rotation = signal.ToQuaternion()))
                .Synthesize(handController.IndexFinger.Pull.Detect(PulseHas.Enabled).Produce(() => isSelecting = true))
                .Synthesize(handController.IndexFinger.Pull.Detect(PulseHas.Disabled).Produce(() => isSelecting = false))
                .Synthesize(handController.HandGrip.Pull.Detect(PulseHas.Enabled).Produce(() => isGrabbing = true))
                .Synthesize(handController.HandGrip.Pull.Detect(PulseHas.Disabled).Produce(() => isGrabbing = false))
                .Build()
                .ToDisposable();
        }

        public void Dispose()
        {
            disposable.Dispose();
            disposable = null;
        }

        private Vector3 position;

        private Quaternion rotation;

        private Vector3 Forward
            => rotation * Vector3.forward;

        private Vector3 AnchorPosition
            => position + distance * Forward;

        private bool isGrabbing;

        private bool isSelecting;

        private Grabbable grabbable;

        private Clickable clickable;

        private Pose previousPose;

        private float distance;

        public void PostTick()
        {
            var found = Find(out var info);

            if (isGrabbing)
            {
                if (grabbable != null)
                {
                    // grabbing

                    var currentPose = new Pose(AnchorPosition, rotation);

                    var positionDiff = currentPose.position - previousPose.position;

                    var rotationDiff = currentPose.rotation * Quaternion.Inverse(previousPose.rotation);

                    var nextPose = new Pose(currentPose.position + positionDiff, currentPose.rotation * rotationDiff);

                    grabbable.Grab(previousPose, currentPose, nextPose);

                    previousPose = currentPose;

                    return;
                }

                if (!found)
                {
                    // couldn't try to grab

                    return;
                }

                // try to grab

                grabbable = info.transform.GetComponentInParent<Grabbable>();

                if (grabbable == null)
                {
                    // couldn't grab

                    return;
                }

                // can grab

                if (clickable != null)
                {
                    clickable = null;
                }

                previousPose = new Pose(info.point, rotation);

                distance = info.distance;

                grabbable.GrabBegin(previousPose);

                return;
            }

            // try to ungrab

            if (grabbable != null)
            {
                grabbable.GrabEnd(new Pose(AnchorPosition, rotation));

                grabbable = null;

                previousPose = Pose.identity;

                distance = 0.0f;
            }

            if (isSelecting)
            {
                if (clickable != null)
                {
                    // selecting
                    return;
                }

                if (!found)
                {
                    // couldn't select

                    return;
                }

                // can select

                clickable = info.transform.GetComponentInParent<Clickable>();
            }

            // try to click

            if (clickable != null)
            {
                clickable.Click();

                clickable = null;
            }
        }

        private bool Find(out RaycastHit hit)
        {
            var found = Physics.Raycast(position, Forward, out hit, float.MaxValue, layerMask);

            if (found)
            {
                lineRenderer.SetPosition(0, position);
                lineRenderer.SetPosition(1, hit.point);
            }
            else
            {
                lineRenderer.SetPosition(0, position);
                lineRenderer.SetPosition(1, position + Forward);
            }

            return found;
        }
    }
}
