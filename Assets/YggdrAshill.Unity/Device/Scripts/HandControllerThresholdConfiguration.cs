using YggdrAshill.Nuadha;
using System;
using UnityEngine;

namespace YggdrAshill.Unity
{
    [CreateAssetMenu(menuName = "YggdrAshill/HandControllerThreshold")]
    public sealed class HandControllerThresholdConfiguration : ScriptableObject
    {
#pragma warning disable IDE0044

        [SerializeField] private HysteresisThresholdConfiguration thumb;

        [SerializeField] private HysteresisThresholdConfiguration indexFinger;

        [SerializeField] private HysteresisThresholdConfiguration handGrip;

        private HandControllerThreshold threshold;
        public HandControllerThreshold Threshold
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

                    threshold = new HandControllerThreshold(new TiltThreshold(thumb.Threshold), indexFinger.Threshold, handGrip.Threshold);
                }

                return threshold;
            }
        }

#pragma warning restore IDE0044
    }
}
