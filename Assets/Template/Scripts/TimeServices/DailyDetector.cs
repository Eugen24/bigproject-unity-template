using System;
using Template.Scripts.Savable;
using UnityEngine;

namespace Template.Scripts.TimeServices
{
    public class DailyDetector
    {
        private readonly SavableAPI _savable;
        private readonly ushort _saveId;
        
        public DailyDetector(SavableAPI savable, ushort saveId)
        {
            _savable = savable;
            _saveId = saveId;
        }

        public int GetCurrentDay(System.DateTime now)
        {
            var lastTick = new DateTime(_savable.GetLong(_saveId, now.Ticks));
            return Mathf.RoundToInt((float)(now - lastTick).TotalDays);
        }

        public void SetCurrentDay(System.DateTime now)
        {
            _savable.SaveLong(_saveId, now.Ticks);
        }
    }

}
