using Moq;
using NUnit.Framework;

namespace Template.Scripts.TimeServices.Editor
{
    public class TimeServiceTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void TimeServiceTestSimplePasses()
        {
            var t = new TimeService();
            var moq = new Mock<IUpdater>();
            t.Register(moq.Object);
            float d = 1;
            moq.Verify(_ => _.OnUpdate(ref d), Times.Never);
            t.UpdateFromMono(d);
            moq.Verify(_ => _.OnUpdate(ref d), Times.Once);
            t.UnRegister(moq.Object);
            t.UpdateFromMono(d);
            t.Register(moq.Object);
            t.UpdateFromMono(d);
            moq.Verify(_ => _.OnUpdate(ref d), Times.Exactly(2));
        }
        
    }
}
