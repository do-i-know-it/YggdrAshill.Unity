using YggdrAshill.Nuadha;
using UnityEngine;

namespace YggdrAshill.Unity.Samples
{
    [DisallowMultipleComponent]
    internal sealed class DebugLogOfHMD : MonoBehaviour
    {
        private CompositeCancellation cancellation;

        private void OnEnable()
        {
            cancellation = new CompositeCancellation();

            var module
                = HeadMountedDisplay
                .Instance
                .Hardware;

            // left hand
            module
                .LeftHand
                .Thumb
                .Touch
                .Produce(signal => Debug.Log($"Left hand stick touch: {signal}"))
                .Synthesize(cancellation);
            module
                .LeftHand
                .Thumb
                .Tilt
                .Produce(signal => Debug.Log($"Left hand stick tilt: {signal}"))
                .Synthesize(cancellation);

            module
                .LeftHand
                .IndexFinger
                .Touch
                .Produce(signal => Debug.Log($"Left hand index finger touch: {signal}"))
                .Synthesize(cancellation);
            module
                .LeftHand
                .IndexFinger
                .Pull
                .Produce(signal => Debug.Log($"Left hand index finger pull: {signal}"))
                .Synthesize(cancellation);

            module
                .LeftHand
                .HandGrip
                .Touch
                .Produce(signal => Debug.Log($"Left hand hand grip touch: {signal}"))
                .Synthesize(cancellation);
            module
                .LeftHand
                .HandGrip
                .Pull
                .Produce(signal => Debug.Log($"Left hand hand grip pull: {signal}"))
                .Synthesize(cancellation);

            // right hand
            module
                .RightHand
                .Thumb
                .Touch
                .Produce(signal => Debug.Log($"Right hand stick touch: {signal}"))
                .Synthesize(cancellation);
            module
                .RightHand
                .Thumb
                .Tilt
                .Produce(signal => Debug.Log($"Right hand stick tilt: {signal}"))
                .Synthesize(cancellation);

            module
                .RightHand
                .IndexFinger
                .Touch
                .Produce(signal => Debug.Log($"Right hand index finger touch: {signal}"))
                .Synthesize(cancellation);
            module
                .RightHand
                .IndexFinger
                .Pull
                .Produce(signal => Debug.Log($"Right hand index finger pull: {signal}"))
                .Synthesize(cancellation);

            module
                .RightHand
                .HandGrip
                .Touch
                .Produce(signal => Debug.Log($"Right hand hand grip touch: {signal}"))
                .Synthesize(cancellation);
            module
                .RightHand
                .HandGrip
                .Pull
                .Produce(signal => Debug.Log($"Right hand hand grip pull: {signal}"))
                .Synthesize(cancellation);
        }

        private void OnDisable()
        {
            cancellation.Cancel();

            cancellation = null;
        }
    }
}
