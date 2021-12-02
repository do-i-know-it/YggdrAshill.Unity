using YggdrAshill.Nuadha.Units;
using YggdrAshill.Nuadha.Unity;
using UnityEngine;

namespace YggdrAshill.Unity.OVR
{
    internal sealed class OculusController :
        IHandControllerConfiguration
    {
        internal OculusController(Transform origin, Transform transform, bool leftHand) : this(leftHand)
        {
            Pose = SimulatedPoseTracker.Transform(origin, transform);
        }

        private OculusController(bool leftHand)
        {
            if (leftHand)
            {
                Thumb = Oculus.LeftThumbStick;

                IndexFinger = Oculus.LeftIndexFingerTrigger;

                HandGrip = Oculus.LeftHandTrigger;
            }
            else
            {
                Thumb = Oculus.RightThumbStick;

                IndexFinger = Oculus.RightIndexFingerTrigger;

                HandGrip = Oculus.RightHandTrigger;
            }
        }

        public IPoseTrackerConfiguration Pose { get; }

        public IStickConfiguration Thumb { get; }

        public ITriggerConfiguration IndexFinger { get; }

        public ITriggerConfiguration HandGrip { get; }
    }
}
