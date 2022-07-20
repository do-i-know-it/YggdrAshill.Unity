﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Linq;

namespace YggdrAshill.Samples
{
    internal sealed class BackgroundStore : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI countView;
        [SerializeField] private BackgroundButton[] backgroundButtons;
        [SerializeField] private Button[] addButtons;

        [SerializeField] private Button previousButton;
        [SerializeField] private Button nextButton;
        private int maxPageIndex;
        private int pageIndex;
        private bool IsFirstPage => pageIndex == 0;
        private void GoToPreviousPage()
        {
            if (IsFirstPage)
            {
                return;
            }

            pageIndex--;

            UpdateView();
        }
        private bool IsLastPage => pageIndex == maxPageIndex;
        private void GoToNextPage()
        {
            if (IsLastPage)
            {
                return;
            }

            pageIndex++;

            UpdateView();
        }
        private bool addButtonActivated;
        private void UpdateView()
        {
            previousButton.interactable = !IsFirstPage;
            nextButton.interactable = !IsLastPage;

            countView.text = $"{(pageIndex + 1).ToString("000")}/{(maxPageIndex + 1).ToString("000")}";

            addButtonActivated = false;

            if (filePaths.Length == 0)
            {
                foreach (var button in backgroundButtons)
                {
                    button.gameObject.SetActive(false);
                }

                foreach (var button in addButtons)
                {
                    if (addButtonActivated)
                    {
                        button.gameObject.SetActive(false);

                        continue;
                    }

                    button.gameObject.SetActive(true);

                    addButtonActivated = true;
                }

                return;
            }

            for (var index = 0; index < backgroundButtons.Length; index++)
            {
                var offsettedIndex = index + backgroundButtons.Length * pageIndex;

                if (offsettedIndex < filePaths.Length)
                {
                    backgroundButtons[index].gameObject.SetActive(true);
                    backgroundButtons[index].Register(filePaths[offsettedIndex]);

                    addButtons[index].gameObject.SetActive(false);

                    continue;
                }

                backgroundButtons[index].gameObject.SetActive(false);

                if (addButtonActivated)
                {
                    continue;
                }

                addButtons[index].gameObject.SetActive(true);

                addButtonActivated = true;
            }
        }

        internal void SetBackgroundChanger(BackgroundChanger backgroundChanger)
        {
            foreach (var button in backgroundButtons)
            {
                button.SetBackgroundChanger(backgroundChanger);
            }
        }

        private static string directoryPath;
        private static string DirectoryPath
        {
            get
            {
                if (directoryPath == null)
                {
                    const string FilePath = "Pictures/Backgrounds";
#if UNITY_EDITOR
                    directoryPath = $"{Application.persistentDataPath}/{FilePath}";
#elif UNITY_ANDROID
                    const string UnityPlayer = "com.unity3d.player.UnityPlayer";
                    const string CurrentActivity = "currentActivity";
                    const string GetExternalFilesDir = "getExternalFilesDir";
                    const string GetAbsolutePath = "getAbsolutePath";

                    using (var unityPlayer = new AndroidJavaClass(UnityPlayer))
                    using (var currentActivity = unityPlayer.GetStatic<AndroidJavaObject>(CurrentActivity))
                    using (var directoryFile = currentActivity.Call<AndroidJavaObject>(GetExternalFilesDir, FilePath))
                    {
                        directoryPath = directoryFile.Call<string>(GetAbsolutePath);
                    }
#endif
                }

                return directoryPath;
            }
        }
        private const string PNG = "*.jpg";

        private string[] filePaths;

        private void OnEnable()
        {
            if (!Directory.Exists(DirectoryPath))
            {
                filePaths = new string[0];
            }
            else
            {
                filePaths = Directory.GetFiles(DirectoryPath, PNG).ToArray();
            }

            maxPageIndex = filePaths.Length / backgroundButtons.Length;
            if ((filePaths.Length % backgroundButtons.Length) == 0 && maxPageIndex != 0)
            {
                maxPageIndex--;
            }

            UpdateView();

            previousButton.onClick.AddListener(GoToPreviousPage);
            nextButton.onClick.AddListener(GoToNextPage);
        }

        private void OnDisable()
        {
            previousButton.onClick.RemoveListener(GoToPreviousPage);
            nextButton.onClick.RemoveListener(GoToNextPage);
        }
    }
}
