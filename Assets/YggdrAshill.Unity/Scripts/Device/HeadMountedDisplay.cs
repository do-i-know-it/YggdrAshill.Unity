using YggdrAshill.Nuadha;
using YggdrAshill.Nuadha.Units;
using YggdrAshill.Nuadha.Unity;
using UnityEngine;

namespace YggdrAshill.Unity
{
    /// <summary>
    /// Implementation of <see cref="IHeadMountedDisplay"/>.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class HeadMountedDisplay : Singleton<HeadMountedDisplay>,
        IHeadMountedDisplay
    {
        /// <inheritdoc/>
        public IHeadTrackerProtocol Head { get; } = HeadTracker.WithLatestCache();

        /// <inheritdoc/>
        public IHandControllerProtocol LeftHand { get; } = HandController.WithLatestCache();

        /// <inheritdoc/>
        public IHandControllerProtocol RightHand { get; } = HandController.WithLatestCache();
    }
}
