using YggdrAshill.Nuadha;
using YggdrAshill.Nuadha.Unity;
using UnityEngine;

namespace YggdrAshill.Unity.Samples
{
    [DisallowMultipleComponent]
    internal sealed class TrackTPPT : MonoBehaviour
    {
        [SerializeField] private Transform headTransform;
        private Transform HeadTransform
        {
            get
            {
                if (headTransform is null)
                {
                    headTransform = transform;
                }

                return headTransform;
            }
        }

        [SerializeField] private Transform leftHandTransform;
        private Transform LeftHandTransform
        {
            get
            {
                if (leftHandTransform is null)
                {
                    leftHandTransform = transform;
                }

                return leftHandTransform;
            }
        }

        [SerializeField] private Transform rightTransform;
        private Transform RightHandTransform
        {
            get
            {
                if (rightTransform is null)
                {
                    rightTransform = transform;
                }

                return rightTransform;
            }
        }

        private CompositeCancellation cancellation;

        private void OnEnable()
        {
            cancellation = new CompositeCancellation();

            ThreePointPoseTracker
                .Instance
                .Head
                .Hardware
                .Position
                .Produce(ConsumeSpace3D.AbsolutePosition(HeadTransform));
            ThreePointPoseTracker
                .Instance
                .Head
                .Hardware
                .Rotation
                .Produce(ConsumeSpace3D.AbsoluteRotation(HeadTransform));

            ThreePointPoseTracker
                .Instance
                .LeftHand
                .Hardware
                .Position
                .Produce(ConsumeSpace3D.AbsolutePosition(LeftHandTransform));
            ThreePointPoseTracker
                .Instance
                .LeftHand
                .Hardware
                .Rotation
                .Produce(ConsumeSpace3D.AbsoluteRotation(LeftHandTransform));

            ThreePointPoseTracker
                .Instance
                .RightHand
                .Hardware
                .Position
                .Produce(ConsumeSpace3D.AbsolutePosition(RightHandTransform));
            ThreePointPoseTracker
                .Instance
                .RightHand
                .Hardware
                .Rotation
                .Produce(ConsumeSpace3D.AbsoluteRotation(RightHandTransform));
        }

        private void OnDisable()
        {
            cancellation.Cancel();

            cancellation = null;
        }
    }
}
