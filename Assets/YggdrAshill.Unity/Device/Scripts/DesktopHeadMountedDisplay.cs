using YggdrAshill.Nuadha;
using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Signals;
using YggdrAshill.Nuadha.Units;
using YggdrAshill.Nuadha.Unity;
using YggdrAshill.VContainer;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Touch = YggdrAshill.Nuadha.Signals.Touch;

namespace YggdrAshill.Unity
{
    [DisallowMultipleComponent]
    internal sealed class DesktopHeadMountedDisplay : LifetimeScope
    {
#pragma warning disable IDE0044

        [SerializeField] private Camera targetCamera;
        private Camera TargetCamera
        {
            get
            {
                if (targetCamera == null)
                {
                    targetCamera = Camera.main;
                }

                return targetCamera;
            }
        }

        [SerializeField] private LayerMask layerMask = ~0;

        [SerializeField] private Handedness handedness;
        internal Handedness Handedness
        {
            get
            {
                return handedness;
            }
            set
            {
                handedness = value;
                switch (handedness)
                {
                    case Handedness.Left:
                        leftThumb = SimulateStick.WASD;
                        rightThumb = SimulateStick.QE;
                        break;
                    case Handedness.Right:
                        leftThumb = SimulateStick.QE;
                        rightThumb = SimulateStick.WASD;
                        break;
                    case Handedness.None:
                        leftThumb = Imitate.Stick;
                        rightThumb = Imitate.Stick;
                        break;
                }
            }
        }

#pragma warning restore IDE0044

        private IHeadMountedDisplayConfiguration configuration;
        private IHeadMountedDisplayConfiguration Configuration
        {
            get
            {
                if (configuration == null)
                {
                    configuration = new HeadMountedDisplayConfiguration()
                    {
                        Origin = SimulatePoseTracker.ToConfigure(CameraRig.OriginTransform),
                        Head = SimulateHeadTracker.ToConfigure(CameraRig.OriginTransform, CameraRig.HeadTransform),
                        LeftHand = new HandControllerConfiguration()
                        {
                            Pose = SimulatePoseTracker.ToConfigure(CameraRig.OriginTransform, CameraRig.LeftHandTransform),
                            Thumb = new StickConfiguration()
                            {
                                Touch = Generate.Signal(LeftHandThumbTouch),
                                Tilt = Generate.Signal(LeftHandThumbTilt),
                            },
                            IndexFinger = new TriggerConfiguration()
                            {
                                Touch = SimulateTouch.ToGenerate(LeftHandIndexFingerTouch),
                                Pull = SimulatePull.ToGenerate(LeftHandIndexFingerPull),
                            },
                            HandGrip = new TriggerConfiguration()
                            {
                                Touch = SimulateTouch.ToGenerate(LeftHandHandGripTouch),
                                Pull = SimulatePull.ToGenerate(LeftHandHandGripPull),
                            },
                        },
                        RightHand = new HandControllerConfiguration()
                        {
                            Pose = SimulatePoseTracker.ToConfigure(CameraRig.OriginTransform, CameraRig.RightHandTransform),
                            Thumb = new StickConfiguration()
                            {
                                Touch = Generate.Signal(RightHandThumbTouch),
                                Tilt = Generate.Signal(RightHandThumbTilt),
                            },
                            IndexFinger = new TriggerConfiguration()
                            {
                                Touch = SimulateTouch.ToGenerate(RightHandIndexFingerTouch),
                                Pull = SimulatePull.ToGenerate(RightHandIndexFingerPull),
                            },
                            HandGrip = new TriggerConfiguration()
                            {
                                Touch = SimulateTouch.ToGenerate(RightHandHandGripTouch),
                                Pull = SimulatePull.ToGenerate(RightHandHandGripPull),
                            },
                        },
                    };
                }

                return configuration;
            }
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder
                .RegisterInstance(Configuration)
                .AsSelf();
            builder
                .RegisterInstance(DeviceManagement.HeadMountedDisplay.Software)
                .AsSelf();

            builder.RegisterEntryPoint<TransmitHeadMountedDisplayEntryPoint>();

            Handedness = Handedness;
        }

        private bool isUpdated = false;

        private void LateUpdate()
        {
            isUpdated = false;
        }

        private void UpdateIfNeeded()
        {
            if (isUpdated)
            {
                return;
            }

            var ray = TargetCamera.ScreenPointToRay(Input.mousePosition);
            var targetPosition = Physics.Raycast(ray, out var info, float.MaxValue, layerMask) ? info.point : CameraRig.OriginTransform.position;

            var mouseLeftButton = Input.GetMouseButton(0);
            var mouseRightButton = Input.GetMouseButton(1);

            switch (Handedness)
            {
                case Handedness.Left:
                    CameraRig.LeftHandTransform.LookAt(targetPosition);
                    leftHandIndexFinger = mouseRightButton;
                    leftHandHandGrip = mouseLeftButton;

                    CameraRig.RightHandTransform.rotation = Quaternion.identity;
                    rightHandIndexFinger = false;
                    rightHandHandGrip = false;

                    break;
                case Handedness.Right:
                    CameraRig.LeftHandTransform.rotation = Quaternion.identity;
                    leftHandIndexFinger = false;
                    leftHandHandGrip = false;

                    CameraRig.RightHandTransform.LookAt(targetPosition);
                    rightHandIndexFinger = mouseRightButton;
                    rightHandHandGrip = mouseLeftButton;

                    break;
                case Handedness.None:
                    CameraRig.LeftHandTransform.rotation = Quaternion.identity;
                    leftHandIndexFinger = false;
                    leftHandHandGrip = false;

                    CameraRig.RightHandTransform.rotation = Quaternion.identity;
                    rightHandIndexFinger = false;
                    rightHandHandGrip = false;

                    break;
            }

            // todo: skip update signal when operating uGUI.
            // todo: left and right thumb sticks update.
            // todo: rotation of camera and avatar.
            // todo: change TPS or FPS.

            isUpdated = true;
        }

        private IStickConfiguration leftThumb;
        private Touch LeftHandThumbTouch()
        {
            return leftThumb.Touch.Generate();
        }

        private Tilt LeftHandThumbTilt()
        {
            return leftThumb.Tilt.Generate();
        }

        private bool leftHandIndexFinger;
        private bool LeftHandIndexFingerTouch()
        {
            UpdateIfNeeded();

            return leftHandIndexFinger;
        }

        private bool LeftHandIndexFingerPull()
        {
            UpdateIfNeeded();

            return leftHandIndexFinger;
        }

        private bool leftHandHandGrip;
        private bool LeftHandHandGripTouch()
        {
            UpdateIfNeeded();

            return leftHandHandGrip;
        }

        private bool LeftHandHandGripPull()
        {
            UpdateIfNeeded();

            return leftHandHandGrip;
        }

        private IStickConfiguration rightThumb;
        private Touch RightHandThumbTouch()
        {
            return rightThumb.Touch.Generate();
        }

        private Tilt RightHandThumbTilt()
        {
            return rightThumb.Tilt.Generate();
        }

        private bool rightHandIndexFinger;
        private bool RightHandIndexFingerTouch()
        {
            UpdateIfNeeded();

            return rightHandIndexFinger;
        }

        private bool RightHandIndexFingerPull()
        {
            UpdateIfNeeded();

            return rightHandIndexFinger;
        }

        private bool rightHandHandGrip;
        private bool RightHandHandGripTouch()
        {
            UpdateIfNeeded();

            return rightHandHandGrip;
        }

        private bool RightHandHandGripPull()
        {
            UpdateIfNeeded();

            return rightHandHandGrip;
        }

        private sealed class StickConfiguration :
            IStickConfiguration
        {
            public IGeneration<Touch> Touch { get; set; }

            public IGeneration<Tilt> Tilt { get; set; }
        }

        private sealed class TriggerConfiguration :
            ITriggerConfiguration
        {
            public IGeneration<Touch> Touch { get; set; }

            public IGeneration<Pull> Pull { get; set; }
        }

        private sealed class HandControllerConfiguration :
            IHandControllerConfiguration
        {
            public IPoseTrackerConfiguration Pose { get; set; }

            public IStickConfiguration Thumb { get; set; }

            public ITriggerConfiguration IndexFinger { get; set; }

            public ITriggerConfiguration HandGrip { get; set; }
        }

        private sealed class HeadMountedDisplayConfiguration :
            IHeadMountedDisplayConfiguration
        {
            public IPoseTrackerConfiguration Origin { get; set; }

            public IHeadTrackerConfiguration Head { get; set; }

            public IHandControllerConfiguration LeftHand { get; set; }

            public IHandControllerConfiguration RightHand { get; set; }
        }
    }
}
