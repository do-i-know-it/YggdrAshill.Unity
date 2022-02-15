using UnityEngine;
using UnityEngine.UI;

namespace YggdrAshill.Samples
{
    internal sealed class StageStore : MonoBehaviour
    {
        [SerializeField] private StageButton[] stageButtons;
        [SerializeField] private Transform anchor;
        [SerializeField] private BackgroundStore backgroundStore;
        [SerializeField] private ImageStore imageStore;
        [SerializeField] private ModelStore modelStore;

        [SerializeField] private Button previewButton;
        [SerializeField] private Transform itemSelector;

        [SerializeField] private Button backgroundStoreButton;
        [SerializeField] private Button imageStoreButton;
        [SerializeField] private Button modelStoreButton;
        [SerializeField] private Image backgroundStoreImage;
        [SerializeField] private Image imageStoreImage;
        [SerializeField] private Image modelStoreImage;

        [SerializeField] private Color onDeselected = Color.white;
        [SerializeField] private Color onSelected = Color.cyan;

        private void ActivateBackgroundStore()
        {
            DeactivateAllStores();
            backgroundStore.gameObject.SetActive(true);
            backgroundStoreImage.color = onSelected;
        }
        private void ActivateImageStore()
        {
            DeactivateAllStores();
            imageStore.gameObject.SetActive(true);
            imageStoreImage.color = onSelected;
        }
        private void ActivateModelStore()
        {
            DeactivateAllStores();
            modelStore.gameObject.SetActive(true);
            modelStoreImage.color = onSelected;
        }

        private void DeactivateAllStores()
        {
            backgroundStore.gameObject.SetActive(false);
            imageStore.gameObject.SetActive(false);
            modelStore.gameObject.SetActive(false);

            backgroundStoreImage.color = onDeselected;
            imageStoreImage.color = onDeselected;
            modelStoreImage.color = onDeselected;
        }

        private void DeactivateStages()
        {
            foreach (var button in stageButtons)
            {
                button.DeactivateStage();
            }
        }

        private void ToggleItemSelector()
        {
            ToggleItemSelector(!itemSelector.gameObject.activeInHierarchy);
        }

        private void ToggleItemSelector(bool active)
        {
            itemSelector.gameObject.SetActive(active);
            DeactivateAllStores();
        }

        private void EnableItemSelector()
        {
            ToggleItemSelector(true);
        }

        private void OnEnable()
        {
            ToggleItemSelector(false);

            previewButton.onClick.AddListener(ToggleItemSelector);

            backgroundStoreButton.onClick.AddListener(ActivateBackgroundStore);
            imageStoreButton.onClick.AddListener(ActivateImageStore);
            modelStoreButton.onClick.AddListener(ActivateModelStore);

            foreach (var button in stageButtons)
            {
                button.SetConfiguration(modelStore, imageStore, backgroundStore, anchor);
                button.BeforeActivation.AddListener(DeactivateStages);
                button.BeforeActivation.AddListener(EnableItemSelector);
            }
        }

        private void OnDisable()
        {
            previewButton.onClick.RemoveListener(ToggleItemSelector);

            backgroundStoreButton.onClick.RemoveListener(ActivateBackgroundStore);
            imageStoreButton.onClick.RemoveListener(ActivateImageStore);
            modelStoreButton.onClick.RemoveListener(ActivateModelStore);

            foreach (var button in stageButtons)
            {
                button.BeforeActivation.RemoveListener(DeactivateStages);
                button.BeforeActivation.RemoveListener(EnableItemSelector);
            }
        }
    }
}
