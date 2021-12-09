using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools.Utils;

namespace YggdrAshill.Nuadha.Unity.Specification
{
    [TestFixture(TestOf = typeof(SimulateSpace3D))]
    internal class SimulateSpace3DSpecification
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
            var consumption = SimulateSpace3D.ToConsumePosition(signal =>
            {
                consumed = signal;
            });

            var generation = SimulateSpace3D.ToGeneratePosition(expected);

            var emission = consumption.Conduct(generation);

            emission.Emit();

            Assert.That(consumed, Is.EqualTo(expected).Using(QuaternionEqualityComparer.Instance));
        }

        [Test]
        public void PositionShouldBeGeneratedAndConsumedWithAbsoluteCoordinate()
        {
            var one = new GameObject();
            var another = new GameObject();

            one.transform.position = new Vector3(Random.value, Random.value, Random.value);
            another.transform.position = Vector3.zero;

            var generation = SimulateSpace3D.ToGeneratePosition(one.transform);

            var consumption = SimulateSpace3D.ToConsumePosition(another.transform);

            var emission = consumption.Conduct(generation);

            emission.Emit();

            Assert.That(another.transform.position, Is.EqualTo(one.transform.position).Using(Vector3EqualityComparer.Instance));
        }

        [Test]
        public void PositionShouldBeGeneratedAndConsumedWithRelativeCoordinate()
        {
            var origin = new GameObject();
            var one = new GameObject();
            var another = new GameObject();

            origin.transform.position = Vector3.zero;
            one.transform.position = new Vector3(Random.value, Random.value, Random.value);
            another.transform.position = Vector3.zero;

            one.transform.parent = origin.transform;
            another.transform.parent = origin.transform;

            var generation = SimulateSpace3D.ToGeneratePosition(origin.transform, one.transform);

            var consumption = SimulateSpace3D.ToConsumePosition(origin.transform, another.transform);

            var emission = consumption.Conduct(generation);

            emission.Emit();

            Assert.That(another.transform.localPosition, Is.EqualTo(one.transform.localPosition).Using(Vector3EqualityComparer.Instance));
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
            var consumption = SimulateSpace3D.ToConsumeDirection(signal =>
            {
                consumed = signal;
            });

            var generation = SimulateSpace3D.ToGenerateDirection(expected);

            var emission = consumption.Conduct(generation);

            emission.Emit();

            Assert.That(consumed, Is.EqualTo(expected.normalized).Using(Vector3EqualityComparer.Instance));
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
            var consumption = SimulateSpace3D.ToConsumeRotation(signal =>
            {
                consumed = signal;
            });

            var generation = SimulateSpace3D.ToGenerateRotation(expected);

            var emission = consumption.Conduct(generation);

            emission.Emit();

            Assert.That(consumed, Is.EqualTo(expected.normalized).Using(QuaternionEqualityComparer.Instance));
        }

        [Test]
        public void RotationShouldBeGeneratedAndConsumedWithAbsoluteCoordinate()
        {
            var one = new GameObject();
            var another = new GameObject();

            one.transform.rotation = Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), new Vector3(Random.value, Random.value, Random.value));
            another.transform.rotation = Quaternion.identity;

            var generation = SimulateSpace3D.ToGenerateRotation(one.transform);

            var consumption = SimulateSpace3D.ToConsumeRotation(another.transform);

            var emission = consumption.Conduct(generation);

            emission.Emit();

            Assert.That(another.transform.rotation, Is.EqualTo(one.transform.rotation).Using(QuaternionEqualityComparer.Instance));
        }

        [Test]
        public void RotationShouldBeGeneratedAndConsumedWithRelativeCoordinate()
        {
            var origin = new GameObject();
            var one = new GameObject();
            var another = new GameObject();

            origin.transform.rotation = Quaternion.identity;
            one.transform.rotation = Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), new Vector3(Random.value, Random.value, Random.value));
            another.transform.rotation = Quaternion.identity;

            one.transform.parent = origin.transform;
            another.transform.parent = origin.transform;

            var generation = SimulateSpace3D.ToGenerateRotation(origin.transform, one.transform);

            var consumption = SimulateSpace3D.ToConsumeRotation(origin.transform, another.transform);

            var emission = consumption.Conduct(generation);

            emission.Emit();

            Assert.That(another.transform.localRotation, Is.EqualTo(one.transform.localRotation).Using(QuaternionEqualityComparer.Instance));
        }
    }
}
