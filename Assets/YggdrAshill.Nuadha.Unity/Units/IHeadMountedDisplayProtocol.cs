using YggdrAshill.Nuadha.Units;

namespace YggdrAshill.Nuadha.Unity
{
    public interface IHeadMountedDisplayProtocol
    {
        IHeadTrackerProtocol Head { get; }

        IHandControllerProtocol LeftHand { get; }

        IHandControllerProtocol RightHand { get; }
    }
}
