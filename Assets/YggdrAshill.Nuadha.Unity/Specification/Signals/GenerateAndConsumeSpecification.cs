using NUnit.Framework;
using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Signals;
using UnityEngine;

namespace YggdrAshill.Nuadha.Unity.Specification
{
    [TestFixture(TestOf = typeof(Generate))]
    [TestFixture(TestOf = typeof(Consume))]
    internal class GenerateAndConsumeSpecification
    {
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
            var consumption = Consume.Tilt(signal =>
            {
                consumed = signal;
            });

            var generation = Generate.Tilt(() => expected);

            var transmission = Propagate.WithoutCache<Tilt>().Transmit(generation);

            using (transmission.Produce(consumption).ToDisposable())
            {
                transmission.Emit();
         
                Assert.AreEqual(expected.normalized, expected.ToTilt().ToVector());
            }
        }
    }
}
