using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Signals;
using YggdrAshill.Nuadha.Units;
using YggdrAshill.Nuadha.Unity;
using YggdrAshill.VContainer;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

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
            get => handedness;
            set => handedness = value;
        }

        [SerializeField] private Transform originTransform;
        private Transform OriginTransform
        {
            get
            {
                if (originTransform == null)
                {
                    originTransform = transform;
                }

                return originTransform;
            }
        }

        [SerializeField] private Transform headTransform;
        private Transform HeadTransform
        {
            get
            {
                if (headTransform == null)
                {
                    throw new InvalidOperationException($"{nameof(HeadTransform)} is null.");
                }

                if (headTransform == OriginTransform)
                {
                    throw new InvalidOperationException($"{nameof(HeadTransform)} is same as {nameof(OriginTransform)}.");
                }

                return headTransform;
            }
        }

        [SerializeField] private Transform leftHandTransform;
        private Transform LeftHandTransform
        {
            get
            {
                if (leftHandTransform == null)
                {
                    throw new InvalidOperationException($"{nameof(LeftHandTransform)} is null.");
                }

                if (leftHandTransform == OriginTransform)
                {
                    throw new InvalidOperationException($"{nameof(LeftHandTransform)} is same as {nameof(OriginTransform)}.");
                }

                return leftHandTransform;
            }
        }

        [SerializeField] private Transform rightTransform;
        private Transform RightHandTransform
        {
            get
            {
                if (rightTransform == null)
                {
                    throw new InvalidOperationException($"{nameof(RightHandTransform)} is null.");
                }

                if (rightTransform == OriginTransform)
                {
                    throw new InvalidOperationException($"{nameof(RightHandTransform)} is same as {nameof(OriginTransform)}.");
                }

                return rightTransform;
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
                        Origin = SimulatePoseTracker.ToConfigure(OriginTransform),
                        Head = SimulateHeadTracker.ToConfigure(OriginTransform, HeadTransform),
                        LeftHand = new HandControllerConfiguration()
                        {
                            Pose = SimulatePoseTracker.ToConfigure(OriginTransform, LeftHandTransform),
                            Thumb = new StickConfiguration()
                            {
                                Touch = SimulateTouch.ToGenerate(LeftHandThumbTouch),
                                Tilt = SimulateTilt.ToGenerate(LeftHandThumbTilt),
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
                            Pose = SimulatePoseTracker.ToConfigure(OriginTransform, RightHandTransform),
                            Thumb = new StickConfiguration()
                            {
                                Touch = SimulateTouch.ToGenerate(RightHandThumbTouch),
                                Tilt = SimulateTilt.ToGenerate(RightHandThumbTilt),
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
            var targetPosition = Physics.Raycast(ray, out var info, float.MaxValue, layerMask) ? info.point : OriginTransform.position;

            var mouseLeftButton = Input.GetMouseButton(0);
            var mouseRightButton = Input.GetMouseButton(1);

            switch (Handedness)
            {
                case Handedness.Left:
                    LeftHandTransform.LookAt(targetPosition);
                    leftHandIndexFinger = mouseRightButton;
                    leftHandHandGrip = mouseLeftButton;

                    RightHandTransform.rotation = Quaternion.identity;
                    rightHandIndexFinger = false;
                    rightHandHandGrip = false;

                    break;
                case Handedness.Right:
                    LeftHandTransform.rotation = Quaternion.identity;
                    leftHandIndexFinger = false;
                    leftHandHandGrip = false;

                    RightHandTransform.LookAt(targetPosition);
                    rightHandIndexFinger = mouseRightButton;
                    rightHandHandGrip = mouseLeftButton;

                    break;
                case Handedness.None:
                    LeftHandTransform.rotation = Quaternion.identity;
                    leftHandIndexFinger = false;
                    leftHandHandGrip = false;

                    RightHandTransform.rotation = Quaternion.identity;
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

        private bool leftHandThumbTouch;
        private bool LeftHandThumbTouch()
        {
            UpdateIfNeeded();

            return leftHandThumbTouch;
        }

        private Vector2 leftHandThumbTilt;
        private Vector2 LeftHandThumbTilt()
        {
            UpdateIfNeeded();

            return leftHandThumbTilt;
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

        private bool rightHandThumbTouch;
        private bool RightHandThumbTouch()
        {
            UpdateIfNeeded();

            return rightHandThumbTouch;
        }

        private Vector2 rightHandThumbTilt;
        private Vector2 RightHandThumbTilt()
        {
            UpdateIfNeeded();

            return rightHandThumbTilt;
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
            public IGeneration<Nuadha.Signals.Touch> Touch { get; set; }

            public IGeneration<Tilt> Tilt { get; set; }
        }

        private sealed class TriggerConfiguration :
            ITriggerConfiguration
        {
            public IGeneration<Nuadha.Signals.Touch> Touch { get; set; }

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
