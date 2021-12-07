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
        /// <see cref="IPulsatedHeadMountedDisplayProtocol"/> accessed globally.
        /// </summary>
        public static IPulsatedHeadMountedDisplayProtocol PulsatedHeadMountedDisplay => Instance.pulsatedHeadMountedDisplay;

        /// <summary>
        /// <see cref="IHumanPoseTrackerProtocol"/> accessed globally.
        /// </summary>
        public static IHumanPoseTrackerProtocol HumanPoseTracker => Instance.humanPoseTracker;

        private readonly IHeadMountedDisplayProtocol headMountedDisplay = Nuadha.Unity.HeadMountedDisplay.WithLatestCache();

        private readonly IPulsatedHeadMountedDisplayProtocol pulsatedHeadMountedDisplay = Nuadha.Unity.PulsatedHeadMountedDisplay.WithLatestCache();

        private readonly IHumanPoseTrackerProtocol humanPoseTracker = Nuadha.Unity.HumanPoseTracker.WithLatestCache();
    }
}
