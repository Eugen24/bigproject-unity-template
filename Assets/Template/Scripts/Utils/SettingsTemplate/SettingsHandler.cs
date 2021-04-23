using System;
using UnityEngine;

namespace Template.Scripts.Utils.SettingsTemplate
{
    public class SettingsHandler : MonoBehaviour
    {
        [SerializeField] private string _url;
        [SerializeField] private GameObject _view;

        [SerializeField] private GameObject _hapticOn;
        [SerializeField] private GameObject _hapticOff;
        private bool _isOpen;

        private static string HapticId = "Haptic";
        
        public static bool IsHaptic
        {
            get => PlayerPrefs.GetInt(HapticId, 0) != 1;
            set => PlayerPrefs.SetInt(HapticId, value ? 1 : 0);
        }

        private void Awake()
        {
            CloseView();
            UpdateView();
        }
        
        private void LateUpdate()
        {
            if (_isOpen == false)
                return;

            if (Input.GetMouseButtonUp(0))
            {
                if (UiHelper.IsOnUi() == false)
                {
                    CloseView();
                }
                UpdateView();
            }
        }

        private void UpdateView()
        {
            _hapticOn.SetActive(IsHaptic);
            _hapticOff.SetActive(!IsHaptic);
        }

        public void OnEnableHaptic()
        {
            IsHaptic = true;
        }

        public void OnDisableHaptic()
        {
            IsHaptic = false;
        }

        
        public void OpenView()
        {
            if (_isOpen)
            {
                CloseView();
                return;
            }
            _view.SetActive(true);
            _isOpen = true;
        }

        private void CloseView()
        {
            _view.SetActive(false);
            _isOpen = false;
        }



        public void OpenPrivacyPolicy()
        {
            Application.OpenURL(_url);
        }
    }
}
