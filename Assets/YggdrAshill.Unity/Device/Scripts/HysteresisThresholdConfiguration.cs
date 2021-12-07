using YggdrAshill.Nuadha;
using System;
using UnityEngine;

namespace YggdrAshill.Unity
{
    [Serializable]
    public sealed class HysteresisThresholdConfiguration
    {
#pragma warning disable IDE0044

        [Range(0.0f, 1.0f)]
        [SerializeField]
        private float lower = 0.5f;

        [Range(0.0f, 1.0f)]
        [SerializeField]
        private float upper = 0.5f;

#pragma warning restore IDE0044

        private HysteresisThreshold threshold;
        public HysteresisThreshold Threshold
        {
            get
            {
                if (threshold == null)
                {
                    threshold = new HysteresisThreshold(lower, upper);
                }

                return threshold;
            }
        }
    }
}
