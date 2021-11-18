namespace YggdrAshill.Nuadha.Unity
{
    public interface IHeadMountedDisplayConfiguration
    {
        IHeadTrackerConfiguration Head { get; }

        IHandControllerConfiguration LeftHand { get; }

        IHandControllerConfiguration RightHand { get; }
    }
}
