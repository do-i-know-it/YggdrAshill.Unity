using NUnit.Framework;
using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Signals;
using System;

namespace YggdrAshill.Nuadha.Unity.Specification
{
    [TestFixture(TestOf = typeof(SimulateTouch))]
    internal class SimulateTouchSpecification
    {
        [TestCase(true)]
        [TestCase(false)]
        public void ShouldGenerateAndConsumeSignalWithBoolean(bool expected)
        {
            var consumed = false;
            var consumption = SimulateTouch.ToConsume(signal =>
            {
                consumed = signal;
            });

            var generation = SimulateTouch.ToGenerate(() =>
            {
                return expected;
            });

            var emission = consumption.Conduct(generation);

            emission.Emit();

            Assert.AreEqual(expected, consumed);
        }

        [Test]
        public void CannotBeGeneratedWithNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var generation = SimulateTouch.ToGenerate(default(Func<bool>));
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                var generation = SimulateTouch.ToGenerate(default(IGeneration<Push>));
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                var consumption = SimulateTouch.ToConsume(default);
            });
        }
    }
}
