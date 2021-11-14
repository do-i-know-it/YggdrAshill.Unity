using NUnit.Framework;
using YggdrAshill.Unity.Nuadha;
using UnityEngine;

namespace YggdrAshill.Unity.Specification
{
    [TestFixture(TestOf = typeof(SignalExtension))]
    internal class SignalExtensionSpecification
    {
        private static object[] TestSuiteForTiltConversion => new[]
        {
            new object[] { Vector2.zero },
            new object[] { Vector2.left },
            new object[] { Vector2.right },
            new object[] { Vector2.up },
            new object[] { Vector2.down },
            new object[] { Vector2.one },
        };
        [TestCaseSource("TestSuiteForTiltConversion")]
        public void TiltAndVector2ShouldBeConvertible(Vector2 signal)
        {
            Assert.AreEqual(signal.normalized, signal.ToTilt().ToVector());
        }
    }
}
