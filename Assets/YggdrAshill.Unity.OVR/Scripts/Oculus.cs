using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Signals;
using YggdrAshill.Nuadha.Units;
using YggdrAshill.Nuadha.Unity;
using System;
using UnityEngine;
using Touch = YggdrAshill.Nuadha.Signals.Touch;

namespace YggdrAshill.Unity.OVR
{
    public static class Oculus
    {
        #region Stick

        public static IStickConfiguration LeftThumbStick { get; } = new StickConfiguration(OVRInput.RawTouch.LThumbstick, OVRInput.RawAxis2D.LThumbstick);

        public static IStickConfiguration RightThumbStick { get; } = new StickConfiguration(OVRInput.RawTouch.LThumbstick, OVRInput.RawAxis2D.RThumbstick);

        private sealed class StickConfiguration : IStickConfiguration
        {
            internal StickConfiguration(OVRInput.RawTouch touch, OVRInput.RawAxis2D tilt)
            {
                Touch = Generate.Touch(() => OVRInput.Get(touch));

                Tilt = Generate.Tilt(() => OVRInput.Get(tilt));
            }

            public IGeneration<Touch> Touch { get; }

            public IGeneration<Tilt> Tilt { get; }
        }

        #endregion

        #region Trigger

        public static ITriggerConfiguration LeftIndexFingerTrigger { get; } = new TriggerConfiguration(OVRInput.RawTouch.LIndexTrigger, OVRInput.RawAxis1D.LIndexTrigger);

        public static ITriggerConfiguration RightIndexFingerTrigger { get; } = new TriggerConfiguration(OVRInput.RawTouch.RIndexTrigger, OVRInput.RawAxis1D.RIndexTrigger);

        public static ITriggerConfiguration LeftHandTrigger { get; } = new TriggerConfiguration(OVRInput.RawAxis1D.LHandTrigger);

        public static ITriggerConfiguration RightHandTrigger { get; } = new TriggerConfiguration(OVRInput.RawAxis1D.RHandTrigger);

        private sealed class TriggerConfiguration :
            ITriggerConfiguration
        {
            internal TriggerConfiguration(OVRInput.RawTouch touch, OVRInput.RawAxis1D pull)
            {
                Touch = Generate.Touch(() => OVRInput.Get(touch));

                Pull = Generate.Pull(() => OVRInput.Get(pull));
            }

            internal TriggerConfiguration(OVRInput.RawAxis1D pull)
            {
                Touch = Generate.Touch(() => OVRInput.Get(pull) > 0.1f);

                Pull = Generate.Pull(() => OVRInput.Get(pull));
            }

            public IGeneration<Touch> Touch { get; }

            public IGeneration<Pull> Pull { get; }
        }

        #endregion

        #region HandController

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

            return new HandControllerConfiguration(origin, transform, true);
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

            return new HandControllerConfiguration(origin, transform, false);
        }

        private sealed class HandControllerConfiguration :
            IHandControllerConfiguration
        {
            internal HandControllerConfiguration(Transform origin, Transform transform, bool leftHand) : this(leftHand)
            {
                Pose = SimulatedPoseTracker.Transform(origin, transform);
            }

            private HandControllerConfiguration(bool leftHand)
            {
                if (leftHand)
                {
                    Thumb = LeftThumbStick;

                    IndexFinger = LeftIndexFingerTrigger;

                    HandGrip = LeftHandTrigger;
                }
                else
                {
                    Thumb = RightThumbStick;

                    IndexFinger = RightIndexFingerTrigger;

                    HandGrip = RightHandTrigger;
                }
            }

            public IPoseTrackerConfiguration Pose { get; }

            public IStickConfiguration Thumb { get; }

            public ITriggerConfiguration IndexFinger { get; }

            public ITriggerConfiguration HandGrip { get; }
        }

        #endregion

        #region HeadMountedDisplay

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

            return new HeadMountedDisplayConfiguration(origin, head, leftHand, rightHand);
        }
        private sealed class HeadMountedDisplayConfiguration :
            IHeadMountedDisplayConfiguration
        {
            internal HeadMountedDisplayConfiguration(Transform origin, Transform head, Transform leftHand, Transform rightHand)
            {
                Origin = SimulatedPoseTracker.Transform(origin);

                Head = SimulatedHeadTracker.Transform(origin, head);

                LeftHand = LeftHandController(origin, leftHand);

                RightHand = RightHandController(origin, rightHand);
            }

            public IPoseTrackerConfiguration Origin { get; }

            public IHeadTrackerConfiguration Head { get; }

            public IHandControllerConfiguration LeftHand { get; }

            public IHandControllerConfiguration RightHand { get; }
        }

        #endregion
    }
}
