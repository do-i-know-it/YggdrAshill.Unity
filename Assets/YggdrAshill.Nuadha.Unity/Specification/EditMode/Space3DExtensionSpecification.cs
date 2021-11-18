using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools.Utils;

namespace YggdrAshill.Nuadha.Unity.Specification
{
    [TestFixture(TestOf = typeof(Space3DExtension))]
    internal class Space3DExtensionSpecification
    {
        private static object[] TestSuiteForPositionConversion => new[]
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
        [TestCaseSource("TestSuiteForPositionConversion")]
        public void PositionAndVector3ShouldBeConvertible(Vector3 signal)
        {
            Assert.AreEqual(signal, signal.ToPosition().ToVector());
        }

        private static object[] TestSuiteForDirectionConversion => new[]
        {
            new object[] { Vector3.left },
            new object[] { Vector3.right },
            new object[] { Vector3.up },
            new object[] { Vector3.down },
            new object[] { Vector3.forward },
            new object[] { Vector3.back },
            new object[] { Vector3.one },
        };
        [TestCaseSource("TestSuiteForDirectionConversion")]
        public void DirectionAndVector3ShouldBeConvertible(Vector3 signal)
        {
            Assert.AreEqual(signal.normalized, signal.ToDirection().ToVector());
        }

        private static object[] TestSuiteForRotationConversion => new[]
        {
            new object[] { Quaternion.identity },
            new object[] { Quaternion.AngleAxis(90f, Vector3.right) },
            new object[] { Quaternion.AngleAxis(90f, Vector3.up) },
            new object[] { Quaternion.AngleAxis(90f, Vector3.forward) },
        };
        [TestCaseSource("TestSuiteForRotationConversion")]
        public void RotationAndQuaternionShouldBeConvertible(Quaternion signal)
        {
            Assert.That(signal.ToRotation().ToQuaternion(), Is.EqualTo(signal).Using(QuaternionEqualityComparer.Instance));
        }

        private static object[] TestSuiteForPositionCalculation => new[]
        {
            new object[] { Vector3.zero, Vector3.zero },
            new object[] { Vector3.zero, Vector3.one },
            new object[] { Vector3.one, Vector3.zero },
            new object[] { Vector3.one, Vector3.one },
        };
        [TestCaseSource("TestSuiteForPositionCalculation")]
        public void Vector3ShouldBeCalculatedInSpace3DPosition(Vector3 left, Vector3 right)
        {
            Assert.That((left.ToPosition() + right.ToPosition()).ToVector(), Is.EqualTo(left + right).Using(Vector3EqualityComparer.Instance));
        }

        private static object[] TestSuiteForRotationCalculation => new[]
        {
            new object[] { Quaternion.identity, Quaternion.identity },
            new object[] { Quaternion.identity, Quaternion.AngleAxis(90f, Vector3.right) },
            new object[] { Quaternion.identity, Quaternion.AngleAxis(90f, Vector3.up) },
            new object[] { Quaternion.identity, Quaternion.AngleAxis(90f, Vector3.forward) },
        };
        [TestCaseSource("TestSuiteForRotationCalculation")]
        public void QuaternionShouldBeCalculatedInSpace3DRotation(Quaternion left, Quaternion right)
        {
            Assert.That((left.ToRotation() + right.ToRotation()).ToQuaternion(), Is.EqualTo(left * right).Using(QuaternionEqualityComparer.Instance));
        }
    }
}
