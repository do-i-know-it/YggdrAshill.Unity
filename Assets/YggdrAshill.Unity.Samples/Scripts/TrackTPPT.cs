using YggdrAshill.Nuadha;
using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Unity;
using System;
using UnityEngine;

namespace YggdrAshill.Unity.Samples
{
    [DisallowMultipleComponent]
    internal sealed class TrackTPPT : MonoBehaviour
    {
        [SerializeField] private Transform headTransform;
        private Transform HeadTransform
        {
            get
            {
                if (headTransform is null)
                {
                    headTransform = transform;
                }

                return headTransform;
            }
        }

        [SerializeField] private Transform leftHandTransform;
        private Transform LeftHandTransform
        {
            get
            {
                if (leftHandTransform is null)
                {
                    leftHandTransform = transform;
                }

                return leftHandTransform;
            }
        }

        [SerializeField] private Transform rightTransform;
        private Transform RightHandTransform
        {
            get
            {
                if (rightTransform is null)
                {
                    rightTransform = transform;
                }

                return rightTransform;
            }
        }

        private IDisposable disposable;

        private void OnEnable()
        {
            var module
                = DeviceManagement.ThreePointPoseTracker.Hardware;

            disposable
                = CancellationSource.Default
                .Synthesize(module.Head.Position.Produce(signal => Debug.Log($"head position: {signal}")))
                .Synthesize(module.LeftHand.Position.Produce(signal => Debug.Log($"left hand position: {signal}")))
                .Synthesize(module.RightHand.Position.Produce(signal => Debug.Log($"right hand position: {signal}")))
                .Synthesize(module.Head.Position.Produce(ConsumeSpace3D.AbsolutePosition(HeadTransform)))
                .Synthesize(module.Head.Rotation.Produce(ConsumeSpace3D.AbsoluteRotation(HeadTransform)))
                .Synthesize(module.LeftHand.Position.Produce(ConsumeSpace3D.AbsolutePosition(LeftHandTransform)))
                .Synthesize(module.LeftHand.Rotation.Produce(ConsumeSpace3D.AbsoluteRotation(LeftHandTransform)))
                .Synthesize(module.RightHand.Position.Produce(ConsumeSpace3D.AbsolutePosition(RightHandTransform)))
                .Synthesize(module.RightHand.Rotation.Produce(ConsumeSpace3D.AbsoluteRotation(RightHandTransform)))
                .Build()
                .ToDisposable();
        }

        private void OnDisable()
        {
            disposable.Dispose();

            disposable = null;
        }
    }
}
