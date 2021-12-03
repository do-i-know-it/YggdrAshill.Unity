using UnityEngine;
using Photon.Pun;

namespace YggdrAshill.Samples
{
    internal sealed class SynchronizedObjectTest : MonoBehaviour
    {
        [SerializeField] private SynchronizedObject synchronized;

        private GameObject cache;

        private void Update()
        {
            if (cache != null)
            {
                return;
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                cache = PhotonNetwork.Instantiate(synchronized.name, Vector3.zero, Quaternion.identity);
            }
        }

        private void OnDestroy()
        {
            if (cache != null)
            {
                PhotonNetwork.Destroy(cache.gameObject);

                cache = null;
            }
        }
    }
}
