using YggdrAshill.Nuadha;
using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Signals;
using YggdrAshill.Nuadha.Units;
using YggdrAshill.Nuadha.Unity;
using System;
using UnityEngine;

namespace YggdrAshill.Unity.Samples
{
    [DisallowMultipleComponent]
    internal sealed class TPPTOfHMD : MonoBehaviour,
        IThreePointPoseTrackerConfiguration
    {
        [SerializeField] private Pose originAdjustment;
        [SerializeField] private Pose headAdjustment;
        [SerializeField] private Pose leftHandAdjustment;
        [SerializeField] private Pose rightHandAdjustment;

        private IPoseTrackerConfiguration origin;
        public IPoseTrackerConfiguration Origin
        {
            get
            {
                if (origin is null)
                {
                    origin = new PoseTrackerConfiguration(originAdjustment);
                }

                return origin;
            }
        }

        private IPoseTrackerConfiguration head;
        public IPoseTrackerConfiguration Head
        {
            get
            {
                if (head is null)
                {
                    head = new PoseTrackerConfiguration(headAdjustment);
                }

                return head;
            }
        }

        private IPoseTrackerConfiguration leftHand;
        public IPoseTrackerConfiguration LeftHand
        {
            get
            {
                if (leftHand is null)
                {
                    leftHand = new PoseTrackerConfiguration(leftHandAdjustment);
                }

                return leftHand;
            }
        }

        private IPoseTrackerConfiguration rightHand;
        public IPoseTrackerConfiguration RightHand
        {
            get
            {
                if (rightHand is null)
                {
                    rightHand = new PoseTrackerConfiguration(rightHandAdjustment);
                }

                return rightHand;
            }
        }

        private IDisposable disposable;

        private void OnEnable()
        {
            disposable
                = DeviceManagement.HeadMountedDisplay.Hardware
                .Calibrate(this)
                .Connect()
                .Connect(DeviceManagement.ThreePointPoseTracker.Software)
                .ToDisposable();
        }

        private void OnDisable()
        {
            disposable.Dispose();

            disposable = null;
        }

        private sealed class PoseTrackerConfiguration :
            IPoseTrackerConfiguration
        {
            internal PoseTrackerConfiguration(Pose pose)
            {

                Position = GenerateSpace3D.Position(pose.position);

                Rotation = GenerateSpace3D.Rotation(pose.rotation);
            }

            public IGeneration<Space3D.Position> Position { get; }

            public IGeneration<Space3D.Rotation> Rotation { get; }
        }
    }
}
