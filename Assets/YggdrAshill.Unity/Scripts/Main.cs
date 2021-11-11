using System.Collections.Generic;
using UnityEngine;

namespace YggdrAshill.Unity
{
    [DisallowMultipleComponent]
    internal sealed class Main : Singleton<Main>
    {
#pragma warning disable IDE0044
        [SerializeField] private bool dontDestroyOnLoad;
#pragma warning restore IDE0044

        [SerializeField] private GameObject[] prefabList;
        private GameObject[] PrefabList
        {
            get
            {
                if (prefabList == null)
                {
                    prefabList = new GameObject[0];
                }

                return prefabList;
            }
        }

        private readonly List<GameObject> instanceList = new List<GameObject>();

        private void OnEnable()
        {
            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);

                foreach (var prefab in PrefabList)
                {
                    var instance = Instantiate(prefab);

                    DontDestroyOnLoad(instance);

                    instanceList.Add(instance);
                }
            }
            else
            {
                foreach (var prefab in PrefabList)
                {
                    var instance = Instantiate(prefab);

                    instanceList.Add(instance);
                }
            }
        }

        private void OnDisable()
        {
            foreach (var instance in instanceList)
            {
                Destroy(instance);
            }

            instanceList.Clear();
        }
    }
}
