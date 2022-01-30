using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace YggdrAshill.Samples
{
    internal sealed class StageButton : MonoBehaviour
    {
        [SerializeField] private Button button;

        internal UnityEvent BeforeActivation = new UnityEvent();

        private ModelStore modelStore;
        private ImageStore imageStore;

        private Transform targetTransform;
        private Transform TargetTransform
        {
            get
            {
                if (targetTransform == null)
                {
                    targetTransform = new GameObject().transform;
                    targetTransform.position = Vector3.zero;
                    targetTransform.rotation = Quaternion.identity;
                }

                return targetTransform;
            }
        }

        internal void SetConfiguration(ModelStore modelStore, ImageStore imageStore)
        {
            this.modelStore = modelStore;

            this.imageStore = imageStore;
        }

        private void OnEnable()
        {
            button.onClick.AddListener(ActivateStage);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(ActivateStage);
        }
        private void ActivateStage()
        {
            BeforeActivation.Invoke();

            for (var index = 0; index < TargetTransform.childCount; index++)
            {
                TargetTransform.GetChild(index).gameObject.SetActive(true);
            }

            modelStore.gameObject.SetActive(true);
            imageStore.gameObject.SetActive(true);
            modelStore.SetTargetTransform(TargetTransform);
            imageStore.SetTargetTransform(TargetTransform);
        }

        internal void DeactivateStage()
        {
            for (var index = 0; index < TargetTransform.childCount; index++)
            {
                TargetTransform.GetChild(index).gameObject.SetActive(false);
            }
        }
    }
}
