using System;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;

namespace YggdrAshill.Samples
{
    internal sealed class ImageStore : MonoBehaviour
    {
        [SerializeField] private ImageButton[] imageButtons;

        private const string UnityPlayer = "com.unity3d.player.UnityPlayer";
        private const string CurrentActivity = "currentActivity";
        private const string AndroidEnvironment = "android.os.Environment";
        private const string DirectoryPictures = "DIRECTORY_PICTURES";
        private const string GetExternalFilesDir = "getExternalFilesDir";
        private const string GetAbsolutePath = "getAbsolutePath";
        private static string directoryPath;
        private static string DirectoryPath
        {
            get
            {
                if (directoryPath == null)
                {
                    using (var unityPlayer = new AndroidJavaClass(UnityPlayer))
                    using (var currentActivity = unityPlayer.GetStatic<AndroidJavaObject>(CurrentActivity))
                    using (var directoryName = new AndroidJavaClass(AndroidEnvironment).GetStatic<AndroidJavaObject>(DirectoryPictures))
                    using (var directoryFile = currentActivity.Call<AndroidJavaObject>(GetExternalFilesDir, directoryName))
                    {
                        directoryPath = directoryFile.Call<string>(GetAbsolutePath);
                    }
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

            for (var index = 0; index < imageButtons.Length; index++)
            {
                if (filePaths.Length <= index)
                {
                    break;
                }

                imageButtons[index].Register(filePaths[index]);
            }
        }

        private void Start()
        {
            Load();
        }
    }
}
