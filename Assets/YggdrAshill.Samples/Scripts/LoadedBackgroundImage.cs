using UnityEngine;

namespace YggdrAshill.Samples
{
    internal sealed class LoadedBackgroundImage : MonoBehaviour
    {
        private bool isLoaded;

        private Texture2D texture;

        internal void Render(Texture2D texture)
        {
            Clear();

            this.texture = texture;

            Render();
        }

        private void Render()
        {
            if (texture == null)
            {
                return;
            }

            BackgroundImageManager.Instance.SetTexture(texture);
        }

        private void Clear()
        {
            if (!isLoaded)
            {
                return;
            }

            Destroy(texture);

            texture = null;

            isLoaded = false;
        }

        private void Awake()
        {
            Clear();
        }

        private void OnDestroy()
        {
            Clear();
        }

        private void OnEnable()
        {
            Render();
        }
    }
}
