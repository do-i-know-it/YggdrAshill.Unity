using YggdrAshill.Nuadha.Units;
using System;
using UnityEngine;

namespace YggdrAshill.Nuadha.Unity
{
    public sealed class SimulateHeadMountedDisplay :
        IHeadMountedDisplayConfiguration
    {
        public static IHeadMountedDisplayConfiguration WASDIJKL(
            Transform origin, Transform head, Transform leftHand, Transform rightHand,
            KeyCode leftIndexFinger, KeyCode leftHandGrip, KeyCode rightIndexFinger, KeyCode rightHandGrip)
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

            return new SimulateHeadMountedDisplay()
            {
                Origin = SimulatePoseTracker.ToConfigure(origin),

                Head = SimulateHeadTracker.ToConfigure(origin, head),

                LeftHand = SimulateHandController.WASD(origin, leftHand, leftIndexFinger, leftHandGrip),

                RightHand = SimulateHandController.IJKL(origin, rightHand, rightIndexFinger, rightHandGrip),
            };
        }

        public static IHeadMountedDisplayConfiguration WASDFCIJKLHM(Transform origin, Transform head, Transform leftHand, Transform rightHand)
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

            return new SimulateHeadMountedDisplay()
            {
                Origin = SimulatePoseTracker.ToConfigure(origin),

                Head = SimulateHeadTracker.ToConfigure(origin, head),

                LeftHand = SimulateHandController.WASDFC(origin, leftHand),

                RightHand = SimulateHandController.IJKLHM(origin, rightHand),
            };
        }

        public IPoseTrackerConfiguration Origin { get; private set; }

        public IHeadTrackerConfiguration Head { get; private set; }

        public IHandControllerConfiguration LeftHand { get; private set; }

        public IHandControllerConfiguration RightHand { get; private set; }
    }
}
