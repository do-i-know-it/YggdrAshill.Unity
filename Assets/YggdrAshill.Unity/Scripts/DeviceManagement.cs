using YggdrAshill.Nuadha.Unity;
using UnityEngine;

namespace YggdrAshill.Unity
{
    /// <summary>
    /// Entry point of device management.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class DeviceManagement : Singleton<DeviceManagement>
    {
        /// <summary>
        /// <see cref="IHeadMountedDisplayProtocol"/> accessed globally.
        /// </summary>
        public static IHeadMountedDisplayProtocol HeadMountedDisplay => Instance.headMountedDisplay;

        /// <summary>
        /// <see cref="IThreePointPoseTrackerProtocol"/> accessed globally.
        /// </summary>
        public static IThreePointPoseTrackerProtocol ThreePointPoseTracker => Instance.threePointPoseTracker;

        private readonly IHeadMountedDisplayProtocol headMountedDisplay = Nuadha.Unity.HeadMountedDisplay.WithLatestCache();

        private readonly IThreePointPoseTrackerProtocol threePointPoseTracker = Nuadha.Unity.ThreePointPoseTracker.WithLatestCache();
    }
}
