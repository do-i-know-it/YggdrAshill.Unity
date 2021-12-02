using YggdrAshill.Nuadha.Unity;
using YggdrAshill.Unity;
using YggdrAshill.Unity.OVR;
using YggdrAshill.VContainer;
using UnityEngine;

namespace YggdrAshill.Samples
{
    internal sealed class OculusController : TransmitHeadMountedDisplayLifetimeScope
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

        protected override IHeadMountedDisplayConfiguration Configuration => Oculus.HeadMountedDisplay(OriginTransform, HeadTransform, LeftHandTransform, RightHandTransform);

        protected override IHeadMountedDisplaySoftware Software => DeviceManagement.HeadMountedDisplay.Software;
    }
}
