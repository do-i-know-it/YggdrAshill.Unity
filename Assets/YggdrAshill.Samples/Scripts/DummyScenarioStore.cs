using UnityEngine;

namespace YggdrAshill.Samples
{
    internal sealed class DummyScenarioStore : MonoBehaviour
    {
        [SerializeField] private StageStore stageStore;

        private void Start()
        {
            stageStore.gameObject.SetActive(false);
        }

        public void GoToStageSelection()
        {
            stageStore.gameObject.SetActive(true);

            stageStore.transform.position = transform.position;
            stageStore.transform.rotation = transform.rotation;

            Destroy(gameObject);
        }
    }
}
