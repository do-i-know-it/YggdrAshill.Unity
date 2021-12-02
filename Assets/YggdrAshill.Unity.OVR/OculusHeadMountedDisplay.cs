using YggdrAshill.Nuadha.Units;
using YggdrAshill.Nuadha.Unity;
using UnityEngine;

namespace YggdrAshill.Unity.OVR
{
    internal sealed class OculusHeadMountedDisplay :
        IHeadMountedDisplayConfiguration
    {
        internal OculusHeadMountedDisplay(Transform origin, Transform head, Transform leftHand, Transform rightHand)
        {
            Origin = SimulatedPoseTracker.Transform(origin);

            Head = SimulatedHeadTracker.Transform(origin, head);

            LeftHand = Oculus.LeftHandController(origin, leftHand);

            RightHand = Oculus.RightHandController(origin, rightHand);
        }

        public IPoseTrackerConfiguration Origin { get; }

        public IHeadTrackerConfiguration Head { get; }

        public IHandControllerConfiguration LeftHand { get; }

        public IHandControllerConfiguration RightHand { get; }
    }
}
