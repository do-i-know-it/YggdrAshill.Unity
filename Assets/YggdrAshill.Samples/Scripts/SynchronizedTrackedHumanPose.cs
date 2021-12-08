using YggdrAshill.Unity;
using YggdrAshill.Nuadha.Unity;
using YggdrAshill.VContainer;
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

        [SerializeField] private PhotonView headPhotonView;
        
        [SerializeField] private PhotonView leftHandPhotonView;

        [SerializeField] private PhotonView rightHandPhotonView;

        protected override void Configure(IContainerBuilder builder)
        {
            if (!originPhotonView.IsMine)
            {
                Destroy(this);

                return;
            }

            builder
                .RegisterInstance(DeviceManagement.HumanPoseTracker.Hardware)
                .AsSelf();
            builder
                .RegisterInstance(ToTrack.HumanPose(originPhotonView.transform, headPhotonView.transform, leftHandPhotonView.transform, rightHandPhotonView.transform))
                .AsSelf();

            builder.RegisterEntryPoint<TrackHumanPoseEntryPoint>();
        }
    }
}
