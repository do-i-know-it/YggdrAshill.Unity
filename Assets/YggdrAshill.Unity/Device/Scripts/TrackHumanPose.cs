using YggdrAshill.Nuadha.Unity;
using YggdrAshill.VContainer;
using System;
using UnityEngine;

namespace YggdrAshill.Unity
{
    internal sealed class TrackHumanPose : TrackHumanPoseLifetimeScope
    {
#pragma warning disable IDE0044

        [SerializeField] private Transform originTransform;
        private Transform OriginTransform
        {
            get
            {
                if (originTransform == null)
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
                if (headTransform == null)
                {
                    throw new InvalidOperationException($"{nameof(headTransform)} is null.");
                }

                return headTransform;
            }
        }

        [SerializeField] private Transform leftHandTransform;
        private Transform LeftHandTransform
        {
            get
            {
                if (leftHandTransform == null)
                {
                    throw new InvalidOperationException($"{nameof(leftHandTransform)} is null.");
                }

                return leftHandTransform;
            }
        }

        [SerializeField] private Transform rightTransform;
        private Transform RightHandTransform
        {
            get
            {
                if (rightTransform == null)
                {
                    throw new InvalidOperationException($"{nameof(rightTransform)} is null.");
                }

                return rightTransform;
            }
        }

#pragma warning restore IDE0044

        protected override IHumanPoseTrackerHardware Hardware => DeviceManagement.HumanPoseTracker.Hardware;

        protected override IHumanPoseTrackerSoftware Software => ToTrack.HumanPose(OriginTransform, HeadTransform, LeftHandTransform, RightHandTransform);
    }
}
