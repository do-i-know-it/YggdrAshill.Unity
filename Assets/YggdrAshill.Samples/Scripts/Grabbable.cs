using UnityEngine;

namespace YggdrAshill.Samples
{
    internal sealed class Grabbable : MonoBehaviour
    {
        [SerializeField] private Transform targetTransform;

        private Pose relativePose;

        public void GrabBegin(Pose on)
        {
            var relativePosition = Quaternion.Inverse(on.rotation) * (targetTransform.position - on.position);

            var relativeRotation = targetTransform.rotation * Quaternion.Inverse(on.rotation);

            relativePose = new Pose(relativePosition, relativeRotation);
        }

        public void Grab(Pose previous, Pose current, Pose next)
        {
            targetTransform.position = current.position + current.rotation * relativePose.position;

            targetTransform.rotation = relativePose.rotation * current.rotation;
        }

        public void GrabEnd(Pose from)
        {
            relativePose = Pose.identity;
        }
    }
}
