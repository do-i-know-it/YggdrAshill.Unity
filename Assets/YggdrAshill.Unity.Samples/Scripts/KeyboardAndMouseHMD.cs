using YggdrAshill.Nuadha;
using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Units;
using YggdrAshill.Nuadha.Unity;
using UnityEngine;
using System;

namespace YggdrAshill.Unity.Samples
{
    [DisallowMultipleComponent]
    internal sealed class KeyboardAndMouseHMD : MonoBehaviour,
        IHeadMountedDisplayConfiguration
    {
        [SerializeField] private Transform originTransform;
        private Transform OriginTransform
        {
            get
            {
                if (originTransform is null)
                {
                    originTransform = transform;
                }

                return originTransform;
            }
        }

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

        private IPoseTrackerConfiguration origin;
        public IPoseTrackerConfiguration Origin
        {
            get
            {
                if (origin is null)
                {
                    origin = SimulatedPoseTracker.Transform(OriginTransform);
                }

                return origin;
            }
        }

        private IHeadTrackerConfiguration head;
        public IHeadTrackerConfiguration Head
        {
            get
            {
                if (head is null)
                {
                    head = SimulatedHeadTracker.Transform(HeadTransform);
                }

                return head;
            }
        }

        private IHandControllerConfiguration leftHand;
        public IHandControllerConfiguration LeftHand
        {
            get
            {
                if (leftHand is null)
                {
                    leftHand = SimulatedHandController.Left(LeftHandTransform);
                }

                return leftHand;
            }
        }

        private IHandControllerConfiguration rightHand;
        public IHandControllerConfiguration RightHand
        {
            get
            {
                if (rightHand is null)
                {
                    rightHand = SimulatedHandController.Right(RightHandTransform);
                }

                return rightHand;
            }
        }

        private ITransmission<IHeadMountedDisplaySoftware> transmission;

        private IDisposable disposable;

        private void OnEnable()
        {
            transmission
                = HeadMountedDisplay
                .Transmit(this);

            disposable 
                = transmission
                .Connect(DeviceManagement.HeadMountedDisplay.Software)
                .ToDisposable();
        }

        private void OnDisable()
        {
            disposable.Dispose();

            transmission = null;

            disposable = null;
        }

        private void Update()
        {
            transmission.Emit();
        }
    }
}
