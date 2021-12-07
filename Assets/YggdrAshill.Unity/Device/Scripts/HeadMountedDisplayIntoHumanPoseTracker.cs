using YggdrAshill.VContainer;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace YggdrAshill.Unity
{
    [DisallowMultipleComponent]
    internal sealed class HeadMountedDisplayIntoHumanPoseTracker : LifetimeScope
    {
#pragma warning disable IDE0044

        [SerializeField] private HumanPoseAdjustment adjustment;
        private HumanPoseAdjustment Adjustment
        {
            get
            {
                if (adjustment == null)
                {
                    throw new InvalidOperationException($"{nameof(adjustment)} is null.");
                }

                return adjustment;
            }
        }

#pragma warning restore IDE0044

        protected override void Configure(IContainerBuilder builder)
        {
            builder
                .RegisterInstance(DeviceManagement.HeadMountedDisplay.Hardware)
                .AsSelf();
            builder
                .RegisterInstance(Adjustment.Configuration)
                .AsSelf();
            builder
                .RegisterInstance(DeviceManagement.HumanPoseTracker.Software)
                .AsSelf();

            builder.RegisterEntryPoint<HeadMountedDisplayIntoHumanPoseTrackerEntryPoint>();
        }
    }
}
