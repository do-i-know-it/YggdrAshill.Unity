using UnityEngine;

namespace YggdrAshill.Samples
{
    internal sealed class Grabbable : MonoBehaviour
    {
        [SerializeField] private Transform targetTransform;

        private Transform previousParent;

        public void Grab(Transform transform)
        {
            previousParent = targetTransform.parent;

            targetTransform.parent = transform;
        }

        public void Release(Transform transform)
        {
            targetTransform.parent = previousParent;
        }
    }
}
