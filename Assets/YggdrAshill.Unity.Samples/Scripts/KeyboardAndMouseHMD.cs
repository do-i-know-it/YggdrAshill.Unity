using YggdrAshill.Nuadha;
using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Units;
using YggdrAshill.Nuadha.Unity;
using UnityEngine;

namespace YggdrAshill.Unity.Samples
{
    [DisallowMultipleComponent]
    internal sealed class KeyboardAndMouseHMD : MonoBehaviour
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

        private IIgnition<IHeadTrackerSoftware> head;
        private IIgnition<IHeadTrackerSoftware> Head
        {
            get
            {
                if (head is null)
                {
                    head = HeadTracker.Ignite(SimulatedHeadTracker.Transform(HeadTransform));
                }

                return head;
            }
        }

        private IIgnition<IHandControllerSoftware> leftHand;
        private IIgnition<IHandControllerSoftware> LeftHand
        {
            get
            {
                if (leftHand is null)
                {
                    leftHand = HandController.Ignite(SimulatedHandController.Left(LeftHandTransform));
                }

                return leftHand;
            }
        }

        private IIgnition<IHandControllerSoftware> rightHand;
        private IIgnition<IHandControllerSoftware> RightHand
        {
            get
            {
                if (rightHand is null)
                {
                    rightHand = HandController.Ignite(SimulatedHandController.Right(RightHandTransform));
                }

                return rightHand;
            }
        }

        private CompositeCancellation cancellation;

        private void OnEnable()
        {
            cancellation = new CompositeCancellation();

            var display = HeadMountedDisplay.Instance;

            Head.Connect(display.Head.Software).Synthesize(cancellation);

            LeftHand.Connect(display.LeftHand.Software).Synthesize(cancellation);

            RightHand.Connect(display.RightHand.Software).Synthesize(cancellation);
        }

        private void OnDisable()
        {
            cancellation.Cancel();

            cancellation = null;
        }

        private void Update()
        {
            Head.Emit();

            LeftHand.Emit();

            RightHand.Emit();
        }
    }
}
