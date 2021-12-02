using System;

namespace YggdrAshill.Nuadha.Unity
{
    public sealed class HeadMountedDisplayThreshold
    {
        public HandControllerThreshold LeftHand { get; }

        public HandControllerThreshold RightHand { get; }

        public HeadMountedDisplayThreshold(HandControllerThreshold leftHand, HandControllerThreshold rightHand)
        {
            if (leftHand == null)
            {
                throw new ArgumentNullException(nameof(leftHand));
            }
            if (rightHand == null)
            {
                throw new ArgumentNullException(nameof(rightHand));
            }

            LeftHand = leftHand;

            RightHand = rightHand;
        }

        public HeadMountedDisplayThreshold(HandControllerThreshold threshold)
        {
            if (threshold == null)
            {
                throw new ArgumentNullException(nameof(threshold));
            }

            LeftHand = threshold;

            RightHand = threshold;
        }
    }
}
