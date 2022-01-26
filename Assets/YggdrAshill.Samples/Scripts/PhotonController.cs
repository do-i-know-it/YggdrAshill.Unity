using YggdrAshill.Unity;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

namespace YggdrAshill.Samples
{
    internal sealed class PhotonController : MonoBehaviourPunCallbacks
    {
        [SerializeField] private PhotonView viewPrefab;

        [SerializeField] private SceneInformation scene;

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

        private Coroutine coroutine;

        public override void OnJoinedRoom()
        {
            coroutine = StartCoroutine(LoadScene());
        }
        private IEnumerator LoadScene()
        {
            PhotonNetwork.IsMessageQueueRunning = false;

            yield return SceneManager.LoadSceneAsync(scene.Path, LoadSceneMode.Additive);

            SceneManager.SetActiveScene(SceneManager.GetSceneByPath(scene.Path));

            PhotonNetwork.IsMessageQueueRunning = true;

            PhotonNetwork.Instantiate(viewPrefab.name, Vector3.zero, Quaternion.identity);

            coroutine = null;
        }

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            coroutine = StartCoroutine(ReloadLoadScene());
        }
        private IEnumerator ReloadLoadScene()
        {
            PhotonNetwork.IsMessageQueueRunning = false;

            yield return SceneManager.UnloadSceneAsync(scene.Path);

            PhotonNetwork.IsMessageQueueRunning = true;

            PhotonNetwork.LeaveRoom();

            coroutine = null;
        }
    }
}
