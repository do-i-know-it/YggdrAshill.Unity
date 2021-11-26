using YggdrAshill.Nuadha.Unitization;
using YggdrAshill.Nuadha.Units;

namespace YggdrAshill.Nuadha.Unity
{
    public interface IPulsatedHeadMountedDisplayHardware :
        IModule
    {
        IPulsatedHandControllerHardware LeftHand { get; }

        IPulsatedHandControllerHardware RightHand { get; }
    }
}
