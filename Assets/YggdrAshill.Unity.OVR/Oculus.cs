using YggdrAshill.Nuadha.Units;
using YggdrAshill.Nuadha.Unity;
using System;
using UnityEngine;

namespace YggdrAshill.Unity.OVR
{
    public static class Oculus
    {
        public static IStickConfiguration LeftThumbStick { get; } = new OculusStick(OVRInput.RawTouch.LThumbstick, OVRInput.RawAxis2D.LThumbstick);

        public static IStickConfiguration RightThumbStick { get; } = new OculusStick(OVRInput.RawTouch.LThumbstick, OVRInput.RawAxis2D.RThumbstick);

        public static ITriggerConfiguration LeftIndexFingerTrigger { get; } = new OculusTrigger(OVRInput.RawTouch.LIndexTrigger, OVRInput.RawAxis1D.LIndexTrigger);

        public static ITriggerConfiguration RightIndexFingerTrigger { get; } = new OculusTrigger(OVRInput.RawTouch.RIndexTrigger, OVRInput.RawAxis1D.RIndexTrigger);

        public static ITriggerConfiguration LeftHandTrigger { get; } = new OculusTrigger(OVRInput.RawAxis1D.LHandTrigger);

        public static ITriggerConfiguration RightHandTrigger { get; } = new OculusTrigger(OVRInput.RawAxis1D.RHandTrigger);

        public static IHandControllerConfiguration LeftHandController(Transform origin, Transform transform)
        {
            if (origin == null)
            {
                throw new ArgumentNullException(nameof(origin));
            }
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return new OculusController(origin, transform, true);
        }

        public static IHandControllerConfiguration RightHandController(Transform origin, Transform transform)
        {
            if (origin == null)
            {
                throw new ArgumentNullException(nameof(origin));
            }
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return new OculusController(origin, transform, false);
        }

        public static IHeadMountedDisplayConfiguration HeadMountedDisplay(Transform origin, Transform head, Transform leftHand, Transform rightHand)
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

            return new OculusHeadMountedDisplay(origin, head, leftHand, rightHand);
        }
    }
}
