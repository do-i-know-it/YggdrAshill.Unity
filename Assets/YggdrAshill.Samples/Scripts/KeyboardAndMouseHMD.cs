using YggdrAshill.Nuadha;
using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Unity;
using YggdrAshill.Unity;
using System;
using UnityEngine;

namespace YggdrAshill.Samples
{
    [DisallowMultipleComponent]
    internal sealed class KeyboardAndMouseHMD : MonoBehaviour
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

        private ITransmission<IHeadMountedDisplaySoftware> transmission;

        private IDisposable disposable;

        private void OnEnable()
        {
            transmission
                = HeadMountedDisplay
                .Transmit(SimulatedHeadMountedDisplay.Transform(OriginTransform, HeadTransform, LeftHandTransform, RightHandTransform));

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
