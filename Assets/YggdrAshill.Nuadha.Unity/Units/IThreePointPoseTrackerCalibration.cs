namespace YggdrAshill.Nuadha.Unity
{
    public interface IThreePointPoseTrackerCalibration
    {
        IPoseTrackerConfiguration Head { get; }

        IPoseTrackerConfiguration LeftHand { get; }

        IPoseTrackerConfiguration RightHand { get; }
    }
}
