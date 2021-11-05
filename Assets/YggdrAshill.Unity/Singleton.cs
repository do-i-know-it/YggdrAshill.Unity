using System;
using UnityEngine;

namespace YggdrAshill.Unity
{
    public abstract class Singleton<T> : MonoBehaviour
        where T : MonoBehaviour
    {
        private static readonly object lockObject = new object();
        
        private static volatile T instance;
        public static T Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance != null)
                    {
                        return instance;
                    }

                    instance = FindObjectOfType<T>();

                    if (instance != null)
                    {
                        return instance;
                    }

                    throw new InvalidOperationException($"{nameof(instance)} is null.");
                }
            }
            set
            {
                lock (lockObject)
                {
                    instance = value;
                }
            }
        }

        #region Unity messages

        protected virtual void Awake()
        {
            if (Instance == this)
            {
                return;
            }

            DestroyImmediate(this);
        }

        protected virtual void OnDestroy()
        {
            if (Instance != this)
            {
                return;
            }

            Instance = null;
        }

        #endregion
    }
}
