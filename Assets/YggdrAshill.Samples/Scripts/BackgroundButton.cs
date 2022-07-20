using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace YggdrAshill.Samples
{
    internal sealed class BackgroundButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI text;

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
            RecreateCancellationTokenSource();

            DisposeCapsuleIfNeeded();

            capsule = new Texture2DFromLocalFile(filePath);

            text.text = Path.GetFileNameWithoutExtension(filePath);
        }

        private BackgroundChanger backgroundChanger;

        internal void SetBackgroundChanger(BackgroundChanger backgroundChanger)
        {
            this.backgroundChanger = backgroundChanger;
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

            backgroundChanger.SetTexture(texture);
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
