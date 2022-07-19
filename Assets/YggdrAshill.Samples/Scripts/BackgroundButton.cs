using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace YggdrAshill.Samples
{
    internal sealed class BackgroundButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI text;

        internal void Register(Texture2D texture)
        {
            this.texture = texture;

            text.text = texture.name;
        }

        private BackgroundChanger backgroundChanger;

        internal void SetBackgroundChanger(BackgroundChanger backgroundChanger)
        {
            this.backgroundChanger = backgroundChanger;
        }

        private Texture2D texture;

        private void Load()
        {
            if (texture == null)
            {
                return;
            }

            backgroundChanger.SetTexture(texture);
        }

        private void OnEnable()
        {
            button.onClick.AddListener(Load);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(Load);
        }
    }
}
