using NUnit.Framework;
using System;

namespace YggdrAshill.Nuadha.Unity.Specification
{
    [TestFixture(TestOf = typeof(SimulatePush))]
    internal class SimulatePushSpecification
    {
        [TestCase(true)]
        [TestCase(false)]
        public void ShouldGenerateAndConsumeSignalWithBoolean(bool expected)
        {
            var consumed = false;
            var consumption = SimulatePush.ToConsume(signal =>
            {
                consumed = signal;
            });

            var generation = SimulatePush.ToGenerate(() =>
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
                var generation = SimulatePush.ToGenerate(default(Func<bool>));
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                var consumption = SimulatePush.ToConsume(default);
            });
        }
    }
}
