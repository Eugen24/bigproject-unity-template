using System;
using System.Collections.Generic;
using Template.Scripts.Savable;

namespace Template.Scripts.TimeServices
{
    public class MissingDetector : IUpdater
    {
        private readonly SavableAPI _savableApi;
        private readonly ushort _id;
        private readonly List<IMissingHandler> _list;

        public MissingDetector(SavableAPI savableApi, TimeService timeService, ushort saveId)
        {
            _savableApi = savableApi;
            _list = new List<IMissingHandler>();
            _id = saveId;
            timeService.Register(this);
        }

        public void Register(IMissingHandler handler)
        {
            _list.Add(handler);
        }

        public void UnRegister(IMissingHandler handler)
        {
            _list.Remove(handler);
        }

        public void OnUpdate(ref float deltaTime)
        {
            var currentTime = DateTime.Now;
            var lastTime = _savableApi.GetLong(_id, currentTime.Ticks);
            var lastUpdate = new DateTime(lastTime);
            var difSeconds = (currentTime - lastUpdate).TotalSeconds;
            if (difSeconds > 1)
            {
                FireMissingEvent(difSeconds);
            }
            _savableApi.SaveLong(_id, currentTime.Ticks);
        }


        private void FireMissingEvent(double secondsMissing)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                _list[i].OnMissing(secondsMissing);
            }
        }
    }


    public interface IMissingHandler
    {
        void OnMissing(double secondMissing);
    }
}
