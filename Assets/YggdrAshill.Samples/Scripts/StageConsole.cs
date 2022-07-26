using UnityEngine;
using UnityEngine.UI;

namespace YggdrAshill.Samples
{
    internal sealed class StageConsole : MonoBehaviour
    {
        private int? selectedIndex;
        [SerializeField] private Button homeButton;
        [SerializeField] private ScenarioConsole scenarioConsole;
        private void ActivateScenarioConsole()
        {
            gameObject.SetActive(false);

            scenarioConsole.Activate();
        }

        [SerializeField] private Button previewButton;
        [SerializeField] private Image previewImage;
        [SerializeField] private Transform itemSelector;
        private bool isPreview;
        private void TogglePreview()
        {
            if (isPreview)
            {
                DisablePreviewMode();
            }
            else
            {
                EnablePreviewMode();
            }
        }
        private void EnablePreviewMode()
        {
            DeactivateAllStages();
            DeactivateAllStores();

            itemSelector.gameObject.SetActive(false);

            selectedIndex = null;

            BackgroundImageManager.Instance.SetTexture(backgroundImage);

            previewImage.color = onSelected;
            isPreview = true;
        }
        private void DisablePreviewMode()
        {
            DeactivateAllStages();
            DeactivateAllStores();

            selectedIndex = null;

            BackgroundImageManager.Instance.SetTexture(backgroundImage);

            previewImage.color = onDeselected;
            isPreview = false;
        }

        [SerializeField] private Button[] stageButtons;
        [SerializeField] private Image[] stageImages;
        [SerializeField] private Transform[] anchorTransforms;
        private void DeactivateAllStages()
        {
            for (var index = 0; index < stageButtons.Length; index++)
            {
                var image = stageImages[index];
                var anchorTransform = anchorTransforms[index];

                anchorTransform.gameObject.SetActive(false);
                image.color = onDeselected;
            }
        }
        public void ActivateStage(int index)
        {
            DeactivateAllStages();
            DeactivateAllStores();

            itemSelector.gameObject.SetActive(true);

            var image = stageImages[index];
            var anchorTransform = anchorTransforms[index];

            anchorTransform.gameObject.SetActive(true);
            image.color = onSelected;

            imageStore.SetTargetTransform(anchorTransform);
            if (!anchorTransform.TryGetComponent<LoadedBackgroundImage>(out var loadedBackgroundImage))
            {
                loadedBackgroundImage = anchorTransform.gameObject.AddComponent<LoadedBackgroundImage>();
                loadedBackgroundImage.Render(backgroundImage);
            }
         
            backgroundStore.SetLoadedBackgroundImage(loadedBackgroundImage);

            selectedIndex = index;
        }

        [SerializeField] private Button backgroundStoreButton;
        [SerializeField] private Image backgroundStoreImage;
        [SerializeField] private BackgroundStore backgroundStore;
        private void ActivateBackgroundStore()
        {
            DeactivateAllStores();

            backgroundStore.gameObject.SetActive(true);
            backgroundStoreImage.color = onSelected;
        }
        
        [SerializeField] private Button imageStoreButton;
        [SerializeField] private Image imageStoreImage;
        [SerializeField] private ImageStore imageStore;
        private void ActivateImageStore()
        {
            DeactivateAllStores();
            
            imageStore.gameObject.SetActive(true);
            imageStoreImage.color = onSelected;
        }

        private void DeactivateAllStores()
        {
            backgroundStore.gameObject.SetActive(false);
            imageStore.gameObject.SetActive(false);

            backgroundStoreImage.color = onDeselected;
            imageStoreImage.color = onDeselected;
        }

        [SerializeField] private Texture2D backgroundImage;

        internal void Activate()
        {
            gameObject.SetActive(true);

            if (selectedIndex == null)
            {
                BackgroundImageManager.Instance.SetTexture(backgroundImage);
                return;
            }

            ActivateStage(selectedIndex.Value);
        }

        [SerializeField] private Color onDeselected = Color.white;
        [SerializeField] private Color onSelected = Color.cyan;

        private void OnEnable()
        {
            homeButton.onClick.AddListener(ActivateScenarioConsole);
            previewButton.onClick.AddListener(TogglePreview);

            backgroundStoreButton.onClick.AddListener(ActivateBackgroundStore);
            imageStoreButton.onClick.AddListener(ActivateImageStore);
        }

        private void OnDisable()
        {
            homeButton.onClick.RemoveListener(ActivateScenarioConsole);
            previewButton.onClick.RemoveListener(TogglePreview);

            backgroundStoreButton.onClick.RemoveListener(ActivateBackgroundStore);
            imageStoreButton.onClick.RemoveListener(ActivateImageStore);
        }
    }
}
