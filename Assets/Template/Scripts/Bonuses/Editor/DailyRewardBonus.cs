using System;
using System.IO;
using Moq;
using NUnit.Framework;
using Template.Scripts.Savable;
using Template.Scripts.TimeServices;
using UnityEngine;

namespace Template.Scripts.Bonus.Editor
{
    public class DailyRewardBonus
    {
        // A Test behaves as an ordinary method
        [Test]
        public void DailyRewardBonusSimplePasses()
        {
            
            var moq = new Mock<IFileInterface>();
            moq.Setup(_ => _.ReadBytes(It.IsAny<string>())).Returns(new byte[0]);

            var path = Application.dataPath + Path.DirectorySeparatorChar + "Save1.txt";
            
            var savable = new SavableAPI(new SavableContext
            {
                SourceFilePath = path
            }, moq.Object);
            
            var d = new DailyDetector(savable, 1);
            var now = System.DateTime.Now;

            var config = new DailyRewardConfig
            {
                Rewards = new DailyReward[1]
                {
                    new DailyReward
                    {
                        Day = 1,
                        Diamonds = 1,
                        Gold = 1,
                        Score = 1
                    }
                }
            };
            
            var daily = new DailyRewardService(savable, 2, config);
            daily.GotReward(now);
            now = now.Add(TimeSpan.FromDays(1));
            var reward = daily.GetRewards(now);
            Assert.AreEqual(1, reward.Length);
        }
    }
}
