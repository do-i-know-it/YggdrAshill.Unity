using NUnit.Framework;
using System;
using UnityEngine;

namespace YggdrAshill.Unity.Specification
{
    [TestFixture(TestOf = typeof(Singleton<>))]
    internal class SingletonSpecification
    {
        [Test]
        public void ShouldBeReferencedAfterGenerated()
        {
            var gameobject = new GameObject();

            gameobject.AddComponent<FakeSingleton>();

            Assert.DoesNotThrow(() =>
            {
                var singleton = FakeSingleton.Instance;
            });
        }

        [Test]
        public void CannotBeReferencedBeforeGenerated()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                var singleton = FakeSingleton.Instance;
            });
        }
    }
}
