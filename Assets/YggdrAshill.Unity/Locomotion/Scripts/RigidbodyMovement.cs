using YggdrAshill.Nuadha;
using YggdrAshill.Nuadha.Signals;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using YggdrAshill.Nuadha.Signalization;

namespace YggdrAshill.Unity
{
    internal static class FindCandidate
    {
        internal static INormalCandidateFinder WithCapsule(CapsuleCollider collider, float distance, LayerMask layerMask)
        {
            if (collider == null)
            {
                throw new ArgumentNullException(nameof(collider));
            }
            if (distance < 0.0f)
            {
                throw new ArgumentOutOfRangeException(nameof(distance));
            }

            return new FindCandidateWithCapsule(collider, distance, layerMask);
        }
        private sealed class FindCandidateWithCapsule :
            INormalCandidateFinder
        {
            private readonly CapsuleCollider collider;

            private readonly float distance;

            private readonly LayerMask layerMask;

            internal FindCandidateWithCapsule(CapsuleCollider collider, float distance, LayerMask layerMask)
            {
                this.collider = collider;

                this.distance = distance;

                this.layerMask = layerMask;
            }

            public IEnumerable<Vector3> Find()
            {
                var origin
                    = collider.transform.position + collider.transform.rotation * Vector3.down * collider.height * 0.5f;
                var radius = collider.radius;
                var direction = -collider.transform.up;

                var candidates
                    = Physics.SphereCastAll(origin, radius, direction, distance, layerMask)
                    .Where(hit => hit.collider != collider)
                    .Select(hit => hit.normal);

                return (candidates.Count() > 0) ? candidates : new Vector3[] { collider.transform.up };
            }
        }
    }
    internal sealed class RigidbodyMovement : LifetimeScope
    {
#pragma warning disable IDE0044
        [SerializeField] private Rigidbody targetRigidbody;
        private Rigidbody TargetRigidbody
        {
            get
            {
                if (targetRigidbody != null)
                {
                    return targetRigidbody;
                }

                if (TryGetComponent(out targetRigidbody))
                {
                    return targetRigidbody;
                }

                throw new InvalidOperationException($"{nameof(TargetRigidbody)} is null.");
            }
        }


        [SerializeField] private Handedness handedness;

        [SerializeField] private CapsuleCollider targetCollider;
        private CapsuleCollider TargetCollider
        {
            get
            {
                if (targetCollider != null)
                {
                    return targetCollider;
                }

                if (TryGetComponent(out targetCollider))
                {
                    return targetCollider;
                }

                throw new InvalidOperationException($"{nameof(TargetCollider)} is null.");
            }
        }

        [SerializeField] private float distance;

        [SerializeField] private LayerMask layerMask = ~0;

#pragma warning restore IDE0044

        protected override void Configure(IContainerBuilder builder)
        {
            switch (handedness)
            {
                case Handedness.Left:
                    builder.RegisterInstance(DeviceManagement.HeadMountedDisplay.Hardware.LeftHand.Thumb.Tilt);
                    break;
                case Handedness.Right:
                    builder.RegisterInstance(DeviceManagement.HeadMountedDisplay.Hardware.RightHand.Thumb.Tilt);
                    break;
                default:
                    builder.RegisterInstance(Propagate.WithLatestCache(() => Tilt.Origin));
                    break;
            }

            builder.RegisterInstance(FindCandidate.WithCapsule(TargetCollider, distance, layerMask));

            builder.RegisterInstance(TargetRigidbody);
            builder.RegisterInstance(Propagate.WithLatestCache(() => Space3D.Direction.Upward))
                .As<IProduction<Space3D.Direction>>()
                .As<IConsumption<Space3D.Direction>>();
            builder.RegisterEntryPoint<NormalDetectionEntryPoint>();
            builder.RegisterEntryPoint<RigidbodyMovementEntryPoint>();
        }
    }
}
