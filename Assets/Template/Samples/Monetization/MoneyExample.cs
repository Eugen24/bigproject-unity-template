using Template.Scripts.DI;
using Template.Scripts.Monetization;
using UnityEngine.UI;

namespace Template.Samples.Monetization
{
    public class MoneyExample : InjectedMono
    {
        [In] private MoneySystem _moneySystem;
        public Text MoneyText;
        public Button Decrement;
        public void OnIncrepemtnWith10()
        {
            _moneySystem.Add(10);
        }

        public void OnDecrementWith20()
        {
            _moneySystem.Decrement(20);
        }

        private void Update()
        {
            MoneyText.text = _moneySystem.PlayerMoney.ToString();
            Decrement.interactable = _moneySystem.IsPurchasable(20);
        }
    }
}
