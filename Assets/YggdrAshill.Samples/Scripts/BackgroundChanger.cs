using UnityEngine;

namespace YggdrAshill.Samples
{
    internal sealed class BackgroundChanger : MonoBehaviour
    {
        private Material material;

        private Material previous;

        private bool loaded;

        private bool textureIsAssigned;
        internal void SetTexture(Texture2D texture)
        {
            const string TextureName = "_MainTex";

            DeapplyIfLoaded();

            if (textureIsAssigned)
            {
                var previousTexture = material.GetTexture(TextureName);

                Destroy(previousTexture);
            }

            material.SetTexture(TextureName, texture);

            textureIsAssigned = true;
            loaded = true;

            ApplyIfLoaded();
        }

        private void Awake()
        {
            material = new Material(RenderSettings.skybox);
        }

        private void OnEnable()
        {
            ApplyIfLoaded();
        }

        private void OnDisable()
        {
            DeapplyIfLoaded();

        }

        private void OnDestroy()
        {
            Destroy(material);

            material = null;
        }

        private void ApplyIfLoaded()
        {
            if (!loaded)
            {
                return;
            }

            previous = RenderSettings.skybox;

            RenderSettings.skybox = material;
        }

        private void DeapplyIfLoaded()
        {
            if (!loaded)
            {
                return;
            }

            RenderSettings.skybox = previous;
        }
    }
}
