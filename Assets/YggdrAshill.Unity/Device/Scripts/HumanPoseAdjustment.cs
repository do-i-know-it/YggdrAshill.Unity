using YggdrAshill.Nuadha.Unity;
using UnityEngine;

namespace YggdrAshill.Unity
{
    [CreateAssetMenu(menuName = "YggdrAshill/HumanPoseAdjustment")]
    public sealed class HumanPoseAdjustment : ScriptableObject
    {
        [SerializeField] private PoseAdjustment origin;
        [SerializeField] private PoseAdjustment head;
        [SerializeField] private PoseAdjustment leftHand;
        [SerializeField] private PoseAdjustment rightHand;

        private IHumanPoseTrackerConfiguration configuration;
        public IHumanPoseTrackerConfiguration Configuration
        {
            get
            {
                if (configuration == null)
                {
                    configuration = SimulateHumanPoseTracker.ToConfigure(origin.Pose, head.Pose, leftHand.Pose, rightHand.Pose);
                }

                return configuration;
            }
        }
    }
}
