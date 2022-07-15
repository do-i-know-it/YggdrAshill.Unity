using UnityEngine;
using UnityEngine.UI;

namespace YggdrAshill.Samples
{
    internal sealed class SignInPanel : MonoBehaviour
    {
        [SerializeField] private Button button;

        [SerializeField] private ScenarioStore scenarioStore;

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

        private Material previous;

        private void GoToScenarioSelection()
        {
            gameObject.SetActive(false);

            scenarioStore.gameObject.SetActive(true);
        }

        private void OnEnable()
        {
            previous = RenderSettings.skybox;

            RenderSettings.skybox = BackgroundMateral;

            scenarioStore.gameObject.SetActive(false);

            button.onClick.AddListener(GoToScenarioSelection);
        }

        private void OnDisable()
        {
            RenderSettings.skybox = previous;

            button.onClick.RemoveListener(GoToScenarioSelection);
        }
    }
}
