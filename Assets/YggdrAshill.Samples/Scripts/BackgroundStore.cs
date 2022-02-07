using TMPro;
using UnityEngine;

namespace YggdrAshill.Samples
{
    internal sealed class BackgroundStore : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI fallback;
        [SerializeField] private string[] filePathList;
        [SerializeField] private BackgroundButton[] backgroundButtons;

        internal void SetBackgroundChanger(BackgroundChanger backgroundChanger)
        {
            foreach (var button in backgroundButtons)
            {
                button.SetBackgroundChanger(backgroundChanger);
            }
        }

        private void Load()
        {
            if (filePathList.Length == 0)
            {
                foreach (var button in backgroundButtons)
                {
                    button.gameObject.SetActive(false);
                }

                fallback.gameObject.SetActive(true);

                fallback.text = "Background image files not found.\n";

                return;
            }

            fallback.gameObject.SetActive(false);

            for (var index = 0; index < backgroundButtons.Length; index++)
            {
                if (filePathList.Length <= index)
                {
                    backgroundButtons[index].gameObject.SetActive(false);

                    continue;
                }

                backgroundButtons[index].gameObject.SetActive(true);

                backgroundButtons[index].Register(filePathList[index]);
            }
        }

        private void Start()
        {
            Load();
        }
    }
}
