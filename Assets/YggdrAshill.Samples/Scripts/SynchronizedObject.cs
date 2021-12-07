using System;
using UnityEngine;
using Photon.Pun;

namespace YggdrAshill.Samples
{
    internal sealed class SynchronizedObject : MonoBehaviour
    {
        [SerializeField] private PhotonView view;
        private PhotonView View
        {
            get
            {
                if (view != null)
                {
                    return view;
                }

                if (TryGetComponent(out view))
                {
                    return view;
                }

                throw new InvalidOperationException($"{nameof(view)} is null.");
            }
        }

        [SerializeField] private Transform targetTransform;
        private Transform TargetTransform
        {
            get
            {
                if (targetTransform == null)
                {
                    targetTransform = transform;
                }

                return targetTransform;
            }
        }

        [SerializeField] private Grabbable grabbable;
        private Grabbable Grabbable
        {
            get
            {
                if (grabbable == null)
                {
                    throw new InvalidOperationException($"{nameof(grabbable)} is null.");
                }

                return grabbable;
            }
        }

        [SerializeField] private Transform performerPrefab;
        private Transform PerformerPrefab
        {
            get
            {
                if (performerPrefab == null)
                {
                    throw new InvalidOperationException($"{nameof(performerPrefab)} is null.");
                }

                return performerPrefab;
            }
        }

        [SerializeField] private Transform viewerPrefab;
        private Transform ViewerPrefab
        {
            get
            {
                if (viewerPrefab == null)
                {
                    throw new InvalidOperationException($"{nameof(viewerPrefab)} is null.");
                }

                return viewerPrefab;
            }
        }

        private Transform cache;

        private void OnEnable()
        {
            if (View.IsMine)
            {
                cache = Instantiate(PerformerPrefab, Grabbable.transform);
            }
            else
            {
                cache = Instantiate(ViewerPrefab, TargetTransform);

                Destroy(Grabbable);
            }

            cache.transform.position += Vector3.up * cache.transform.lossyScale.y * 0.5f;
        }

        private void OnDisable()
        {
            if (cache != null)
            {
                Destroy(cache);

                cache = null;
            }
        }
    }
}
