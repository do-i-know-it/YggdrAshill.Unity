using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using YggdrAshill.Unity;

namespace YggdrAshill.Samples
{
    internal sealed class HandControllerInteraction : LifetimeScope
    {
        [SerializeField] private LayerMask layerMask = ~0;

        [SerializeField] private LineRenderer lineRenderer;

        [SerializeField] private Handedness handedness;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            switch (handedness)
            {
                case Handedness.Left:
                    builder.RegisterInstance(DeviceManagement.PulsatedHeadMountedDisplay.Hardware.LeftHand)
                        .AsSelf();
                    builder.RegisterInstance(DeviceManagement.HeadMountedDisplay.Hardware.LeftHand.Pose)
                        .AsSelf();
                    break;
                case Handedness.Right:
                    builder.RegisterInstance(DeviceManagement.PulsatedHeadMountedDisplay.Hardware.RightHand)
                        .AsSelf();
                    builder.RegisterInstance(DeviceManagement.HeadMountedDisplay.Hardware.RightHand.Pose)
                        .AsSelf();
                    break;
                default:
                    throw new NotSupportedException(nameof(Handedness.None));
            }

            builder.RegisterInstance(layerMask).AsSelf();
            builder.RegisterInstance(lineRenderer).AsSelf();

            builder.RegisterEntryPoint<HandControllerInteractionEntryPoint>();
        }
    }
}
