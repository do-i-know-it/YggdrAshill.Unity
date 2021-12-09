using YggdrAshill.Nuadha.Units;
using System;
using UnityEngine;

namespace YggdrAshill.Nuadha.Unity
{
    public static class SimulateHumanPoseTracker
    {
        public static IHumanPoseTrackerConfiguration ToConfigure(Pose origin, Pose head, Pose leftHand, Pose rightHand)
        {
            return new HumanPoseTrackerConfiguration()
            {
                Origin = SimulatePoseTracker.ToConfigure(origin),
                Head = SimulatePoseTracker.ToConfigure(head),
                LeftHand = SimulatePoseTracker.ToConfigure(leftHand),
                RightHand = SimulatePoseTracker.ToConfigure(rightHand),
            };
        }

        public static IHumanPoseTrackerConfiguration ToConfigure(Transform origin, Transform head, Transform leftHand, Transform rightHand)
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

            return new HumanPoseTrackerConfiguration()
            {
                Origin = SimulatePoseTracker.ToConfigure(origin),
                Head = SimulatePoseTracker.ToConfigure(origin, head),
                LeftHand = SimulatePoseTracker.ToConfigure(origin, leftHand),
                RightHand = SimulatePoseTracker.ToConfigure(origin, rightHand),
            };
        }

        private sealed class HumanPoseTrackerConfiguration :
            IHumanPoseTrackerConfiguration
        {
            public IPoseTrackerConfiguration Origin { get; set; }

            public IPoseTrackerConfiguration Head { get; set; }

            public IPoseTrackerConfiguration LeftHand { get; set; }

            public IPoseTrackerConfiguration RightHand { get; set; }
        }

        public static IHumanPoseTrackerSoftware ToTrack(Transform origin, Transform head, Transform leftHand, Transform rightHand)
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

            return new HumanPoseTrackerSoftware()
            {
                Origin = SimulatePoseTracker.ToTrack(origin),
                Head = SimulatePoseTracker.ToTrack(origin, head),
                LeftHand = SimulatePoseTracker.ToTrack(origin, leftHand),
                RightHand = SimulatePoseTracker.ToTrack(origin, rightHand),
            };
        }
        private sealed class HumanPoseTrackerSoftware :
            IHumanPoseTrackerSoftware
        {
            public IPoseTrackerSoftware Origin { get; set; }

            public IPoseTrackerSoftware Head { get; set; }

            public IPoseTrackerSoftware LeftHand { get; set; }

            public IPoseTrackerSoftware RightHand { get; set; }
        }
    }
}
