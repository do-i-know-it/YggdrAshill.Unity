using UnityEngine;
using UnityEngine.Events;

namespace YggdrAshill.Samples
{
    internal sealed class Clickable : MonoBehaviour
    {
        [SerializeField] private UnityEvent onClicked;

        public void Click()
        {
            onClicked.Invoke();
        }
    }
}
