using YggdrAshill.Nuadha.Units;

namespace YggdrAshill.Nuadha.Unity
{
    public interface IHeadMountedDisplayConfiguration
    {
        IPoseTrackerConfiguration Origin { get; }

        IHeadTrackerConfiguration Head { get; }

        IHandControllerConfiguration LeftHand { get; }

        IHandControllerConfiguration RightHand { get; }
    }
}
