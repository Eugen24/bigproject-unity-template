using System;
using Template.Scripts.TimeServices;
using UnityEngine;

namespace Template.Scripts.Bonus
{
    public class OfflineBonusService : IMissingHandler
    {
        private readonly OfflineBonusConfig _config;
        private readonly ISavableOfflineData _savable;

        public OfflineBonusService(OfflineBonusConfig config, ISavableOfflineData savable)
        {
            _config = config;
            _savable = savable;
        }
        
        public void OnMissing(double secondMissing)
        {
            if (secondMissing < 0)
                return;
            
            var seconds = (float) secondMissing;
            _savable.Gold += (ulong) Mathf.RoundToInt(seconds * _config.GoldPerMissingSeconds);
            _savable.Diamond += (ulong) Mathf.RoundToInt(seconds * _config.DiamondPerMissingSeconds);
            _savable.Score += (ulong) Mathf.RoundToInt(seconds * _config.ScorePerMissingSeconds);
        }
    }


    public interface IOfflineBonusUpdater
    {
        void OnIncreaseGold(ulong amount);
        void OnIncreaseDiamond(ulong amount);
        void OnIncreaseScore(ulong amount);
    }


    public interface ISavableOfflineData
    {
        ulong Gold { get; set; }
        ulong Diamond { get; set; }
        ulong Score { get; set; }
    }

    public struct OfflineBonusConfig
    {
        public ulong GoldPerMissingSeconds;
        public ulong DiamondPerMissingSeconds;
        public ulong ScorePerMissingSeconds;
    }
}
