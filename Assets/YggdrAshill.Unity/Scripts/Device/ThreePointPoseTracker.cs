using YggdrAshill.Nuadha;
using YggdrAshill.Nuadha.Units;
using YggdrAshill.Nuadha.Unity;
using UnityEngine;

namespace YggdrAshill.Unity
{
    /// <summary>
    /// Implementation of <see cref="IHeadMountedDisplayProtocol"/>.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class ThreePointPoseTracker : Singleton<ThreePointPoseTracker>,
        IThreePointPoseTrackerProtocol
    {
        /// <inheritdoc/>
        public IPoseTrackerProtocol Head { get; } = PoseTracker.WithLatestCache();

        /// <inheritdoc/>
        public IPoseTrackerProtocol LeftHand { get; } = PoseTracker.WithLatestCache();

        /// <inheritdoc/>
        public IPoseTrackerProtocol RightHand { get; } = PoseTracker.WithLatestCache();
    }
}
