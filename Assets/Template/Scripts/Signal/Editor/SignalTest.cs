using NUnit.Framework;

namespace Template.Scripts.Signal.Editor
{
    public class SignalTest
    {
        [Test]
        public void SignalBusTest()
        {
            bool isCalled = false;
            var bus = new SignalBus();
            bus.Sub<DummyStruct>(CallBack);

            void CallBack(DummyStruct obj)
            {
                isCalled = true;
            }
            
            bus.Fire(new DummyStruct());
            Assert.IsTrue(isCalled);
        }
        
        [Test]
        public void SignalBusTestRemove()
        {
            bool isCalled = false;
            var bus = new SignalBus();
            bus.Sub<DummyStruct>(CallBack);

            void CallBack(DummyStruct obj)
            {
                isCalled = true;
            }
            
            bus.UnSub<DummyStruct>(CallBack);
            bus.Fire(new DummyStruct());
            Assert.IsFalse(isCalled);
        }
        
                
        [Test]
        public void SignalBusTestClear()
        {
            bool isCalled = false;
            var bus = new SignalBus();
            bus.Sub<DummyStruct>(CallBack);

            void CallBack(DummyStruct obj)
            {
                isCalled = true;
            }
            
            bus.Clear();
            bus.Fire(new DummyStruct());
            Assert.IsFalse(isCalled);
        }


   
    }
    
    
    internal class DummyClass
    {
        
    }
    
    public struct DummyStruct
    {
        
    }

}
