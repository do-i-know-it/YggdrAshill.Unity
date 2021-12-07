using YggdrAshill.VContainer;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace YggdrAshill.Unity.OVR
{
    [DisallowMultipleComponent]
    internal sealed class OculusController : LifetimeScope
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
                    throw new InvalidOperationException($"{nameof(headTransform)} is null.");
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
                    throw new InvalidOperationException($"{nameof(leftHandTransform)} is null.");
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
                    throw new InvalidOperationException($"{nameof(rightTransform)} is null.");
                }

                return rightTransform;
            }
        }

#pragma warning restore IDE0044

        protected override void Configure(IContainerBuilder builder)
        {
            builder
                .RegisterInstance(Oculus.HeadMountedDisplay(OriginTransform, HeadTransform, LeftHandTransform, RightHandTransform))
                .AsSelf();
            builder
                .RegisterInstance(DeviceManagement.HeadMountedDisplay.Software)
                .AsSelf();

            builder.RegisterEntryPoint<TransmitHeadMountedDisplayEntryPoint>();
        }
    }
}
