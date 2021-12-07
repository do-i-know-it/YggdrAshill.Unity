using YggdrAshill.VContainer;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace YggdrAshill.Unity
{
    [DisallowMultipleComponent]
    internal sealed class PulsateHeadMountedDisplay : LifetimeScope
    {
#pragma warning disable IDE0044

        [SerializeField] private HeadMountedDisplayThresholdConfiguration configuration;
        private HeadMountedDisplayThresholdConfiguration Configuration
        {
            get
            {
                if (configuration == null)
                {
                    throw new InvalidOperationException($"{nameof(configuration)} is null.");
                }

                return configuration;
            }
        }

#pragma warning restore IDE0044

        protected override void Configure(IContainerBuilder builder)
        {
            builder
                .RegisterInstance(DeviceManagement.HeadMountedDisplay.Hardware)
                .AsSelf();
            builder
                .RegisterInstance(Configuration.Threshold)
                .AsSelf();
            builder
                .RegisterInstance(DeviceManagement.PulsatedHeadMountedDisplay.Software)
                .AsSelf();

            builder.RegisterEntryPoint<PulsateHeadMountedDisplayEntryPoint>();
        }
    }
}
