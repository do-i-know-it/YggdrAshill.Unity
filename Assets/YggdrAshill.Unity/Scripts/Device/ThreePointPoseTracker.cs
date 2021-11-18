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
    public sealed class ThreePointPoseTracker : Singleton<ThreePointPoseTracker>,
        IThreePointPoseTracker
    {
        /// <inheritdoc/>
        public IPoseTrackerProtocol Head { get; } = PoseTracker.WithLatestCache();

        /// <inheritdoc/>
        public IPoseTrackerProtocol LeftHand { get; } = PoseTracker.WithLatestCache();

        /// <inheritdoc/>
        public IPoseTrackerProtocol RightHand { get; } = PoseTracker.WithLatestCache();
    }
}
