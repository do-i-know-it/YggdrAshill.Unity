using YggdrAshill.Nuadha;
using UnityEngine;
using System;

namespace YggdrAshill.UniRx
{
    [CreateAssetMenu(menuName = "YggdrAshill/TiltThreshold")]
    public sealed class TiltThresholdConfiguration : ScriptableObject
    {
#pragma warning disable IDE0044

        [SerializeField] private HysteresisThresholdConfiguration configuration;


        private TiltThreshold threshold;
        public TiltThreshold Threshold
        {
            get
            {
                if (threshold == null)
                {
                    if (configuration == null)
                    {
                        throw new InvalidOperationException($"{nameof(configuration)} is null.");
                    }

                    threshold = new TiltThreshold(configuration.Threshold);
                }

                return threshold;
            }
        }

#pragma warning restore IDE0044
    }
}
