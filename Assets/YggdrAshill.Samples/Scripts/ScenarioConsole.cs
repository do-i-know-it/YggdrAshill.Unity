using UnityEngine;
using UnityEngine.UI;

namespace YggdrAshill.Samples
{
    internal sealed class ScenarioConsole : MonoBehaviour
    {
        [SerializeField] private Button signOutButton;
        [SerializeField] private SignInConsole signInConsole;
        private void ActivateSignInConsole()
        {
            gameObject.SetActive(false);

            signInConsole.Activate();
        }

        [SerializeField] private Button scenarioButton;
        [SerializeField] private StageConsole stageConsole;
        private void ActivateStageConsole()
        {
            gameObject.SetActive(false);

            stageConsole.Activate();
        }

        [SerializeField] private Texture2D backgroundImage;

        internal void Activate()
        {
            gameObject.SetActive(true);

            BackgroundImageManager.Instance.SetTexture(backgroundImage);
        }

        private void OnEnable()
        {
            scenarioButton.onClick.AddListener(ActivateStageConsole);
            signOutButton.onClick.AddListener(ActivateSignInConsole);
        }

        private void OnDisable()
        {
            scenarioButton.onClick.RemoveListener(ActivateStageConsole);
            signOutButton.onClick.RemoveListener(ActivateSignInConsole);
        }
    }
}
