using YggdrAshill.Nuadha.Units;
using System;
using UnityEngine;

namespace YggdrAshill.Nuadha.Unity
{
    public sealed class SimulatedHeadMountedDisplay :
        IHeadMountedDisplayConfiguration
    {
        public static IHeadMountedDisplayConfiguration Transform(Transform origin, Transform head, Transform leftHand, Transform rightHand)
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

            return new SimulatedHeadMountedDisplay(origin, head, leftHand, rightHand);
        }
        private SimulatedHeadMountedDisplay(Transform origin, Transform head, Transform leftHand, Transform rightHand)
        {
            Origin = SimulatedPoseTracker.Transform(origin);

            Head = SimulatedHeadTracker.Transform(origin, head);

            LeftHand = SimulatedHandController.Left(origin, leftHand);

            RightHand = SimulatedHandController.Right(origin, rightHand);
        }

        public IPoseTrackerConfiguration Origin { get; }

        public IHeadTrackerConfiguration Head { get; }

        public IHandControllerConfiguration LeftHand { get; }

        public IHandControllerConfiguration RightHand { get; }
    }
}
