using YggdrAshill.Nuadha.Units;

namespace YggdrAshill.Nuadha.Unity
{
    public interface IHumanPoseTrackerConfiguration
    {
        IPoseTrackerConfiguration Origin { get; }

        IPoseTrackerConfiguration Head { get; }

        IPoseTrackerConfiguration LeftHand { get; }

        IPoseTrackerConfiguration RightHand { get; }
    }
}
