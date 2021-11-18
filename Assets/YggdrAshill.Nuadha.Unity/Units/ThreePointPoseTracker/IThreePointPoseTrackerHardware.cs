﻿using YggdrAshill.Nuadha.Unitization;
using YggdrAshill.Nuadha.Units;

namespace YggdrAshill.Nuadha.Unity
{
    public interface IThreePointPoseTrackerHardware :
        IModule
    {
        IPoseTrackerHardware Origin { get; }

        IPoseTrackerHardware Head { get; }

        IPoseTrackerHardware LeftHand { get; }

        IPoseTrackerHardware RightHand { get; }
    }
}