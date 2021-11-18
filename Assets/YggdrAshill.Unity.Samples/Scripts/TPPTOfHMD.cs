using YggdrAshill.Nuadha;
using YggdrAshill.Nuadha.Signals;
using YggdrAshill.Nuadha.Unity;
using UnityEngine;

namespace YggdrAshill.Unity.Samples
{
    [DisallowMultipleComponent]
    internal sealed class TPPTOfHMD : MonoBehaviour
    {
        [SerializeField] private Pose headAdjustment;
        [SerializeField] private Pose leftHandAdjustment;
        [SerializeField] private Pose rightHandAdjustment;

        private IPoseTrackerConfiguration headCalibration;
        private IPoseTrackerConfiguration HeadCalibration
        {
            get
            {
                if (headCalibration is null)
                {
                    headCalibration = new PoseTrackerConfiguration(headAdjustment);
                }

                return headCalibration;
            }
        }

        private IPoseTrackerConfiguration leftHandCalibration;
        private IPoseTrackerConfiguration LeftHandCalibration
        {
            get
            {
                if (leftHandCalibration is null)
                {
                    leftHandCalibration = new PoseTrackerConfiguration(leftHandAdjustment);
                }

                return leftHandCalibration;
            }
        }

        private IPoseTrackerConfiguration rightHandCalibration;
        private IPoseTrackerConfiguration RightHandCalibration
        {
            get
            {
                if (rightHandCalibration is null)
                {
                    rightHandCalibration = new PoseTrackerConfiguration(rightHandAdjustment);
                }

                return rightHandCalibration;
            }
        }

        private CompositeCancellation cancellation;

        private void OnEnable()
        {
            cancellation = new CompositeCancellation();

            var display = HeadMountedDisplay.Instance;
            var tracker = ThreePointPoseTracker.Instance;

            display
                .Head
                .Hardware
                .Pose
                .Calibrate(HeadCalibration)
                .Connect(tracker.Head.Software)
                .Synthesize(cancellation);

            display
                .LeftHand
                .Hardware
                .Pose
                .Calibrate(LeftHandCalibration)
                .Connect(tracker.LeftHand.Software)
                .Synthesize(cancellation);

            display
                .RightHand
                .Hardware
                .Pose
                .Calibrate(RightHandCalibration)
                .Connect(tracker.RightHand.Software)
                .Synthesize(cancellation);
        }

        private void OnDisable()
        {
            cancellation.Cancel();

            cancellation = null;
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
