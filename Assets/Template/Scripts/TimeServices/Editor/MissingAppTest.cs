using System.IO;
using System.Threading;
using Moq;
using NUnit.Framework;
using Template.Scripts.Savable;
using UnityEngine;

namespace Template.Scripts.TimeServices.Editor
{
    public class MissingAppTest
    {
        [Test]
        public void MissingInAppTest()
        {
            var moq = new Mock<IFileInterface>();
            moq.Setup(_ => _.ReadBytes(It.IsAny<string>())).Returns(new byte[0]);

            var timeService = new TimeService();
            var path = Application.dataPath + Path.DirectorySeparatorChar + "Save1.txt";
            
            var savable = new SavableAPI(new SavableContext
            {
                SourceFilePath = path
            }, moq.Object);

            var missing = new MissingDetector(savable, timeService,1);
            
            var m2 = new Mock<IMissingHandler>();
            missing.Register(m2.Object);
            
            timeService.UpdateFromMono(1f);
            Thread.Sleep(2000);
            timeService.UpdateFromMono(1f);

            m2.Verify(_ => _.OnMissing(It.IsAny<double>()));
            
            savable.Dispose();
        }
        
    }
}
