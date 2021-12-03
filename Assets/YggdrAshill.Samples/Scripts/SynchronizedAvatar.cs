using YggdrAshill.Unity;
using System;
using UnityEngine;
using Photon.Pun;

namespace YggdrAshill.Samples
{
    [DisallowMultipleComponent]
    internal sealed class SynchronizedAvatar : MonoBehaviour
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

        [SerializeField] private TrackedHumanPose trackedHumanPosePrefab;
        private TrackedHumanPose TrackedHumanPosePrefab
        {
            get
            {
                if (trackedHumanPosePrefab == null)
                {
                    throw new InvalidOperationException($"{nameof(trackedHumanPosePrefab)} is null.");
                }

                return trackedHumanPosePrefab;
            }
        }

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

        private TrackedHumanPose trackedHumanPose;

        private void Awake()
        {
            if (!View.IsMine)
            {
                return;
            }

            trackedHumanPose = Instantiate(TrackedHumanPosePrefab, OriginTransform);

            HeadTransform.parent = trackedHumanPose.HeadTransform;

            LeftHandTransform.parent = trackedHumanPose.LeftHandTransform;

            RightHandTransform.parent = trackedHumanPose.RightHandTransform;
        }

        private void OnDestroy()
        {
            if (trackedHumanPose == null)
            {
                Destroy(trackedHumanPose);

                trackedHumanPose = null;
            }
        }
    }
}
