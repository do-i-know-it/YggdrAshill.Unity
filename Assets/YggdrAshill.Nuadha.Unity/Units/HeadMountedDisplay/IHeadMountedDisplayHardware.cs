using YggdrAshill.Nuadha.Unitization;
using YggdrAshill.Nuadha.Units;

namespace YggdrAshill.Nuadha.Unity
{
    public interface IHeadMountedDisplayHardware :
        IModule
    {
        IPoseTrackerHardware Origin { get; }

        IHeadTrackerHardware Head { get; }

        IHandControllerHardware LeftHand { get; }

        IHandControllerHardware RightHand { get; }
    }
}
