using NUnit.Framework;
using YggdrAshill.Nuadha;
using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Signals;
using System;
using UnityEngine;

namespace YggdrAshill.Unity.Specification
{
    [TestFixture(TestOf = typeof(Nuadha.Generate))]
    [TestFixture(TestOf = typeof(Nuadha.Consume))]
    internal class GenerateAndConsumeSpecification
    {
        [TestCase(true)]
        [TestCase(false)]
        public void TouchShouldBeGeneratedAndConsumed(bool expected)
        {
            var consumed = false;
            var consumption = Nuadha.Consume.Touch(signal =>
            {
                consumed = signal;
            });

            var generation = Nuadha.Generate.Touch(expected);

            var transmission = Propagate.WithoutCache<YggdrAshill.Nuadha.Signals.Touch>().Transmit(generation);

            using (transmission.Produce(consumption).ToDisposable())
            {
                transmission.Emit();

                Assert.AreEqual(expected, consumed);
            }
        }

        [TestCase(true)]
        [TestCase(false)]
        public void PushShouldBeGeneratedAndConsumed(bool expected)
        {
            var consumed = false;
            var consumption = Nuadha.Consume.Push(signal =>
            {
                consumed = signal;
            });

            var generation = Nuadha.Generate.Push(expected);

            var transmission = Propagate.WithoutCache<Push>().Transmit(generation);

            using (transmission.Produce(consumption).ToDisposable())
            {
                transmission.Emit();

                Assert.AreEqual(expected, consumed);
            }
        }

        [TestCase(1.0f)]
        [TestCase(0.5f)]
        [TestCase(0.0f)]
        public void PullShouldBeGeneratedAndConsumed(float expected)
        {
            var consumed = default(float);
            var consumption = Nuadha.Consume.Pull(signal =>
            {
                consumed = signal;
            });

            var generation = Nuadha.Generate.Pull(expected);

            var transmission = Propagate.WithoutCache<Pull>().Transmit(generation);

            using (transmission.Produce(consumption).ToDisposable())
            {
                transmission.Emit();

                Assert.AreEqual(expected, consumed);
            }
        }

        private static object[] TestSuiteForTilt => new[]
        {
            new object[] { Vector2.zero },
            new object[] { Vector2.left },
            new object[] { Vector2.right },
            new object[] { Vector2.up },
            new object[] { Vector2.down },
            new object[] { Vector2.one },
        };
        [TestCaseSource("TestSuiteForTilt")]
        public void TiltShouldBeGeneratedAndConsumed(Vector2 expected)
        {
            var consumed = default(Vector2);
            var consumption = Nuadha.Consume.Tilt(signal =>
            {
                consumed = signal;
            });

            var generation = Nuadha.Generate.Tilt(expected);

            var transmission = Propagate.WithoutCache<Tilt>().Transmit(generation);

            using (transmission.Produce(consumption).ToDisposable())
            {
                transmission.Emit();
         
                Assert.AreEqual(expected.normalized, consumed);
            }
        }

        [Test]
        public void CannotBeGeneratedWithNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var generation = Nuadha.Generate.Touch(default(Func<bool>));
            });
            Assert.Throws<ArgumentNullException>(() =>
            {
                var consumption = Nuadha.Consume.Touch(default);
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                var generation = Nuadha.Generate.Push(default(Func<bool>));
            });
            Assert.Throws<ArgumentNullException>(() =>
            {
                var consumption = Nuadha.Consume.Push(default);
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                var generation = Nuadha.Generate.Pull(default(Func<float>));
            });
            Assert.Throws<ArgumentNullException>(() =>
            {
                var consumption = Nuadha.Consume.Pull(default);
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                var generation = Nuadha.Generate.Tilt(default(Func<Vector2>));
            });
            Assert.Throws<ArgumentNullException>(() =>
            {
                var consumption = Nuadha.Consume.Tilt(default);
            });
        }
    }
}
