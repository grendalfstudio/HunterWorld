using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.LoadingScene;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class EndingTexts : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textField;

        private int activeTextIndex = 0;

        public void PlayTexts()
        {
            Invoke(nameof(PlayNextText), 20);
        }

        private void PlayNextText()
        {
            textField.text = PlayerProfile.Instance.GameTexts.Credits[activeTextIndex];
            activeTextIndex++;
            
            if (activeTextIndex < PlayerProfile.Instance.GameTexts.Credits.Count)
            {
                PlayTexts();
            }
        }
    }

}
