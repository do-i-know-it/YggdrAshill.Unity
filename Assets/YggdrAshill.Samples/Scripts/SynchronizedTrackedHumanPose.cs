using YggdrAshill.Unity;
using YggdrAshill.Nuadha.Unity;
using YggdrAshill.VContainer;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Photon.Pun;

namespace YggdrAshill.Samples
{
    [DisallowMultipleComponent]
    internal sealed class SynchronizedTrackedHumanPose : LifetimeScope
    {
        [SerializeField] private PhotonView originPhotonView;
        private PhotonView OriginPhotonView
        {
            get
            {
                if (originPhotonView != null)
                {
                    return originPhotonView;
                }

                if (TryGetComponent(out originPhotonView))
                {
                    return originPhotonView;
                }

                throw new InvalidOperationException($"{nameof(OriginPhotonView)} is null.");
            }
        }

        [SerializeField] private PhotonView headPhotonView;
        private PhotonView HeadPhotonView
        {
            get
            {
                if (headPhotonView == null)
                {
                    throw new InvalidOperationException($"{nameof(HeadPhotonView)} is null.");
                }

                if (headPhotonView == OriginPhotonView)
                {
                    throw new InvalidOperationException($"{nameof(HeadPhotonView)} is same as {nameof(OriginPhotonView)}.");
                }

                return headPhotonView;
            }
        }

        [SerializeField] private PhotonView leftHandPhotonView;
        private PhotonView LeftHandPhotonView
        {
            get
            {
                if (leftHandPhotonView == null)
                {
                    throw new InvalidOperationException($"{nameof(LeftHandPhotonView)} is null.");
                }

                if (leftHandPhotonView == OriginPhotonView)
                {
                    throw new InvalidOperationException($"{nameof(LeftHandPhotonView)} is same as {nameof(OriginPhotonView)}.");
                }

                return leftHandPhotonView;
            }
        }

        [SerializeField] private PhotonView rightPhotonView;
        private PhotonView RightHandPhotonView
        {
            get
            {
                if (rightPhotonView == null)
                {
                    throw new InvalidOperationException($"{nameof(RightHandPhotonView)} is null.");
                }

                if (rightPhotonView == OriginPhotonView)
                {
                    throw new InvalidOperationException($"{nameof(RightHandPhotonView)} is same as {nameof(OriginPhotonView)}.");
                }

                return rightPhotonView;
            }
        }

        protected override void Configure(IContainerBuilder builder)
        {
            if (!OriginPhotonView.IsMine)
            {
                Destroy(this);

                return;
            }

            builder
                .RegisterInstance(DeviceManagement.HumanPoseTracker.Hardware)
                .AsSelf();
            builder
                .RegisterInstance(ToTrack.HumanPose(OriginPhotonView.transform, HeadPhotonView.transform, LeftHandPhotonView.transform, RightHandPhotonView.transform))
                .AsSelf();

            builder.RegisterEntryPoint<TrackHumanPoseEntryPoint>();
        }
    }
}
