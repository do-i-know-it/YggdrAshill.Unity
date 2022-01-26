using NUnit.Framework;
using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Signals;
using System;

namespace YggdrAshill.Nuadha.Unity.Specification
{
    [TestFixture(TestOf = typeof(SimulatePull))]
    internal class SimulatePullSpecification
    {
        [TestCase(1.0f)]
        [TestCase(0.5f)]
        [TestCase(0.0f)]
        public void ShouldGenerateAndConsumeSignalWithSingle(float expected)
        {
            var consumed = default(float);
            var consumption = SimulatePull.ToConsume(signal =>
            {
                consumed = signal;
            });

            var generation = SimulatePull.ToGenerate(() =>
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
                var generation = SimulatePull.ToGenerate(default(Func<float>));
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                var generation = SimulatePull.ToGenerate(default(Func<bool>));
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                var generation = SimulatePull.ToGenerate(default(IGeneration<Push>));
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                var consumption = SimulatePull.ToConsume(default);
            });
        }
    }
}
