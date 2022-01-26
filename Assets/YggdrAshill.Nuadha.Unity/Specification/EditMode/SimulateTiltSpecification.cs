using NUnit.Framework;
using System;
using UnityEngine;

namespace YggdrAshill.Nuadha.Unity.Specification
{
    [TestFixture(TestOf = typeof(SimulateTilt))]
    internal class SimulateTiltSpecification
    {
        private static object[] TestSuiteForTilt => new[]
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
        [TestCaseSource("TestSuiteForTilt")]
        public void TiltShouldBeGeneratedAndConsumed(Vector2 raw, Vector2 expected)
        {
            var consumed = default(Vector2);
            var consumption = SimulateTilt.ToConsume(signal =>
            {
                consumed = signal;
            });

            var generation = SimulateTilt.ToGenerate(raw);

            var emission = consumption.Conduct(generation);

            emission.Emit();

            Assert.AreEqual(expected, consumed);
        }

        [Test]
        public void CannotBeGeneratedWithNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var generation = SimulateTilt.ToGenerate(default(Func<Vector2>));
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                var consumption = SimulateTilt.ToConsume(default);
            });
        }
    }
}
