using YggdrAshill.Nuadha;
using YggdrAshill.Nuadha.Unity;
using System;
using UnityEngine;

namespace YggdrAshill.Unity.Samples
{
    [DisallowMultipleComponent]
    internal sealed class ThreePointPoseOfHMD : MonoBehaviour
    {
        [SerializeField] private Pose origin;
        [SerializeField] private Pose head;
        [SerializeField] private Pose leftHand;
        [SerializeField] private Pose rightHand;

        private IDisposable disposable;

        private void OnEnable()
        {
            disposable
                = DeviceManagement.HeadMountedDisplay.Hardware
                .Calibrate(SimulatedThreePointPoseTracker.FixedPose(origin, head, leftHand, rightHand))
                .Connect()
                .Connect(DeviceManagement.ThreePointPoseTracker.Software)
                .ToDisposable();
        }

        private void OnDisable()
        {
            disposable.Dispose();

            disposable = null;
        }
    }
}
