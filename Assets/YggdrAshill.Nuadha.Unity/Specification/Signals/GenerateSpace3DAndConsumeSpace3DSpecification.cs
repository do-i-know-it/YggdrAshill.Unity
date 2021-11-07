using NUnit.Framework;
using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Signals;
using UnityEngine;

namespace YggdrAshill.Nuadha.Unity.Specification
{
    [TestFixture(TestOf = typeof(GenerateSpace3D))]
    [TestFixture(TestOf = typeof(ConsumeSpace3D))]
    internal class GenerateSpace3DAndConsumeSpace3DSpecification
    {
        private static object[] TestSuiteForPosition => new[]
        {
            new object[] { Vector3.zero },
            new object[] { Vector3.left },
            new object[] { Vector3.right },
            new object[] { Vector3.up },
            new object[] { Vector3.down },
            new object[] { Vector3.forward },
            new object[] { Vector3.back },
            new object[] { Vector3.one },
        };
        [TestCaseSource("TestSuiteForPosition")]
        public void PositionShouldBeGeneratedAndConsumed(Vector3 expected)
        {
            var consumed = default(Vector3);
            var consumption = ConsumeSpace3D.Position(signal =>
            {
                consumed = signal;
            });

            var generation = GenerateSpace3D.Position(() => expected);

            var transmission = Propagate.WithoutCache<Space3D.Position>().Transmit(generation);

            using (transmission.Produce(consumption).ToDisposable())
            {
                transmission.Emit();

                Assert.AreEqual(expected, expected.ToPosition().ToVector());
            }
        }

        private static object[] TestSuiteForDirection => new[]
        {
            new object[] { Vector3.left },
            new object[] { Vector3.right },
            new object[] { Vector3.up },
            new object[] { Vector3.down },
            new object[] { Vector3.forward },
            new object[] { Vector3.back },
            new object[] { Vector3.one },
        };
        [TestCaseSource("TestSuiteForDirection")]
        public void DirectionShouldBeGeneratedAndConsumed(Vector3 expected)
        {
            var consumed = default(Vector3);
            var consumption = ConsumeSpace3D.Direction(signal =>
            {
                consumed = signal;
            });

            var generation = GenerateSpace3D.Direction(() => expected);

            var transmission = Propagate.WithoutCache<Space3D.Direction>().Transmit(generation);

            using (transmission.Produce(consumption).ToDisposable())
            {
                transmission.Emit();

                Assert.AreEqual(expected.normalized, expected.ToDirection().ToVector());
            }
        }

        private static object[] TestSuiteForRotation => new[]
        {
            new object[] { Quaternion.identity },
            new object[] { Quaternion.AngleAxis(90f, Vector3.right) },
            new object[] { Quaternion.AngleAxis(90f, Vector3.up) },
            new object[] { Quaternion.AngleAxis(90f, Vector3.forward) },
        };
        [TestCaseSource("TestSuiteForRotation")]
        public void RotationShouldBeGeneratedAndConsumed(Quaternion expected)
        {
            var consumed = default(Quaternion);
            var consumption = ConsumeSpace3D.Rotation(signal =>
            {
                consumed = signal;
            });

            var generation = GenerateSpace3D.Rotation(() => expected);

            var transmission = Propagate.WithoutCache<Space3D.Rotation>().Transmit(generation);

            using (transmission.Produce(consumption).ToDisposable())
            {
                transmission.Emit();

                Assert.AreEqual(expected.normalized, expected.ToRotation().ToQuaternion());
            }
        }
    }
}
