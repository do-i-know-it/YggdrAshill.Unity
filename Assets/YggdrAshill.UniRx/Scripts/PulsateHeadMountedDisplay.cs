using YggdrAshill.Unity;
using YggdrAshill.Nuadha;
using YggdrAshill.Nuadha.Unity;
using System;
using UniRx;
using UnityEngine;

namespace YggdrAshill.UniRx
{
    [DisallowMultipleComponent]
    internal sealed class PulsateHeadMountedDisplay : MonoBehaviour
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

        private void Awake()
        {
            DeviceManagement.HeadMountedDisplay.Hardware
                .Pulsate(Configuration.Threshold)
                .Connect()
                .Connect(DeviceManagement.PulsatedHeadMountedDisplay.Software)
                .ToDisposable()
                .AddTo(this);
        }
    }
}
