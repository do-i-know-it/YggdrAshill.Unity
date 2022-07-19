using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace YggdrAshill.Samples
{
    internal sealed class ImageStore : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI fallback;
        [SerializeField] private TextMeshProUGUI countView;
        [SerializeField] private ImageButton[] imageButtons;
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
                foreach (var button in imageButtons)
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

                fallback.gameObject.SetActive(true);

                fallback.text = "Image files not found.\n";

                return;
            }

            for (var index = 0; index < imageButtons.Length; index++)
            {
                var offsettedIndex = index + imageButtons.Length * pageIndex;

                if (offsettedIndex < filePaths.Length)
                {
                    imageButtons[index].gameObject.SetActive(true);
                    imageButtons[index].Register(filePaths[offsettedIndex]);

                    addButtons[index].gameObject.SetActive(false);

                    continue;
                }

                imageButtons[index].gameObject.SetActive(false);

                if (addButtonActivated)
                {
                    continue;
                }

                addButtons[index].gameObject.SetActive(true);

                addButtonActivated = true;
            }

            fallback.gameObject.SetActive(false);
        }

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

        internal void SetTargetTransform(Transform transform)
        {
            foreach (var button in imageButtons)
            {
                button.SetTargetTransform(transform);
            }
        }

        private string[] filePaths;

        private void OnEnable()
        {
            filePaths = Directory.GetFiles(DirectoryPath, PNG).ToArray();

            maxPageIndex = filePaths.Length / imageButtons.Length;
            if ((filePaths.Length % imageButtons.Length) == 0)
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
