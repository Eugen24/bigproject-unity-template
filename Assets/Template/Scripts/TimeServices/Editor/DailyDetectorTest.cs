using System;
using System.IO;
using Moq;
using NUnit.Framework;
using Template.Scripts.Savable;
using UnityEngine;

namespace Template.Scripts.TimeServices.Editor
{
    public class DailyDetectorTest 
    {
        [Test]
        public void SimpleTest()
        {
            var moq = new Mock<IFileInterface>();
            moq.Setup(_ => _.ReadBytes(It.IsAny<string>())).Returns(new byte[0]);

            var path = Application.dataPath + Path.DirectorySeparatorChar + "Save1.txt";
            
            var savable = new SavableAPI(new SavableContext
            {
                SourceFilePath = path
            }, moq.Object);
            
            var d = new DailyDetector(savable, 1);
            var now = new System.DateTime(2005, 3, 2, 4, 4, 4);
            Assert.AreEqual(0, d.GetCurrentDay(now));
            d.SetCurrentDay(now);
            for (int i = 1; i < 20; i++)
            {
                now = now.Add(TimeSpan.FromDays(1));
                Assert.AreEqual(i, d.GetCurrentDay(now));
            }
        }
    }
}
