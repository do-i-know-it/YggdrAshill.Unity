using YggdrAshill.Unity;
using UnityEngine;
using Photon.Pun;

namespace YggdrAshill.Samples
{
    [DisallowMultipleComponent]
    internal sealed class SynchronizedTrackedHumanPose : MonoBehaviour
    {
        [SerializeField] private PhotonView originPhotonView;

        [SerializeField] private TrackHeadMountedDisplay headMountedDisplay;

        private void Awake()
        {
            if (originPhotonView.IsMine)
            {
                return;
            }

            Destroy(headMountedDisplay);
        }
    }
}
