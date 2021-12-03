using YggdrAshill.Nuadha.Unity;
using YggdrAshill.Unity;
using YggdrAshill.VContainer;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace YggdrAshill.Samples
{
    [DisallowMultipleComponent]
    internal sealed class FakeHumanPoseTracker : LifetimeScope
    {
        [SerializeField] private Transform originTransform;
        private Transform OriginTransform
        {
            get
            {
                if (originTransform is null)
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
                if (headTransform is null)
                {
                    headTransform = transform;
                }

                return headTransform;
            }
        }

        [SerializeField] private Transform leftHandTransform;
        private Transform LeftHandTransform
        {
            get
            {
                if (leftHandTransform is null)
                {
                    leftHandTransform = transform;
                }

                return leftHandTransform;
            }
        }

        [SerializeField] private Transform rightTransform;
        private Transform RightHandTransform
        {
            get
            {
                if (rightTransform is null)
                {
                    rightTransform = transform;
                }

                return rightTransform;
            }
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder
                .RegisterInstance(SimulatedHumanPoseTracker.Transform(OriginTransform, HeadTransform, LeftHandTransform, RightHandTransform))
                .AsSelf();
            builder
                .RegisterInstance(DeviceManagement.HumanPoseTracker.Software)
                .AsSelf();

            builder.RegisterEntryPoint<TransmitHumanPoseTrackerEntryPoint>();
        }

        [SerializeField] private float anglePerSecond = 45;

        [SerializeField] private float radius = 1.0f;

        private float angle;

        private float theta;

        private Vector3 origin;

        private void Start()
        {
            var x = Random.Range(-2.0f, 2.0f);

            var y = OriginTransform.position.y;

            var z = Random.Range(0.0f, 2.0f);

            origin = new Vector3(x, y, z);
        }

        private void Update()
        {
            angle += anglePerSecond * Time.deltaTime;

            theta = angle * Mathf.Deg2Rad;

            OriginTransform.position = origin + new Vector3(Mathf.Cos(theta), 0.0f, Mathf.Sin(theta)) * radius;

            OriginTransform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        }
    }
}
