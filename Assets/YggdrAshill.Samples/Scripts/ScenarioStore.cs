using UnityEngine;
using UnityEngine.UI;

namespace YggdrAshill.Samples
{
    internal sealed class ScenarioStore : MonoBehaviour
    {
        [SerializeField] private SignInPanel panel;
        [SerializeField] private Button signOutButton;

        private void GoToSigIn()
        {
            gameObject.SetActive(false);

            panel.gameObject.SetActive(true);
        }

        [SerializeField] private StageStore stageStore;
        private void GoToStageSelection()
        {
            stageStore.transform.position = transform.position;
            stageStore.transform.rotation = transform.rotation;

            gameObject.SetActive(false);

            stageStore.gameObject.SetActive(true);
        }

        [SerializeField] private Texture2D background;
        private Material backgroundMateral;
        private Material BackgroundMateral
        {
            get
            {
                if (backgroundMateral == null)
                {
                    backgroundMateral = new Material(RenderSettings.skybox);
                    backgroundMateral.SetTexture("_MainTex", background);
                }

                return backgroundMateral;
            }
        }

        [SerializeField] private Button[] stageButtons;

        private Material previous;

        private void OnEnable()
        {
            previous = RenderSettings.skybox;

            RenderSettings.skybox = BackgroundMateral;

            stageStore.gameObject.SetActive(false);

            foreach (var button in stageButtons)
            {
                button.onClick.AddListener(GoToStageSelection);
            }

            signOutButton.onClick.AddListener(GoToSigIn);
        }

        private void OnDisable()
        {
            RenderSettings.skybox = previous;

            foreach (var button in stageButtons)
            {
                button.onClick.RemoveListener(GoToStageSelection);
            }

            signOutButton.onClick.RemoveListener(GoToSigIn);
        }
    }
}
