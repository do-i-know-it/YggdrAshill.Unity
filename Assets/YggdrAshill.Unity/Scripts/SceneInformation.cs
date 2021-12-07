using System;
using UnityEngine;

namespace YggdrAshill.Unity
{
    [Serializable]
    public sealed class SceneInformation
#if UNITY_EDITOR
        : ISerializationCallbackReceiver
    {
#pragma warning disable IDE0044

        [SerializeField] private UnityEditor.SceneAsset scene = default;

#pragma warning restore IDE0044

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            path = UnityEditor.AssetDatabase.GetAssetPath(scene);
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {

        }

#else
    {
#endif
        [SerializeField] private string path = default;

        public string Path
        {
            get
            {
                if (path == null)
                {
                    throw new ArgumentNullException(nameof(path));
                }

                return path;
            }
        }
    }
}
