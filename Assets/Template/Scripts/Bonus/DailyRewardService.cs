using System.Linq;
using Castle.Core.Internal;
using Template.Scripts.Savable;
using Template.Scripts.TimeServices;

namespace Template.Scripts.Bonus
{
    public class DailyRewardService
    {
        private readonly DailyDetector _detector;
        private readonly DailyRewardConfig _config;

        public DailyRewardService(SavableAPI savable, ushort saveId, DailyRewardConfig config)
        {
            _config = config;
            _detector = new DailyDetector(savable, saveId);
        }

        public DailyReward[] GetRewards(System.DateTime now)
        {
            var day = _detector.GetCurrentDay(now);
            return _config.Rewards.FindAll(_ => _.Day == day);
        }

        public void GotReward(System.DateTime now)
        {
            _detector.SetCurrentDay(now);
        }
    }


    public struct DailyRewardConfig
    {
        public DailyReward[] Rewards;
    }

    public struct DailyReward
    {
        public int Day;
        public ulong Gold;
        public ulong Diamonds;
        public ulong Score;
    }
}
