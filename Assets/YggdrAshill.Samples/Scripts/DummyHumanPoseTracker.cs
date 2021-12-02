using YggdrAshill.Nuadha.Unity;
using YggdrAshill.Unity;
using YggdrAshill.VContainer;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace YggdrAshill.Samples
{
    [DisallowMultipleComponent]
    internal sealed class DummyHumanPoseTracker : LifetimeScope
    {
        [SerializeField] private Transform originTransform;
        private Transform OriginTransform
        {
            get
            {
                if (originTransform is null)
                {
                    originTransform = transform;
                }

                return originTransform;
            }
        }

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

        protected override void Configure(IContainerBuilder builder)
        {
            builder
                .RegisterInstance(SimulatedHumanPoseTracker.Transform(OriginTransform, HeadTransform, LeftHandTransform, RightHandTransform))
                .AsSelf();
            builder
                .RegisterInstance(DeviceManagement.HumanPoseTracker.Software)
                .AsSelf();

            builder.RegisterEntryPoint<TransmitHumanPoseTrackerEntryPoint>();
        }
    }
}
