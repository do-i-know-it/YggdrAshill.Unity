using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace YggdrAshill.Samples
{
    internal sealed class ModelButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI text;

        private string filePath;

        private GameObject prefab;

        internal void Register(string filePath)
        {
            this.filePath = filePath;

            text.text = Path.GetFileName(filePath);

            if (prefab != null)
            {
                Resources.UnloadAsset(prefab);
                prefab = null;
            }
        }

        private Transform targetTransform;

        internal void SetTargetTransform(Transform transform)
        {
            targetTransform = transform;
        }

        private void Load()
        {
            if (prefab == null)
            {
                prefab = Resources.Load<GameObject>(filePath);
            }

            if (targetTransform == null)
            {
                Instantiate(prefab);
                return;
            }

            var model = Instantiate(prefab, targetTransform);
            model.transform.position = targetTransform.position + targetTransform.forward;
            model.transform.rotation = targetTransform.rotation;
        }

        private void OnEnable()
        {
            button.onClick.AddListener(Load);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(Load);
        }
    }
}
