using YggdrAshill.Nuadha.Units;

namespace YggdrAshill.Nuadha.Unity
{
    public interface IHeadMountedDisplayPulsation
    {
        IHandControllerPulsation LeftHand { get; }

        IHandControllerPulsation RightHand { get; }
    }
}
