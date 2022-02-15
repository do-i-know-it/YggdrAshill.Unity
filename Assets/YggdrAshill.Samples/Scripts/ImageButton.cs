using System.IO;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace YggdrAshill.Samples
{
    internal sealed class ImageButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI text;

        [SerializeField] private LoadedImage imagePrefab;

        private string filePath;

        internal void Register(string filePath)
        {
            this.filePath = filePath;

            text.text = Path.GetFileName(filePath);
        }

        private Transform targetTransform;

        internal void SetTargetTransform(Transform transform)
        {
            targetTransform = transform;
        }

        private void Load()
        {
            StartCoroutine(LoadAsync());
        }
        private IEnumerator LoadAsync()
        {
            var request = UnityWebRequestTexture.GetTexture(filePath);

            yield return request.SendWebRequest();

            switch (request.result)
            {
                case UnityWebRequest.Result.ProtocolError:
                    break;
                case UnityWebRequest.Result.ConnectionError:
                    break;
                case UnityWebRequest.Result.Success:
                    var texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                    var image = Instantiate(imagePrefab, targetTransform);
                    image.transform.position = targetTransform.position;
                    image.transform.rotation = targetTransform.rotation;
                    image.Render(texture);
                    break;
                default:
                    break;
            }
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
