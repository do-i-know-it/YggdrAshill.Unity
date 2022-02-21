using UnityEngine;
using UnityEngine.UI;

namespace YggdrAshill.Samples
{
    internal sealed class ToggleLicenseDescription : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private GameObject target;

        private void Toggle()
        {
            target.SetActive(!target.activeInHierarchy);
        }

        private void OnEnable()
        {
            button.onClick.AddListener(Toggle);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(Toggle);
        }
    }
}
