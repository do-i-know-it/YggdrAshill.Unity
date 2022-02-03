﻿using UnityEngine;

namespace YggdrAshill.Samples
{
    internal sealed class StageStore : MonoBehaviour
    {
        [SerializeField] private ModelStore modelStore;
        [SerializeField] private ImageStore imageStore;
        [SerializeField] private BackgroundStore backgroundStore;
        [SerializeField] private Transform anchor;
        [SerializeField] private StageButton[] stageButtons;

        private void DeactivateStages()
        {
            foreach (var button in stageButtons)
            {
                button.DeactivateStage();
            }
        }

        private void OnEnable()
        {
            modelStore.gameObject.SetActive(false);
            imageStore.gameObject.SetActive(false);
            backgroundStore.gameObject.SetActive(false);

            foreach (var button in stageButtons)
            {
                button.SetConfiguration(modelStore, imageStore, backgroundStore, anchor);
                button.BeforeActivation.AddListener(DeactivateStages);
            }
        }

        private void OnDisable()
        {
            foreach (var button in stageButtons)
            {
                button.BeforeActivation.RemoveListener(DeactivateStages);
            }
        }
    }
}
