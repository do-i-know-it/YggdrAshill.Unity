using YggdrAshill.Nuadha.Signalization;
using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Units;
using YggdrAshill.Nuadha.Unity;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace YggdrAshill.VContainer
{
    public sealed class TrackHeadMountedDisplayEntryPoint :
        IStartable,
        IDisposable
    {
        private readonly IHeadMountedDisplayHardware hardware;

        private readonly IPoseTrackerSoftware origin;

        private readonly IPoseTrackerSoftware head;

        private readonly IPoseTrackerSoftware leftHand;

        private readonly IPoseTrackerSoftware rightHand;

        private ICancellation cancellation;

        [Inject]
        public TrackHeadMountedDisplayEntryPoint(
            IHeadMountedDisplayHardware hardware,
            Transform origin, Transform head, Transform leftHand, Transform rightHand)
        {
            this.hardware = hardware;

            this.origin = SimulatePoseTracker.ToTrack(origin);

            this.head = SimulatePoseTracker.ToTrack(origin, head);

            this.leftHand = SimulatePoseTracker.ToTrack(origin, leftHand);

            this.rightHand = SimulatePoseTracker.ToTrack(origin, rightHand);
        }

        public void Start()
        {
            cancellation
                = CancellationSource.Default
                .Synthesize(hardware.Origin.Position.Produce(origin.Position))
                .Synthesize(hardware.Origin.Rotation.Produce(origin.Rotation))
                .Synthesize(hardware.Head.Pose.Position.Produce(head.Position))
                .Synthesize(hardware.Head.Pose.Rotation.Produce(head.Rotation))
                .Synthesize(hardware.LeftHand.Pose.Position.Produce(leftHand.Position))
                .Synthesize(hardware.LeftHand.Pose.Rotation.Produce(leftHand.Rotation))
                .Synthesize(hardware.RightHand.Pose.Position.Produce(rightHand.Position))
                .Synthesize(hardware.RightHand.Pose.Rotation.Produce(rightHand.Rotation))
                .Build();
        }

        public void Dispose()
        {
            cancellation.Cancel();

            cancellation = null;
        }
    }
}
