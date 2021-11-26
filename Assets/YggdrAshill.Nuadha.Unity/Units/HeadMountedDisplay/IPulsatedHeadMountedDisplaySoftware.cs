using YggdrAshill.Nuadha.Unitization;
using YggdrAshill.Nuadha.Units;

namespace YggdrAshill.Nuadha.Unity
{
    public interface IPulsatedHeadMountedDisplaySoftware :
        IModule
    {
        IPulsatedHandControllerSoftware LeftHand { get; }

        IPulsatedHandControllerSoftware RightHand { get; }
    }
}
