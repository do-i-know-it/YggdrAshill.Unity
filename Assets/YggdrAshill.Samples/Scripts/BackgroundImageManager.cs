using UnityEngine;
using YggdrAshill.Unity;

namespace YggdrAshill.Samples
{
    internal sealed class BackgroundImageManager : Singleton<BackgroundImageManager>
    {
        private Material material;
        private void CreateMaterialIfNeeded()
        {
            if (material != null)
            {
                return;
            }

            material = new Material(RenderSettings.skybox);
        }
        private void DisposeMaterialIfNeeded()
        {
            if (material == null)
            {
                return;
            }

            Destroy(material);

            material = null;
        }

        private Material previous;
        private void SavePreviousMaterialIfNeeded()
        {
            if (previous != null)
            {
                return;
            }
         
            previous = RenderSettings.skybox;
        }
        private void LoadPreviousMaterialIfNeeded()
        {
            if (previous == null)
            {
                return;
            }

            RenderSettings.skybox = previous;

            previous = null;
        }

        private Texture2D texture;
        
        internal void SetTexture(Texture2D texture)
        {
            this.texture = texture;
        }

        internal void Reset()
        {
            texture = null;

            LoadPreviousMaterialIfNeeded();
        }

        protected override void Awake()
        {
            base.Awake();

            CreateMaterialIfNeeded();

            SavePreviousMaterialIfNeeded();
        }

        protected override void OnDestroy()
        {
            LoadPreviousMaterialIfNeeded();

            DisposeMaterialIfNeeded();

            base.OnDestroy();
        }

        private void LateUpdate()
        {
            if (texture == null)
            {
                return;
            }

            const string TextureName = "_MainTex";

            material.SetTexture(TextureName, texture);

            RenderSettings.skybox = material;
        }
    }
}
