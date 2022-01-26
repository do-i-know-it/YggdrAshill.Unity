using System;
using UnityEngine;

namespace YggdrAshill.Unity
{
    [DisallowMultipleComponent]
    public sealed class CameraRig : Singleton<CameraRig>
    {
        public static Transform OriginTransform
        {
            get
            {
                if (Instance.originTransform == null)
                {
                    Instance.originTransform = Instance.transform;
                }

                return Instance.originTransform;
            }
        }

        public static Transform HeadTransform
        {
            get
            {
                if (Instance.headTransform == null)
                {
                    throw new InvalidOperationException($"{nameof(HeadTransform)} is null.");
                }

                if (Instance.headTransform == OriginTransform)
                {
                    throw new InvalidOperationException($"{nameof(HeadTransform)} is same as {nameof(OriginTransform)}.");
                }

                return Instance.headTransform;
            }
        }

        public static Transform LeftHandTransform
        {
            get
            {
                if (Instance.leftHandTransform == null)
                {
                    throw new InvalidOperationException($"{nameof(LeftHandTransform)} is null.");
                }

                if (Instance.leftHandTransform == OriginTransform)
                {
                    throw new InvalidOperationException($"{nameof(LeftHandTransform)} is same as {nameof(OriginTransform)}.");
                }

                if (Instance.leftHandTransform == HeadTransform)
                {
                    throw new InvalidOperationException($"{nameof(LeftHandTransform)} is same as {nameof(HeadTransform)}.");
                }

                return Instance.leftHandTransform;
            }
        }

        public static Transform RightHandTransform
        {
            get
            {
                if (Instance.rightTransform == null)
                {
                    throw new InvalidOperationException($"{nameof(RightHandTransform)} is null.");
                }

                if (Instance.rightTransform == OriginTransform)
                {
                    throw new InvalidOperationException($"{nameof(RightHandTransform)} is same as {nameof(OriginTransform)}.");
                }

                if (Instance.rightTransform == HeadTransform)
                {
                    throw new InvalidOperationException($"{nameof(RightHandTransform)} is same as {nameof(HeadTransform)}.");
                }

                return Instance.rightTransform;
            }
        }

#pragma warning disable IDE0044

        [SerializeField] private Transform originTransform;
        

        [SerializeField] private Transform headTransform;
        

        [SerializeField] private Transform leftHandTransform;
        

        [SerializeField] private Transform rightTransform;

#pragma warning restore IDE0044
    }
}
