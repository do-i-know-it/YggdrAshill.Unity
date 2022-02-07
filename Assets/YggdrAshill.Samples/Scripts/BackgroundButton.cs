using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace YggdrAshill.Samples
{
    internal sealed class BackgroundButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI text;

        private string filePath;

        internal void Register(string filePath)
        {
            this.filePath = filePath;

            text.text = Path.GetFileName(filePath);

            if (prefab != null)
            {
                Resources.UnloadAsset(prefab);
                prefab = null;
            }
        }

        private BackgroundChanger backgroundChanger;

        internal void SetBackgroundChanger(BackgroundChanger backgroundChanger)
        {
            this.backgroundChanger = backgroundChanger;
        }

        private Texture2D prefab;

        private void Load()
        {
            if (prefab == null)
            {
                prefab = Resources.Load<Texture2D>(filePath);
            }

            backgroundChanger.SetTexture(prefab);
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
