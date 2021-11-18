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
        public static IHeadMountedDisplayProtocol HeadMountedDisplay => Unity.HeadMountedDisplay.Instance;

        /// <summary>
        /// <see cref="IThreePointPoseTrackerProtocol"/> accessed globally.
        /// </summary>
        public static IThreePointPoseTrackerProtocol ThreePointPoseTracker => Unity.ThreePointPoseTracker.Instance;
    }
}
