using YggdrAshill.Nuadha.Units;

namespace YggdrAshill.Nuadha.Unity
{
    public interface IThreePointPoseTrackerProtocol
    {
        IPoseTrackerProtocol Head { get; }

        IPoseTrackerProtocol LeftHand { get; }

        IPoseTrackerProtocol RightHand { get; }
    }
}
