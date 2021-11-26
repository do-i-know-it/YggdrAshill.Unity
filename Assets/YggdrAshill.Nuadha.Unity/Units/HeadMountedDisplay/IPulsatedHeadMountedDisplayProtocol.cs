using YggdrAshill.Nuadha.Unitization;
using YggdrAshill.Nuadha.Units;

namespace YggdrAshill.Nuadha.Unity
{
    public interface IPulsatedHeadMountedDisplayProtocol :
        IProtocol<IPulsatedHeadMountedDisplayHardware, IPulsatedHeadMountedDisplaySoftware>
    {
        IPulsatedHandControllerProtocol LeftHand { get; }

        IPulsatedHandControllerProtocol RightHand { get; }
    }
}
