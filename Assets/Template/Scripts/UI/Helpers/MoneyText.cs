using Template.Scripts.DI;
using Template.Scripts.Monetization;
using UnityEngine.UI;

namespace Template.Scripts.UI.Helpers
{
    public class MoneyText : InjectedMono
    {
        [In] private MoneySystem _money;
        [Get] private Text _text;
        
        public override void OnSyncStart()
        {
            _text.text = _money.PlayerMoney.ToString();
        }
    }
}
