using UnityEngine;

namespace YggdrAshill.Samples
{
    internal sealed class LoadedImage : MonoBehaviour
    {
        [SerializeField] private BoxCollider targetCollider;

        [SerializeField] private SpriteRenderer targetRenderer;

        [SerializeField] private float depth = 0.01f;

        private bool isLoaded;

        internal void Render(Texture2D texture)
        {
            Clear();

            var standard = Mathf.Max(texture.width, texture.height);
            var area = new Rect(0, 0, texture.width, texture.height);
            var pivot = Vector2.one * 0.5f;
            var sprite = Sprite.Create(texture, area, pivot, standard, 0, SpriteMeshType.FullRect);
            sprite.name = texture.name;

            targetRenderer.sprite = sprite;

            var boundSize = sprite.bounds.size;
            targetCollider.size = new Vector3(boundSize.x, boundSize.y, depth);
            targetCollider.center = new Vector3(0, 0, depth * 0.5f);

            isLoaded = true;
        }

        private void Clear()
        {
            if (!isLoaded)
            {
                return;
            }

            Destroy(targetRenderer.sprite);

            targetRenderer.sprite = null;

            isLoaded = false;
        }

        private void OnEnable()
        {
            Clear();
        }

        private void OnDisable()
        {
            Clear();
        }
    }
}
