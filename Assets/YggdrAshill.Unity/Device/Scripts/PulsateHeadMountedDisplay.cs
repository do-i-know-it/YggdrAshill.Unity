using YggdrAshill.Nuadha.Unity;
using YggdrAshill.VContainer;
using System;
using UnityEngine;

namespace YggdrAshill.Unity
{
    internal sealed class PulsateHeadMountedDisplay : PulsateHeadMountedDisplayLifetimeScope
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

        protected override IHeadMountedDisplayHardware Hardware => DeviceManagement.HeadMountedDisplay.Hardware;

        protected override HeadMountedDisplayThreshold Threshold => Configuration.Threshold;

        protected override IPulsatedHeadMountedDisplaySoftware Software => DeviceManagement.PulsatedHeadMountedDisplay.Software;
    }
}
