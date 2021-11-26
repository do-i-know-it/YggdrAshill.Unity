using YggdrAshill.Nuadha.Units;

namespace YggdrAshill.Nuadha.Unity
{
    public interface IThreePointPoseTrackerCorrection
    {
        IPoseTrackerCorrection Origin { get; }

        IPoseTrackerCorrection Head { get; }

        IPoseTrackerCorrection LeftHand { get; }

        IPoseTrackerCorrection RightHand { get; }
    }
}
