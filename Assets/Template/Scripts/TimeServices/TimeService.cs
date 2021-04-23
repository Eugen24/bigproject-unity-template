
using System.Collections.Generic;

namespace Template.Scripts.TimeServices
{
    public sealed class TimeService
    {
        private readonly List<IUpdater> _list;

        public TimeService()
        {
            _list = new List<IUpdater>();
        }

        public void Register(IUpdater obj)
        {
            _list.Add(obj);
        }

        public void UnRegister(IUpdater obj)
        {
            _list.Remove(obj);
        }

        public void UpdateFromMono(float deltaTime)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                _list[i].OnUpdate(ref deltaTime);
            }
        }
        
    }

    public interface IUpdater
    {
        void OnUpdate(ref float deltaTime);
    }
}
