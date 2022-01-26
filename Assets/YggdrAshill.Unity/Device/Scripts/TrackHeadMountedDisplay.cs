using YggdrAshill.VContainer;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace YggdrAshill.Unity
{
    [DisallowMultipleComponent]
    public sealed class TrackHeadMountedDisplay : LifetimeScope
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

                if (leftHandTransform == HeadTransform)
                {
                    throw new InvalidOperationException($"{nameof(LeftHandTransform)} is same as {nameof(HeadTransform)}.");
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

                if (rightTransform == HeadTransform)
                {
                    throw new InvalidOperationException($"{nameof(RightHandTransform)} is same as {nameof(HeadTransform)}.");
                }

                return rightTransform;
            }
        }

#pragma warning restore IDE0044

        protected override void Configure(IContainerBuilder builder)
        {
            builder
                .RegisterInstance(DeviceManagement.HeadMountedDisplay.Hardware)
                .AsSelf();

            builder.RegisterEntryPoint<TrackHeadMountedDisplayEntryPoint>()
                .WithParameter("origin", OriginTransform)
                .WithParameter("head", HeadTransform)
                .WithParameter("leftHand", LeftHandTransform)
                .WithParameter("rightHand", RightHandTransform);
        }
    }
}
