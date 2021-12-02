using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools.Utils;

namespace YggdrAshill.Nuadha.Unity.Specification
{
    [TestFixture(TestOf = typeof(TiltExtension))]
    internal class TiltExtensionSpecification
    {
        private static object[] TestSuiteForTiltConversion => new[]
        {
            new object[] { Vector2.zero, Vector2.zero },
            new object[] { Vector2.left, Vector2.left },
            new object[] { Vector2.right, Vector2.right },
            new object[] { Vector2.up, Vector2.up },
            new object[] { Vector2.down, Vector2.down },
            new object[] { Vector2.one, Vector2.one * Mathf.Sqrt(0.5f) },
            new object[] { new Vector2(1.0f, Mathf.Sqrt(3)), new Vector2(1.0f, Mathf.Sqrt(3)) * 0.5f },
            new object[] { new Vector2(Mathf.Sqrt(3), 1.0f), new Vector2(Mathf.Sqrt(3), 1.0f) * 0.5f },
        };
        [TestCaseSource("TestSuiteForTiltConversion")]
        public void TiltAndVector2ShouldBeConvertible(Vector2 raw, Vector2 expected)
        {
            Assert.That(raw.ToTilt().ToVector(), Is.EqualTo(expected).Using(Vector2EqualityComparer.Instance));
        }

        [Test]
        public void Vector2ShouldBeConvertIntoTiltWithoutException()
        {
            var offset = Random.Range(-0.1f, 0.1f);

            Assert.DoesNotThrow(() =>
            {
                var signal = (Vector2.right + Vector2.up * offset).ToTilt();
            });

            Assert.DoesNotThrow(() =>
            {
                var signal = (Vector2.up + Vector2.right * offset).ToTilt();
            });
        }
    }
}
