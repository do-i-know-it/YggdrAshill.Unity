using YggdrAshill.Nuadha.Units;
using System;
using UnityEngine;

namespace YggdrAshill.Nuadha.Unity
{
    public sealed class SimulatedHumanPoseTracker :
        IHumanPoseTrackerConfiguration
    {
        public static IHumanPoseTrackerConfiguration FixedPose(Pose origin, Pose head, Pose leftHand, Pose rightHand)
        {
            return new SimulatedHumanPoseTracker(origin, head, leftHand, rightHand);
        }
        private SimulatedHumanPoseTracker(Pose origin, Pose head, Pose leftHand, Pose rightHand)
        {
            Origin = SimulatedPoseTracker.FixedPose(origin);

            Head = SimulatedPoseTracker.FixedPose(head);

            LeftHand = SimulatedPoseTracker.FixedPose(leftHand);

            RightHand = SimulatedPoseTracker.FixedPose(rightHand);
        }

        public static IHumanPoseTrackerConfiguration FixedPose()
        {
            return new SimulatedHumanPoseTracker();
        }
        private SimulatedHumanPoseTracker()
        {
            Origin = SimulatedPoseTracker.FixedPose();

            Head = SimulatedPoseTracker.FixedPose();

            LeftHand = SimulatedPoseTracker.FixedPose();

            RightHand = SimulatedPoseTracker.FixedPose();
        }

        public static IHumanPoseTrackerConfiguration Transform(Transform origin, Transform head, Transform leftHand, Transform rightHand)
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

            return new SimulatedHumanPoseTracker(origin, head, leftHand, rightHand);
        }
        private SimulatedHumanPoseTracker(Transform origin, Transform head, Transform leftHand, Transform rightHand)
        {
            Origin = SimulatedPoseTracker.Transform(origin);

            Head = SimulatedPoseTracker.Transform(origin, head);

            LeftHand = SimulatedPoseTracker.Transform(origin, leftHand);

            RightHand = SimulatedPoseTracker.Transform(origin, rightHand);
        }

        public IPoseTrackerConfiguration Origin { get; }

        public IPoseTrackerConfiguration Head { get; }

        public IPoseTrackerConfiguration LeftHand { get; }

        public IPoseTrackerConfiguration RightHand { get; }
    }
}
