using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace YggdrAshill.Samples
{
    internal sealed class BackgroundStore : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI fallback;
        [SerializeField] private TextMeshProUGUI countView;
        [SerializeField] private Texture2D[] textures;
        [SerializeField] private BackgroundButton[] backgroundButtons;
        [SerializeField] private Button[] addButtons;

        [SerializeField] private Button previousButton;
        [SerializeField] private Button nextButton;
        private int maxPageIndex;
        private int pageIndex;
        private bool IsFirstPage => pageIndex == 0;
        private void GoToPreviousPage()
        {
            if (IsFirstPage)
            {
                return;
            }

            pageIndex--;

            UpdateView();
        }
        private bool IsLastPage => pageIndex == maxPageIndex;
        private void GoToNextPage()
        {
            if (IsLastPage)
            {
                return;
            }

            pageIndex++;

            UpdateView();
        }
        private bool addButtonActivated;
        private void UpdateView()
        {
            previousButton.interactable = !IsFirstPage;
            nextButton.interactable = !IsLastPage;

            countView.text = $"{(pageIndex + 1).ToString("000")}/{(maxPageIndex + 1).ToString("000")}";

            addButtonActivated = false;

            if (textures.Length == 0)
            {
                foreach (var button in backgroundButtons)
                {
                    button.gameObject.SetActive(false);
                }

                foreach (var button in addButtons)
                {
                    if (addButtonActivated)
                    {
                        button.gameObject.SetActive(false);

                        continue;
                    }

                    button.gameObject.SetActive(true);

                    addButtonActivated = true;
                }

                fallback.gameObject.SetActive(true);

                fallback.text = "Background image files not found.\n";

                return;
            }

            for (var index = 0; index < backgroundButtons.Length; index++)
            {
                var offsettedIndex = index + backgroundButtons.Length * pageIndex;

                if (offsettedIndex < textures.Length)
                {
                    backgroundButtons[index].gameObject.SetActive(true);
                    backgroundButtons[index].Register(textures[offsettedIndex]);

                    addButtons[index].gameObject.SetActive(false);

                    continue;
                }

                backgroundButtons[index].gameObject.SetActive(false);

                if (addButtonActivated)
                {
                    continue;
                }

                addButtons[index].gameObject.SetActive(true);

                addButtonActivated = true;
            }

            fallback.gameObject.SetActive(false);
        }

        internal void SetBackgroundChanger(BackgroundChanger backgroundChanger)
        {
            foreach (var button in backgroundButtons)
            {
                button.SetBackgroundChanger(backgroundChanger);
            }
        }

        private void OnEnable()
        {
            previousButton.onClick.AddListener(GoToPreviousPage);
            nextButton.onClick.AddListener(GoToNextPage);
        }

        private void OnDisable()
        {
            previousButton.onClick.RemoveListener(GoToPreviousPage);
            nextButton.onClick.RemoveListener(GoToNextPage);
        }

        private void Start()
        {
            maxPageIndex = textures.Length / backgroundButtons.Length;

            if ((textures.Length % backgroundButtons.Length) == 0)
            {
                maxPageIndex--;
            }

            UpdateView();
        }
    }
}
