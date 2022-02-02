using UnityEngine;

namespace YggdrAshill.Samples
{
    internal sealed class BackgroundChanger : MonoBehaviour
    {
        private Material material;

        private Material previous;

        private bool loaded;

        internal void SetTexture(Texture2D texture)
        {
            DeapplyIfLoaded();

            material.SetTexture("_MainTex", texture);

            ApplyIfLoaded();

            loaded = true;
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
