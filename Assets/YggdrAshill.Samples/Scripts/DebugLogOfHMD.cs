using YggdrAshill.Unity;
using YggdrAshill.Nuadha;
using YggdrAshill.Nuadha.Conduction;
using System;
using UnityEngine;

namespace YggdrAshill.Samples
{
    [DisallowMultipleComponent]
    internal sealed class DebugLogOfHMD : MonoBehaviour
    {
        private IDisposable disposable;

        private void OnEnable()
        {
            var module
                = DeviceManagement.HeadMountedDisplay.Hardware;

            disposable
                = CancellationSource.Default
                .Synthesize(module.Head.Pose.Position.Produce(signal => Debug.Log($"Head position: {signal}")))
                .Synthesize(module.Head.Pose.Rotation.Produce(signal => Debug.Log($"Head rotation: {signal}")))
                .Synthesize(module.LeftHand.Pose.Position.Produce(signal => Debug.Log($"Left position: {signal}")))
                .Synthesize(module.LeftHand.Pose.Rotation.Produce(signal => Debug.Log($"Left rotation: {signal}")))
                .Synthesize(module.RightHand.Pose.Position.Produce(signal => Debug.Log($"Right position: {signal}")))
                .Synthesize(module.RightHand.Pose.Rotation.Produce(signal => Debug.Log($"Right rotation: {signal}")))
                .Synthesize(module.LeftHand.Thumb.Touch.Produce(signal => Debug.Log($"Left hand stick touch: {signal}")))
                .Synthesize(module.LeftHand.Thumb.Tilt.Produce(signal => Debug.Log($"Left hand stick tilt: {signal}")))
                .Synthesize(module.LeftHand.IndexFinger.Touch.Produce(signal => Debug.Log($"Left hand index finger touch: {signal}")))
                .Synthesize(module.LeftHand.IndexFinger.Pull.Produce(signal => Debug.Log($"Left hand index finger pull: {signal}")))
                .Synthesize(module.LeftHand.HandGrip.Touch.Produce(signal => Debug.Log($"Left hand hand grip touch: {signal}")))
                .Synthesize(module.LeftHand.HandGrip.Pull.Produce(signal => Debug.Log($"Left hand hand grip pull: {signal}")))
                .Synthesize(module.RightHand.Thumb.Touch.Produce(signal => Debug.Log($"Right hand stick touch: {signal}")))
                .Synthesize(module.RightHand.Thumb.Tilt.Produce(signal => Debug.Log($"Right hand stick tilt: {signal}")))
                .Synthesize(module.RightHand.IndexFinger.Touch.Produce(signal => Debug.Log($"Right hand index finger touch: {signal}")))
                .Synthesize(module.RightHand.IndexFinger.Pull.Produce(signal => Debug.Log($"Right hand index finger pull: {signal}")))
                .Synthesize(module.RightHand.HandGrip.Touch.Produce(signal => Debug.Log($"Right hand hand grip touch: {signal}")))
                .Synthesize(module.RightHand.HandGrip.Pull.Produce(signal => Debug.Log($"Right hand hand grip pull: {signal}")))
                .Build()
                .ToDisposable();
        }

        private void OnDisable()
        {
            disposable.Dispose();

            disposable = null;
        }
    }
}
