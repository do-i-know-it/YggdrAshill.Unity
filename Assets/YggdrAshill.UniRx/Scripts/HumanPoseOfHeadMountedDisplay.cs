using YggdrAshill.Unity;
using YggdrAshill.Nuadha;
using YggdrAshill.Nuadha.Unity;
using System;
using UniRx;
using UnityEngine;

namespace YggdrAshill.UniRx
{
    [DisallowMultipleComponent]
    internal sealed class HumanPoseOfHeadMountedDisplay : MonoBehaviour
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

        private void OnEnable()
        {
            DeviceManagement.HeadMountedDisplay.Hardware
                .Calibrate(Adjustment.Configuration)
                .Connect()
                .Connect(DeviceManagement.HumanPoseTracker.Software)
                .ToDisposable()
                .AddTo(this);
        }
    }
}
