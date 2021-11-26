using YggdrAshill.Nuadha.Units;

namespace YggdrAshill.Nuadha.Unity
{
    public interface IThreePointPoseTrackerConfiguration
    {
        IPoseTrackerConfiguration Origin { get; }

        IPoseTrackerConfiguration Head { get; }

        IPoseTrackerConfiguration LeftHand { get; }

        IPoseTrackerConfiguration RightHand { get; }
    }
}
