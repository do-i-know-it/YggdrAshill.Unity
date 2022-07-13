using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using Cysharp.Threading.Tasks;
using System;

namespace YggdrAshill.Samples
{
    internal sealed class ImageButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI text;

        [SerializeField] private LoadedImage imagePrefab;

        private ICapsule<Texture2D> capsule;
        private void DisposeCapsuleIfNeeded()
        {
            if (capsule == null)
            {
                return;
            }

            capsule.Dispose();
            capsule = null;
        }

        internal void Register(string filePath)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            RecreateCancellationTokenSource();

            DisposeCapsuleIfNeeded();

            capsule = new Texture2DFromLocalFile(filePath);

            text.text = Path.GetFileNameWithoutExtension(filePath);
        }

        private Transform targetTransform;

        internal void SetTargetTransform(Transform transform)
        {
            targetTransform = transform;
        }

        private void Load()
        {
            LoadAsync(source.Token).Forget();
        }
        private async UniTask LoadAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            var texture = await capsule.LoadAysnc(token);

            token.ThrowIfCancellationRequested();

            await UniTask.SwitchToMainThread(token);

            token.ThrowIfCancellationRequested();

            var image = Instantiate(imagePrefab, targetTransform);
            image.transform.position = targetTransform.position;
            image.transform.rotation = targetTransform.rotation;
            image.Render(texture);
        }

        private CancellationTokenSource source;
        private void CreateCancellationTokenSourceIfNeeded()
        {
            if (source != null)
            {
                return;
            }

            source = new CancellationTokenSource();
        }
        private void DisposeCancellationTokenSourceIfNeeded()
        {
            if (source == null)
            {
                return;
            }

            source.Cancel();
            source = null;
        }
        private void RecreateCancellationTokenSource()
        {
            DisposeCancellationTokenSourceIfNeeded();
            CreateCancellationTokenSourceIfNeeded();
        }

        private void OnEnable()
        {
            CreateCancellationTokenSourceIfNeeded();

            button.onClick.AddListener(Load);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(Load);

            DisposeCancellationTokenSourceIfNeeded();

            DisposeCapsuleIfNeeded();
        }
    }
}
