using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace YggdrAshill.Samples
{
    internal sealed class PhotonController : MonoBehaviourPunCallbacks
    {
        [SerializeField] private PhotonView viewPrefab;

        private void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby()
        {
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            PhotonNetwork.CreateRoom(null, new RoomOptions());
        }

        public override void OnJoinedRoom()
        {
            PhotonNetwork.Instantiate(viewPrefab.name, Vector3.zero, Quaternion.identity);
        }
    }
}
