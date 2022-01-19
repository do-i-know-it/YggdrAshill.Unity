using System;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;

namespace YggdrAshill.Samples
{
    internal sealed class ImageStore : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI fallback;
        [SerializeField] private ImageButton[] imageButtons;

        private static string directoryPath;
        private static string DirectoryPath
        {
            get
            {
                if (directoryPath == null)
                {
#if UNITY_EDITOR
                    directoryPath = Application.persistentDataPath;
#elif UNITY_ANDROID
                    const string UnityPlayer = "com.unity3d.player.UnityPlayer";
                    const string CurrentActivity = "currentActivity";
                    const string AndroidEnvironment = "android.os.Environment";
                    const string DirectoryPictures = "DIRECTORY_PICTURES";
                    const string GetExternalFilesDir = "getExternalFilesDir";
                    const string GetAbsolutePath = "getAbsolutePath";

                    using (var unityPlayer = new AndroidJavaClass(UnityPlayer))
                    using (var currentActivity = unityPlayer.GetStatic<AndroidJavaObject>(CurrentActivity))
                    using (var directoryName = new AndroidJavaClass(AndroidEnvironment).GetStatic<AndroidJavaObject>(DirectoryPictures))
                    using (var directoryFile = currentActivity.Call<AndroidJavaObject>(GetExternalFilesDir, directoryName))
                    {
                        directoryPath = directoryFile.Call<string>(GetAbsolutePath);
                    }
#endif
                }

                return directoryPath;
            }
        }
        private const string PNG = "*.png";

        private void Load()
        {
            var filePaths
                = Directory.GetFiles(DirectoryPath, PNG)
                .Select(fileName => $"file://{fileName}")
                .ToArray();

            if (filePaths.Length == 0)
            {
                foreach (var button in imageButtons)
                {
                    button.gameObject.SetActive(false);
                }

                fallback.gameObject.SetActive(true);

                fallback.text = "Image files not found.\n";

                return;
            }

            fallback.gameObject.SetActive(false);

            for (var index = 0; index < imageButtons.Length; index++)
            {
                if (filePaths.Length <= index)
                {
                    imageButtons[index].gameObject.SetActive(false);

                    continue;
                }

                imageButtons[index].gameObject.SetActive(true);

                imageButtons[index].Register(filePaths[index]);
            }
        }

        private void Start()
        {
            Load();
        }
    }
}
