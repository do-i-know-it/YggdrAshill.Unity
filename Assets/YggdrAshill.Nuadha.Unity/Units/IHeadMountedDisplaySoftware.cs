using YggdrAshill.Nuadha.Unitization;
using YggdrAshill.Nuadha.Units;

namespace YggdrAshill.Nuadha.Unity
{
    public interface IHeadMountedDisplaySoftware :
        IModule
    {
        IPoseTrackerSoftware Origin { get; }

        IHeadTrackerSoftware Head { get; }

        IHandControllerSoftware LeftHand { get; }

        IHandControllerSoftware RightHand { get; }
    }
}
