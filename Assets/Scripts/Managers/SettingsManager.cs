using System;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class SettingsManager : MonoBehaviour
    {
        [SerializeField] private Toggle onlyKeyboardToggle, keyboardAndMouseToggle;

        public void ChangeOnlyKeyboard(bool isOn)
        {
            if (isOn)
            {
                GameManager.Instance.isKeyboardAndMouse = false;
                return;
            }
            GameManager.Instance.isKeyboardAndMouse = true;
           keyboardAndMouseToggle.isOn = true;

        }
        
        public void ChangeKeyboardAndMouse(bool isOn)
        {
            if (isOn)
            {
                GameManager.Instance.isKeyboardAndMouse = true;
                return;
            }
            GameManager.Instance.isKeyboardAndMouse = false;
            onlyKeyboardToggle.isOn = true;            
        }
        
        private void Update()
        {
            // GameManager.Instance.isKeyboardAndMouse = keyboardAndMouseToggle.isOn;
            
            // if (keyboardAndMouseToggle.isOn)
            // {
            //     GameManager.Instance.isKeyboardAndMouse = true;
            //     onlyKeyboardToggle.isOn = false;
            // }
            // else
            // {
            //     GameManager.Instance.isKeyboardAndMouse = false;
            //     onlyKeyboardToggle.isOn = true;
            // }
            //
            // if (onlyKeyboardToggle.isOn)
            // {
            //     GameManager.Instance.isKeyboardAndMouse = false;
            //     keyboardAndMouseToggle.isOn = false;
            // }
            // else
            // {
            //     GameManager.Instance.isKeyboardAndMouse = true;
            //     keyboardAndMouseToggle.isOn = true;
            // }
        }
    }
}