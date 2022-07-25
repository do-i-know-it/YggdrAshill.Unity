using UnityEngine;
using UnityEngine.UI;

namespace YggdrAshill.Samples
{
    internal sealed class SignInConsole : MonoBehaviour
    {
        [SerializeField] private Button signInButton;
        [SerializeField] private ScenarioConsole scenarioConsole;
        private void ActivateScenarioConsole()
        {
            gameObject.SetActive(false);

            scenarioConsole.Activate();
        }

        [SerializeField] private Button licenseButton;
        [SerializeField] private GameObject licenseViewer;
        private void ToggleLicenseViewer()
        {
            licenseViewer.SetActive(!licenseViewer.activeInHierarchy);
        }

        [SerializeField] private Texture2D backgroundImage;

        internal void Activate()
        {
            gameObject.SetActive(true);

            BackgroundImageManager.Instance.SetTexture(backgroundImage);
        }

        private void Start()
        {
            Activate();
        }

        private void OnEnable()
        {
            signInButton.onClick.AddListener(ActivateScenarioConsole);
            licenseButton.onClick.AddListener(ToggleLicenseViewer);
        }

        private void OnDisable()
        {
            signInButton.onClick.RemoveListener(ActivateScenarioConsole);
            licenseButton.onClick.RemoveListener(ToggleLicenseViewer);
        }
    }
}
