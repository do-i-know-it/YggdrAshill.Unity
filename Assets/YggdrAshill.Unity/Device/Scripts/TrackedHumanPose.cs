using YggdrAshill.Nuadha.Unity;
using YggdrAshill.VContainer;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace YggdrAshill.Unity
{
    [DisallowMultipleComponent]
    internal sealed class TrackedHumanPose : LifetimeScope
    {
#pragma warning disable IDE0044

        [SerializeField] private Transform originTransform;
        private Transform OriginTransform
        {
            get
            {
                if (originTransform == null)
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
                if (headTransform == null)
                {
                    throw new InvalidOperationException($"{nameof(HeadTransform)} is null.");
                }

                if (headTransform == OriginTransform)
                {
                    throw new InvalidOperationException($"{nameof(HeadTransform)} is same as {nameof(OriginTransform)}.");
                }

                return headTransform;
            }
        }

        [SerializeField] private Transform leftHandTransform;
        private Transform LeftHandTransform
        {
            get
            {
                if (leftHandTransform == null)
                {
                    throw new InvalidOperationException($"{nameof(LeftHandTransform)} is null.");
                }

                if (leftHandTransform == OriginTransform)
                {
                    throw new InvalidOperationException($"{nameof(LeftHandTransform)} is same as {nameof(OriginTransform)}.");
                }

                return leftHandTransform;
            }
        }

        [SerializeField] private Transform rightTransform;
        private Transform RightHandTransform
        {
            get
            {
                if (rightTransform == null)
                {
                    throw new InvalidOperationException($"{nameof(RightHandTransform)} is null.");
                }

                if (rightTransform == OriginTransform)
                {
                    throw new InvalidOperationException($"{nameof(RightHandTransform)} is same as {nameof(OriginTransform)}.");
                }

                return rightTransform;
            }
        }

#pragma warning restore IDE0044

        protected override void Configure(IContainerBuilder builder)
        {
            builder
                .RegisterInstance(DeviceManagement.HumanPoseTracker.Hardware)
                .AsSelf();
            builder
                .RegisterInstance(SimulateHumanPoseTracker.ToTrack(OriginTransform, HeadTransform, LeftHandTransform, RightHandTransform))
                .AsSelf();

            builder.RegisterEntryPoint<TrackHumanPoseEntryPoint>();
        }
    }
}
