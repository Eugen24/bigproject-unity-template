using UnityEngine;

namespace Template.Scripts.Monetization
{
    public class MoneySystem : MonoBehaviour
    {
        private int _playerMoney;

        private const string PlayerMoneyPrefsId = "PlayerMoney";
        public int PlayerMoney
        {
            get => _playerMoney;
            private set
            {
                _playerMoney = value;
                if (_playerMoney < 0)
                    _playerMoney = 0;
                PlayerPrefs.SetInt(PlayerMoneyPrefsId, _playerMoney);
            }
        }

        private void Awake()
        {
            _playerMoney = PlayerPrefs.GetInt(PlayerMoneyPrefsId, 0);
        }

        public bool IsPurchasable(int price)
        {
            return price <= PlayerMoney;
        }
        
        public bool Add(int money)
        {
            if (money < 1) return false;
            PlayerMoney += money;
            return true;
        }

        public bool Decrement(int money)
        {
            if (IsPurchasable(money) == false) 
                return false;
            
            PlayerMoney -= money;
            return true;
        }
    }
}
