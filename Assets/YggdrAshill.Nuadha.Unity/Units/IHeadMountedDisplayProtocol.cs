using YggdrAshill.Nuadha.Unitization;
using YggdrAshill.Nuadha.Units;

namespace YggdrAshill.Nuadha.Unity
{
    public interface IHeadMountedDisplayProtocol :
        IModule,
        IProtocol<IHeadMountedDisplayHardware, IHeadMountedDisplaySoftware>
    {
        IPoseTrackerProtocol Origin { get; }

        IHeadTrackerProtocol Head { get; }

        IHandControllerProtocol LeftHand { get; }

        IHandControllerProtocol RightHand { get; }
    }
}
