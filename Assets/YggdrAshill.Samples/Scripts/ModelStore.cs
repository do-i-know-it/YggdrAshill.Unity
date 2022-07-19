using UnityEngine;
using TMPro;

namespace YggdrAshill.Samples
{
    internal sealed class ModelStore : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI fallback;
        [SerializeField] private GameObject[] prefabList;
        [SerializeField] private ModelButton[] modelButtons;

        internal void SetTargetTransform(Transform transform)
        {
            foreach (var button in modelButtons)
            {
                button.SetTargetTransform(transform);
            }
        }

        private void Load()
        {
            if (prefabList.Length == 0)
            {
                foreach (var button in modelButtons)
                {
                    button.gameObject.SetActive(false);
                }

                fallback.gameObject.SetActive(true);

                fallback.text = "Model files not found.\n";

                return;
            }

            fallback.gameObject.SetActive(false);

            for (var index = 0; index < modelButtons.Length; index++)
            {
                if (prefabList.Length <= index)
                {
                    modelButtons[index].gameObject.SetActive(false);

                    continue;
                }

                modelButtons[index].gameObject.SetActive(true);

                modelButtons[index].Register(prefabList[index]);
            }
        }

        private void OnEnable()
        {
            Load();
        }
    }
}
