using YggdrAshill.Nuadha;
using YggdrAshill.Nuadha.Unity;
using System;
using UnityEngine;

namespace YggdrAshill.Unity
{
    [CreateAssetMenu(menuName = "YggdrAshill/HeadMountedDisplayThreshold")]
    public sealed class HeadMountedDisplayThresholdConfiguration : ScriptableObject
    {
#pragma warning disable IDE0044

        [SerializeField] private HysteresisThresholdConfiguration thumb;

        [SerializeField] private HysteresisThresholdConfiguration indexFinger;

        [SerializeField] private HysteresisThresholdConfiguration handGrip;

        private HeadMountedDisplayThreshold threshold;
        public HeadMountedDisplayThreshold Threshold
        {
            get
            {
                if (threshold == null)
                {
                    if (thumb == null)
                    {
                        throw new InvalidOperationException($"{nameof(thumb)} is null.");
                    }
                    if (indexFinger == null)
                    {
                        throw new InvalidOperationException($"{nameof(indexFinger)} is null.");
                    }
                    if (handGrip == null)
                    {
                        throw new InvalidOperationException($"{nameof(handGrip)} is null.");
                    }

                    threshold = new HeadMountedDisplayThreshold(new HandControllerThreshold(new TiltThreshold(thumb.Threshold), indexFinger.Threshold, handGrip.Threshold));
                }

                return threshold;
            }
        }

#pragma warning restore IDE0044
    }
}
