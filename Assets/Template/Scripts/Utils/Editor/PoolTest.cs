using NUnit.Framework;

namespace Template.Scripts.Utils.Editor
{
    public class PoolTest
    {
        [Test]
        public void PoolTestBasic()
        {
            var moq = Pool<TestClass>.GetFromPool(() => new TestClass());
            Assert.IsTrue(moq.IsCalledEnter == false);
            Assert.IsTrue(moq.IsCalledExit == false);
        }
        
        [Test]
        public void PoolTestEnter()
        {
            var r = new TestClass();
            Pool<TestClass>.SetInPool(r);
            Assert.IsTrue(r.IsCalledEnter == true);
            Assert.IsTrue(r.IsCalledExit == false);
        }
        
        [Test]
        public void PoolTestExit()
        {
            var r = new TestClass();
            Pool<TestClass>.SetInPool(r);
            r = Pool<TestClass>.GetFromPool(() => new TestClass());
            Assert.IsTrue(r.IsCalledEnter == true);
            Assert.IsTrue(r.IsCalledExit == true);
        }

        [Test]
        public void ClearAll()
        {
            var r = new TestClass();
            Pool<TestClass>.SetInPool(r);
            Pool<TestClass>.ForceClearAll();
            var r2 = Pool<TestClass>.GetFromPool(() => new TestClass());
            Assert.AreNotEqual(r, r2);
        }
        
        public class TestClass : IPoolObject
        {
            public bool IsCalledEnter;
            public bool IsCalledExit;
            
            public void OnEnterPool()
            {
                IsCalledEnter = true;
            }

            public void OnExitPool()
            {
                IsCalledExit = true;
            }
        }

    }
}
