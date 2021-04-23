using System;
using System.IO;
using System.Threading;
using Moq;
using NUnit.Framework;
using UnityEngine;

namespace Template.Scripts.Savable.Editor
{
    public class SavableSimpleTest
    {
        private Mock<IFileInterface> _moq;
        private SavableAPI _savable;
        [SetUp]
        public void Setup()
        {
            _moq = new Mock<IFileInterface>();
            _moq.Setup(_ => _.ReadBytes(It.IsAny<string>())).Returns(new byte[0]);

            var path = Application.dataPath + Path.DirectorySeparatorChar + "Save1.txt";
            
            _savable = new SavableAPI(new SavableContext
            {
                SourceFilePath = path
            }, _moq.Object);
        }
        
        [Test]
        public void SavableStringTest()
        {
            _savable.GetString(1, "string");
            _savable.GetString(2, "string2");
            _savable.GetString(200, "string3");
            
            _savable.SaveString(1, "string");
            _savable.SaveString(2, "string2");
            _savable.SaveString(200, "string3");

            Assert.IsTrue(_savable.GetString(1) == "string");
            Assert.IsTrue(_savable.GetString(2) == "string2");
            Assert.IsTrue(_savable.GetString(200) == "string3");
            Assert.IsTrue(_savable.GetString(400, "string4") == "string4");
            
            _savable.SaveString(1, "_string");
            _savable.SaveString(2, "_string2");
            _savable.SaveString(200, "_string3");
            _savable.SaveString(400, "_string4");
            
            Assert.IsTrue(_savable.GetString(1) == "_string");
            Assert.IsTrue(_savable.GetString(2) == "_string2");
            Assert.IsTrue(_savable.GetString(200) == "_string3");
            Assert.IsTrue(_savable.GetString(400, "string4") == "_string4");
            Thread.Sleep(1000);
            _moq.Verify(_ => _.WriteBytes(It.IsAny<string>(), It.IsAny<byte[]>()));
            _savable.Dispose();
        }
        
        [Test]
        public void SavableStringTest_Delete()
        {
            _savable.GetString(1, "string");
            _savable.GetString(2, "string2");
            _savable.GetString(3, "string2");
            
            _savable.DeleteString(2);

            var rez = _savable.GetString(2, "asd");
            Assert.IsTrue(rez == "asd");
            rez = _savable.GetString(1, "asd");
            Assert.IsTrue(rez == "string");
            
            _savable.DeleteString(3);
            rez = _savable.GetString(3, "123");
            Assert.IsTrue(rez == "123");
            
            _savable.Dispose();
        }

        
        [Test]
        public void SavableIntsTest()
        {
            _savable.GetInt(1, -100);
            _savable.GetInt(2, 200);
            _savable.GetInt(200, 2);
            
            _savable.SaveInt(1, -100);
            _savable.SaveInt(2, 200);
            _savable.SaveInt(200, 2);

            Assert.IsTrue(_savable.GetInt(1) == -100);
            Assert.IsTrue(_savable.GetInt(2) == 200);
            Assert.IsTrue(_savable.GetInt(200) == 2);
            Assert.IsTrue(_savable.GetInt(400, 4) == 4);
            Assert.IsTrue(_savable.GetInt(401, -4) == -4);
            
            _savable.SaveInt(1, -4);
            _savable.SaveInt(2, 5);
            _savable.SaveInt(200, 6);
            _savable.SaveInt(400, 7);
            _savable.SaveInt(401, -7);
            
            Assert.IsTrue(_savable.GetInt(1) == -4);
            Assert.IsTrue(_savable.GetInt(2) == 5);
            Assert.IsTrue(_savable.GetInt(200) == 6);
            Assert.IsTrue(_savable.GetInt(400, 345) == 7);
            Assert.IsTrue(_savable.GetInt(401, -345) == -7);
            Thread.Sleep(1000);
            _moq.Verify(_ => _.WriteBytes(It.IsAny<string>(), It.IsAny<byte[]>()));
            _savable.Dispose();

        }
        
        
        [Test]
        public void SavableIntsTest_Delete()
        {
            _savable.GetInt(1, 123);
            _savable.GetInt(2, 345);
            _savable.GetInt(3, 678);
            
            _savable.DeleteInt(2);

            var rez = _savable.GetInt(2, 8);
            Assert.IsTrue(rez == 8);
            rez = _savable.GetInt(1, 8);
            Assert.IsTrue(rez == 123);
            
            _savable.DeleteInt(3);
            rez = _savable.GetInt(3, 0);
            Assert.IsTrue(rez == 0);
            
            _savable.Dispose();
        }
        
        [Test]
        public void SavableUIntsTest()
        {
            _savable.GetUInt(1, 100);
            _savable.GetUInt(2, 200);
            _savable.GetUInt(200, 2);
            
            _savable.SaveUInt(1, 100);
            _savable.SaveUInt(2, 200);
            _savable.SaveUInt(200, 2);

            Assert.IsTrue(_savable.GetUInt(1) == 100);
            Assert.IsTrue(_savable.GetUInt(2) == 200);
            Assert.IsTrue(_savable.GetUInt(200) == 2);
            Assert.IsTrue(_savable.GetUInt(400, 4) == 4);
            
            _savable.SaveUInt(1, 4);
            _savable.SaveUInt(2, 5);
            _savable.SaveUInt(200, 6);
            _savable.SaveUInt(400, 7);
            
            Assert.IsTrue(_savable.GetUInt(1) == 4);
            Assert.IsTrue(_savable.GetUInt(2) == 5);
            Assert.IsTrue(_savable.GetUInt(200) == 6);
            Assert.IsTrue(_savable.GetUInt(400, 345) == 7);
            Thread.Sleep(1000);
            _moq.Verify(_ => _.WriteBytes(It.IsAny<string>(), It.IsAny<byte[]>()));
            _savable.Dispose();

        }
        
        [Test]
        public void SavableUIntsTest_Delete()
        {
            _savable.GetUInt(1, 123);
            _savable.GetUInt(2, 345);
            _savable.GetUInt(3, 678);
            
            _savable.DeleteUInt(2);

            var rez = _savable.GetUInt(2, 8);
            Assert.IsTrue(rez == 8);
            rez = _savable.GetUInt(1, 8);
            Assert.IsTrue(rez == 123);
            
            _savable.DeleteUInt(3);
            rez = _savable.GetUInt(3, 0);
            Assert.IsTrue(rez == 0);
            
            _savable.Dispose();
        }

        
        [Test]
        public void SavableLongTest()
        {
            _savable.GetLong(1, -100);
            _savable.GetLong(2, 200);
            _savable.GetLong(200, 2);
            
            _savable.SaveLong(1, -100);
            _savable.SaveLong(2, 200);
            _savable.SaveLong(200, 2);

            Assert.IsTrue(_savable.GetLong(1) == -100);
            Assert.IsTrue(_savable.GetLong(2) == 200);
            Assert.IsTrue(_savable.GetLong(200) == 2);
            Assert.IsTrue(_savable.GetLong(400, 4) == 4);
            Assert.IsTrue(_savable.GetLong(401, -4) == -4);
            
            _savable.SaveLong(1, -4);
            _savable.SaveLong(2, 5);
            _savable.SaveLong(200, 6);
            _savable.SaveLong(400, 7);
            _savable.SaveLong(401, -7);
            
            Assert.IsTrue(_savable.GetLong(1) == -4);
            Assert.IsTrue(_savable.GetLong(2) == 5);
            Assert.IsTrue(_savable.GetLong(200) == 6);
            Assert.IsTrue(_savable.GetLong(400, 345) == 7);
            Assert.IsTrue(_savable.GetLong(401, -345) == -7);
            Thread.Sleep(1000);
            _moq.Verify(_ => _.WriteBytes(It.IsAny<string>(), It.IsAny<byte[]>()));
            _savable.Dispose();

        }
        
        [Test]
        public void SavableLongTest_Delete()
        {
            _savable.GetLong(1, 123);
            _savable.GetLong(2, 345);
            _savable.GetLong(3, 678);
            
            _savable.DeleteLong(2);

            var rez = _savable.GetLong(2, 8);
            Assert.IsTrue(rez == 8);
            rez = _savable.GetLong(1, 8);
            Assert.IsTrue(rez == 123);
            
            _savable.DeleteLong(3);
            rez = _savable.GetLong(3, 0);
            Assert.IsTrue(rez == 0);
            
            _savable.Dispose();
        }

        
        [Test]
        public void SavableULongTest()
        {
            _savable.GetULong(1, 100);
            _savable.GetULong(2, 200);
            _savable.GetULong(200, 2);
            
            _savable.SaveULong(1, 100);
            _savable.SaveULong(2, 200);
            _savable.SaveULong(200, 2);

            Assert.IsTrue(_savable.GetULong(1) == 100);
            Assert.IsTrue(_savable.GetULong(2) == 200);
            Assert.IsTrue(_savable.GetULong(200) == 2);
            Assert.IsTrue(_savable.GetULong(400, 4) == 4);
            Assert.IsTrue(_savable.GetULong(401, 4) == 4);
            
            _savable.SaveULong(1, 4);
            _savable.SaveULong(2, 5);
            _savable.SaveULong(200, 6);
            _savable.SaveULong(400, 7);
            _savable.SaveULong(401, 7);
            
            Assert.IsTrue(_savable.GetULong(1) == 4);
            Assert.IsTrue(_savable.GetULong(2) == 5);
            Assert.IsTrue(_savable.GetULong(200) == 6);
            Assert.IsTrue(_savable.GetULong(400, 345) == 7);
            Assert.IsTrue(_savable.GetULong(401, 345) == 7);
            Thread.Sleep(1000);
            _moq.Verify(_ => _.WriteBytes(It.IsAny<string>(), It.IsAny<byte[]>()));
            _savable.Dispose();

        }
        
        [Test]
        public void SavablULongTest_Delete()
        {
            _savable.GetULong(1, 123);
            _savable.GetULong(2, 345);
            _savable.GetULong(3, 678);
            
            _savable.DeleteULong(2);

            var rez = _savable.GetULong(2, 8);
            Assert.IsTrue(rez == 8);
            rez = _savable.GetULong(1, 8);
            Assert.IsTrue(rez == 123);
            
            _savable.DeleteULong(3);
            rez = _savable.GetULong(3, 0);
            Assert.IsTrue(rez == 0);
            
            _savable.Dispose();
        }

        [Test]
        public void SavableFloatTest()
        {
            _savable.GetFloat(1, -100);
            _savable.GetFloat(2, 200);
            _savable.GetFloat(200, 2);
            
            _savable.SaveFloat(1, -100);
            _savable.SaveFloat(2, 200);
            _savable.SaveFloat(200, 2);

            Assert.IsTrue(_savable.GetFloat(1).Equals(-100));
            Assert.IsTrue(_savable.GetFloat(2).Equals(200));
            Assert.IsTrue(_savable.GetFloat(200).Equals(2));
            Assert.IsTrue(_savable.GetFloat(400, 4).Equals(4));
            Assert.IsTrue(_savable.GetFloat(401, -4).Equals(-4));

            _savable.SaveFloat(1, 4);
            _savable.SaveFloat(2, 5);
            _savable.SaveFloat(200, 6);
            _savable.SaveFloat(400, 7);
            _savable.SaveFloat(401, -7);

            Assert.AreEqual(_savable.GetFloat(1), 4);
            Assert.AreEqual(_savable.GetFloat(2), 5);
            Assert.AreEqual(_savable.GetFloat(200), 6);
            Assert.AreEqual(_savable.GetFloat(400, 345), 7);
            Assert.AreEqual(_savable.GetFloat(401, 345), -7);
            Thread.Sleep(1000);
            _moq.Verify(_ => _.WriteBytes(It.IsAny<string>(), It.IsAny<byte[]>()));
            _savable.Dispose();

        }
        
                
        [Test]
        public void SavablFloatsTest_Delete()
        {
            _savable.GetFloat(1, 123);
            _savable.GetFloat(2, 345);
            _savable.GetFloat(3, 678);
            
            _savable.DeleteFloat(2);

            var rez = _savable.GetFloat(2, 8);
            Assert.IsTrue(Math.Abs(rez - 8) < 0.1f);
            rez = _savable.GetFloat(1, 8);
            Assert.IsTrue(Math.Abs(rez - 123) < 0.1f);
            
            _savable.DeleteFloat(3);
            rez = _savable.GetFloat(3, 0);
            Assert.IsTrue(Math.Abs(rez) < 0.1f);
            
            _savable.Dispose();
        }
        
        [Test]
        public void SavableDoubleTest()
        {
            _savable.GetDouble(1, -100);
            _savable.GetDouble(2, 200);
            _savable.GetDouble(200, 2);
            
            _savable.SaveDouble(1, -100);
            _savable.SaveDouble(2, 200);
            _savable.SaveDouble(200, 2);

            Assert.IsTrue(_savable.GetDouble(1).Equals(-100));
            Assert.IsTrue(_savable.GetDouble(2).Equals(200));
            Assert.IsTrue(_savable.GetDouble(200).Equals(2));
            Assert.IsTrue(_savable.GetDouble(400, 4).Equals(4));
            Assert.IsTrue(_savable.GetDouble(401, -4).Equals(-4));

            _savable.SaveDouble(1, 4);
            _savable.SaveDouble(2, 5);
            _savable.SaveDouble(200, 6);
            _savable.SaveDouble(400, 7);
            _savable.SaveDouble(401, -7);

            Assert.AreEqual(_savable.GetDouble(1), 4);
            Assert.AreEqual(_savable.GetDouble(2), 5);
            Assert.AreEqual(_savable.GetDouble(200), 6);
            Assert.AreEqual(_savable.GetDouble(400, 345), 7);
            Assert.AreEqual(_savable.GetDouble(401, -345), -7);
            Thread.Sleep(1000);
            _moq.Verify(_ => _.WriteBytes(It.IsAny<string>(), It.IsAny<byte[]>()));
            _savable.Dispose();

        }
        
                        
        [Test]
        public void SavablDoubleTest_Delete()
        {
            _savable.GetDouble(1, 123);
            _savable.GetDouble(2, 345);
            _savable.GetDouble(3, 678);
            
            _savable.DeleteDouble(2);

            var rez = _savable.GetDouble(2, 8);
            Assert.IsTrue(Math.Abs(rez - 8) < 0.1f);
            rez = _savable.GetDouble(1, 8);
            Assert.IsTrue(Math.Abs(rez - 123) < 0.1f);
            
            _savable.DeleteDouble(3);
            rez = _savable.GetDouble(3, 0);
            Assert.IsTrue(Math.Abs(rez) < 0.1f);
            
            _savable.Dispose();
        }


    }
}
