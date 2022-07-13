using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;

namespace YggdrAshill.Samples
{
    public sealed class Texture2DFromLocalFile :
        ICapsule<Texture2D>
    {
        private readonly LocalFilePath filePath;

        private Texture2D texture;

        public Texture2DFromLocalFile(string filePath) : this(new LocalFilePath(filePath))
        {

        }

        public Texture2DFromLocalFile(LocalFilePath filePath)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            this.filePath = filePath;
        }

        public async UniTask<Texture2D> LoadAysnc(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            texture = await LoadInternallyAysnc(token);

            token.ThrowIfCancellationRequested();

            return texture;
        }
        private async UniTask<Texture2D> LoadInternallyAysnc(CancellationToken token)
        {
            if (texture != null)
            {
                return texture;
            }

            token.ThrowIfCancellationRequested();

            var request = UnityWebRequestTexture.GetTexture((string)filePath);

            token.ThrowIfCancellationRequested();

            await request.SendWebRequest();

            token.ThrowIfCancellationRequested();

            await UniTask.SwitchToMainThread(token);

            token.ThrowIfCancellationRequested();

            switch (request.result)
            {
                case UnityWebRequest.Result.Success:
                    return ((DownloadHandlerTexture)request.downloadHandler).texture;
                case UnityWebRequest.Result.ProtocolError:
                    throw new InvalidOperationException();
                case UnityWebRequest.Result.ConnectionError:
                    throw new InvalidOperationException();
                default:
                    throw new InvalidOperationException();
            }
        }

        public void Dispose()
        {
            if (texture == null)
            {
                return;
            }

            UnityEngine.Object.Destroy(texture);

            texture = null;
        }
    }
}
