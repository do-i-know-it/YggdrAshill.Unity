using YggdrAshill.Nuadha.Unity;
using YggdrAshill.VContainer;
using System;
using UnityEngine;

namespace YggdrAshill.Unity
{
    [DisallowMultipleComponent]
    internal sealed class HeadMountedDisplayIntoHumanPoseTracker : HeadMountedDisplayIntoHumanPoseTrackerLifetimeScope
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

        protected override IHeadMountedDisplayHardware Hardware => DeviceManagement.HeadMountedDisplay.Hardware;

        protected override IHumanPoseTrackerConfiguration Configuration => Adjustment.Configuration;

        protected override IHumanPoseTrackerSoftware Software => DeviceManagement.HumanPoseTracker.Software;
    }
}
