using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.UI;

namespace YggdrAshill.Samples
{
    internal sealed class StageButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image image;
        [SerializeField] private Color onDeselected = Color.white;
        [SerializeField] private Color onSelected = Color.cyan;

        internal UnityEvent BeforeActivation { get; } = new UnityEvent();

        private ModelStore modelStore;
        private ImageStore imageStore;
        private BackgroundStore backgroundStore;

        private Transform anchor;

        private Transform targetTransform;

        internal void SetConfiguration(ModelStore modelStore, ImageStore imageStore, BackgroundStore backgroundStore, Transform anchor)
        {
            Assert.IsNotNull(modelStore);
            Assert.IsNotNull(imageStore);
            Assert.IsNotNull(backgroundStore);
            Assert.IsNotNull(anchor);

            this.modelStore = modelStore;

            this.imageStore = imageStore;

            this.backgroundStore = backgroundStore;

            this.anchor = anchor;
        }

        private BackgroundChanger backgroundChanger;

        private void OnEnable()
        {
            targetTransform = new GameObject().transform;

            targetTransform.position = anchor.position;
            targetTransform.rotation = anchor.rotation;

            backgroundChanger = targetTransform.gameObject.AddComponent<BackgroundChanger>();

            button.onClick.AddListener(ActivateStage);
        }

        private void OnDisable()
        {
            if (targetTransform != null)
            {
                Destroy(targetTransform.gameObject);
                targetTransform = null;
                backgroundChanger = null;
            }

            button.onClick.RemoveListener(ActivateStage);
        }

        private void ActivateStage()
        {
            BeforeActivation.Invoke();

            targetTransform.gameObject.SetActive(true);
            image.color = onSelected;

            backgroundStore.gameObject.SetActive(false);
            imageStore.gameObject.SetActive(false);
            modelStore.gameObject.SetActive(false);

            backgroundStore.SetBackgroundChanger(backgroundChanger);
            imageStore.SetTargetTransform(targetTransform);
            modelStore.SetTargetTransform(targetTransform);
        }

        internal void DeactivateStage()
        {
            targetTransform.gameObject.SetActive(false);
            image.color = onDeselected;
        }
    }
}
