using YggdrAshill.Nuadha.Unitization;
using YggdrAshill.Nuadha.Units;

namespace YggdrAshill.Nuadha.Unity
{
    public interface IThreePointPoseTrackerSoftware :
        IModule
    {
        IPoseTrackerSoftware Origin { get; }

        IPoseTrackerSoftware Head { get; }

        IPoseTrackerSoftware LeftHand { get; }

        IPoseTrackerSoftware RightHand { get; }
    }
}
