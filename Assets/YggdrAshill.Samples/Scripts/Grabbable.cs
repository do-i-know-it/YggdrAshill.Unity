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

            var relativeRotation = Quaternion.Inverse(on.rotation) * targetTransform.rotation;

            relativePose = new Pose(relativePosition, relativeRotation);
        }

        public void Grab(Pose previous, Pose current, Pose next)
        {
            targetTransform.position = current.position + current.rotation * relativePose.position;

            targetTransform.rotation = current.rotation * relativePose.rotation;
        }

        public void GrabEnd(Pose from)
        {
            relativePose = Pose.identity;
        }
    }
}
