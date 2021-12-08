using UnityEngine;
using Photon.Pun;

namespace YggdrAshill.Samples
{
    internal sealed class SynchronizedObject : MonoBehaviour
    {
        [SerializeField] private PhotonView photonView;

        [SerializeField] private Transform targetTransform;

        [SerializeField] private Grabbable grabbable;

        [SerializeField] private Transform performerPrefab;

        [SerializeField] private Transform viewerPrefab;

        private Transform cache;

        private void OnEnable()
        {
            if (photonView.IsMine)
            {
                cache = Instantiate(performerPrefab, grabbable.transform);
            }
            else
            {
                cache = Instantiate(viewerPrefab, targetTransform);

                Destroy(grabbable);
            }

            cache.transform.position += Vector3.up * cache.transform.lossyScale.y * 0.5f;
        }

        private void OnDisable()
        {
            if (cache != null)
            {
                Destroy(cache.gameObject);

                cache = null;
            }
        }
    }
}
