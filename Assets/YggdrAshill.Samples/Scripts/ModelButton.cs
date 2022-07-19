using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace YggdrAshill.Samples
{
    internal sealed class ModelButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI text;

        private GameObject prefab;

        internal void Register(GameObject prefab)
        {
            this.prefab = prefab;

            text.text = prefab.name;
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
                return;
            }

            if (targetTransform == null)
            {
                Instantiate(prefab);
                
                return;
            }

            var model = Instantiate(prefab, targetTransform);

            model.transform.position = targetTransform.position;
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
