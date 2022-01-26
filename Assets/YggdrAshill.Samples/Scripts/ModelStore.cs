using TMPro;
using UnityEngine;

namespace YggdrAshill.Samples
{
    internal sealed class ModelStore : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI fallback;
        [SerializeField] private string[] filePathList;
        [SerializeField] private ModelButton[] modelButtons;

        private void Load()
        {
            if (filePathList.Length == 0)
            {
                foreach (var button in modelButtons)
                {
                    button.gameObject.SetActive(false);
                }

                fallback.gameObject.SetActive(true);

                fallback.text = "Image files not found.\n";

                return;
            }

            fallback.gameObject.SetActive(false);

            for (var index = 0; index < modelButtons.Length; index++)
            {
                if (filePathList.Length <= index)
                {
                    modelButtons[index].gameObject.SetActive(false);

                    continue;
                }

                modelButtons[index].gameObject.SetActive(true);

                modelButtons[index].Register(filePathList[index]);
            }
        }

        private void Start()
        {
            Load();
        }
    }
}
