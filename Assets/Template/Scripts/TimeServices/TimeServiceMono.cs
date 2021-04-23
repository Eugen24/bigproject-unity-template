using Template.Scripts.DI;
using UnityEngine;

namespace Template.Scripts.TimeServices
{
    public class TimeServiceMono : MonoBehaviour
    {
        private readonly TimeService _timeService = new TimeService();

        private void Update()
        {
            _timeService.UpdateFromMono(Time.deltaTime);
        }
    
        public void Register(IUpdater obj)
        {
            _timeService.Register(obj);
        }

        public void UnRegister(IUpdater obj)
        {
            _timeService.UnRegister(obj);
        }
    
    }
}
