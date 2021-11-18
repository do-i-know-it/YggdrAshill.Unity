using YggdrAshill.Nuadha.Units;

namespace YggdrAshill.Nuadha.Unity
{
    public interface IHeadMountedDisplay
    {
        IHeadTrackerProtocol Head { get; }

        IHandControllerProtocol LeftHand { get; }

        IHandControllerProtocol RightHand { get; }
    }
}
