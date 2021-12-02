using YggdrAshill.Nuadha.Units;
using System;
using UnityEngine;

namespace YggdrAshill.Nuadha.Unity
{
    public static class ToTrack
    {
        public static IHumanPoseTrackerSoftware HumanPose(Transform origin, Transform head, Transform leftHand, Transform rightHand)
        {
            if (origin == null)
            {
                throw new ArgumentNullException(nameof(origin));
            }
            if (head == null)
            {
                throw new ArgumentNullException(nameof(head));
            }
            if (leftHand == null)
            {
                throw new ArgumentNullException(nameof(leftHand));
            }
            if (rightHand == null)
            {
                throw new ArgumentNullException(nameof(rightHand));
            }

            return new HumanPoseTrackerSoftware(origin, head, leftHand, rightHand);
        }
        private sealed class HumanPoseTrackerSoftware :
            IHumanPoseTrackerSoftware
        {
            internal HumanPoseTrackerSoftware(Transform origin, Transform head, Transform leftHand, Transform rightHand)
            {
                Origin = ToTrackPose.Absolute(origin);

                Head = ToTrackPose.Relative(origin, head);

                LeftHand = ToTrackPose.Relative(origin, leftHand);

                RightHand = ToTrackPose.Relative(origin, rightHand);
            }

            public IPoseTrackerSoftware Origin { get; }

            public IPoseTrackerSoftware Head { get; }

            public IPoseTrackerSoftware LeftHand { get; }

            public IPoseTrackerSoftware RightHand { get; }
        }
    }
}
