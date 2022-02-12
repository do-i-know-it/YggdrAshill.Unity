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
        private BackgroundStore backgroundStore;

        private Transform anchor;

        private Transform targetTransform;
        private Transform TargetTransform
        {
            get
            {
                if (targetTransform == null)
                {
                    targetTransform = new GameObject().transform;

                    targetTransform.position = anchor.position;
                    targetTransform.rotation = anchor.rotation;
                }

                return targetTransform;
            }
        }

        internal void SetConfiguration(ModelStore modelStore, ImageStore imageStore, BackgroundStore backgroundStore, Transform anchor)
        {
            this.modelStore = modelStore;

            this.imageStore = imageStore;

            this.backgroundStore = backgroundStore;

            this.anchor = anchor;
        }

        private BackgroundChanger backgroundChanger;

        private void Awake()
        {
            // 最初から可視化された状態で存在する前提の記述になっている
            backgroundChanger = TargetTransform.gameObject.AddComponent<BackgroundChanger>();
        }

        private void OnEnable()
        {
            button.onClick.AddListener(ActivateStage);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(ActivateStage);
        }

        private void OnDestroy()
        {
            backgroundChanger = null;
        }

        private void ActivateStage()
        {
            BeforeActivation.Invoke();

            TargetTransform.gameObject.SetActive(true);

            modelStore.gameObject.SetActive(true);
            imageStore.gameObject.SetActive(true);
            backgroundStore.gameObject.SetActive(true);
            modelStore.SetTargetTransform(TargetTransform);
            imageStore.SetTargetTransform(TargetTransform);
            backgroundStore.SetBackgroundChanger(backgroundChanger);
        }

        internal void DeactivateStage()
        {
            TargetTransform.gameObject.SetActive(false);
        }
    }
}
