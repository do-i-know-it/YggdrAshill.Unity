using YggdrAshill.Nuadha.Unitization;
using YggdrAshill.Nuadha.Units;

namespace YggdrAshill.Nuadha.Unity
{
    public interface IThreePointPoseTrackerProtocol :
        IProtocol<IThreePointPoseTrackerHardware, IThreePointPoseTrackerSoftware>
    {
        IPoseTrackerProtocol Origin { get; }

        IPoseTrackerProtocol Head { get; }

        IPoseTrackerProtocol LeftHand { get; }

        IPoseTrackerProtocol RightHand { get; }
    }
}
