using NUnit.Framework;
using System;
using UnityEngine;

namespace YggdrAshill.Nuadha.Unity.Specification
{
    [TestFixture(TestOf = typeof(Generate))]
    [TestFixture(TestOf = typeof(Consume))]
    internal class GenerateAndConsumeSpecification
    {
        [TestCase(true)]
        [TestCase(false)]
        public void TouchShouldBeGeneratedAndConsumed(bool expected)
        {
            var consumed = false;
            var consumption = Consume.Touch(signal =>
            {
                consumed = signal;
            });

            var generation = Generate.Touch(expected);

            var emission = consumption.Conduct(generation);

            emission.Emit();

            Assert.AreEqual(expected, consumed);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void PushShouldBeGeneratedAndConsumed(bool expected)
        {
            var consumed = false;
            var consumption = Consume.Push(signal =>
            {
                consumed = signal;
            });

            var generation = Generate.Push(expected);

            var emission = consumption.Conduct(generation);

            emission.Emit();

            Assert.AreEqual(expected, consumed);
        }

        [TestCase(1.0f)]
        [TestCase(0.5f)]
        [TestCase(0.0f)]
        public void PullShouldBeGeneratedAndConsumed(float expected)
        {
            var consumed = default(float);
            var consumption = Consume.Pull(signal =>
            {
                consumed = signal;
            });

            var generation = Generate.Pull(expected);

            var emission = consumption.Conduct(generation);

            emission.Emit();

            Assert.AreEqual(expected, consumed);
        }

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
            var consumption = Consume.Tilt(signal =>
            {
                consumed = signal;
            });

            var generation = Generate.Tilt(raw);

            var emission = consumption.Conduct(generation);

            emission.Emit();

            Assert.AreEqual(expected.normalized, consumed);
        }

        [Test]
        public void CannotBeGeneratedWithNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var generation = Generate.Touch(default(Func<bool>));
            });
            Assert.Throws<ArgumentNullException>(() =>
            {
                var consumption = Consume.Touch(default);
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                var generation = Generate.Push(default(Func<bool>));
            });
            Assert.Throws<ArgumentNullException>(() =>
            {
                var consumption = Consume.Push(default);
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                var generation = Generate.Pull(default(Func<float>));
            });
            Assert.Throws<ArgumentNullException>(() =>
            {
                var consumption = Consume.Pull(default);
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                var generation = Generate.Tilt(default(Func<Vector2>));
            });
            Assert.Throws<ArgumentNullException>(() =>
            {
                var consumption = Consume.Tilt(default);
            });
        }
    }
}
