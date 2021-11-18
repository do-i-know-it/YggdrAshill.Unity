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
        /// <see cref="IHeadMountedDisplay"/> accessed globally.
        /// </summary>
        public static IHeadMountedDisplay HeadMountedDisplay => Unity.HeadMountedDisplay.Instance;

        /// <summary>
        /// <see cref="IThreePointPoseTracker"/> accessed globally.
        /// </summary>
        public static IThreePointPoseTracker ThreePointPoseTracker => Unity.ThreePointPoseTracker.Instance;
    }
}
