using Moq;
using NUnit.Framework;
using UnityEngine;

namespace Template.Scripts.Bonus.Editor
{
    public class OfflineBonusTest
    {
        [Test]
        public void OfflineBonusTestSimplePasses()
        {
            var bonus = new OfflineBonusConfig
            {
                DiamondPerMissingSeconds = 1,
                GoldPerMissingSeconds = 1,
                ScorePerMissingSeconds = 1
            };
            
            var moq = new Mock<ISavableOfflineData>();
            var offline = new OfflineBonusService(bonus, moq.Object);
            offline.OnMissing(1);
            moq.VerifySet(_ => _.Diamond, Times.Once());            
            moq.VerifySet(_ => _.Gold, Times.Once());            
            moq.VerifySet(_ => _.Score, Times.Once());            
        }
        
        [Test]
        public void OfflineBonusTestAlgormith()
        {
            var bonus = new OfflineBonusConfig
            {
                DiamondPerMissingSeconds = 100,
                GoldPerMissingSeconds = 15,
                ScorePerMissingSeconds = 20
            };

            ulong seconds = 1;

            var dummy = new DummyData
            {
                Diamond = 0,
                Gold = 0,
                Score = 0
            };
            
            var offline = new OfflineBonusService(bonus, dummy);
            offline.OnMissing(seconds);
            Assert.AreEqual(bonus.DiamondPerMissingSeconds * seconds, dummy.Diamond);
            Assert.AreEqual(bonus.GoldPerMissingSeconds* seconds, dummy.Gold);
            Assert.AreEqual(bonus.ScorePerMissingSeconds* seconds, dummy.Score);
        }

        private class DummyData : ISavableOfflineData
        {
            public ulong Gold { get; set; }
            public ulong Diamond { get; set; }
            public ulong Score { get; set; }
        }
        
    }
}
