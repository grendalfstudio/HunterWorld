using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class EndingTexts : MonoBehaviour
    {
        [SerializeField] private List<GameObject> endingTexts;

        private int activeTextIndex = 0;

        public void PlayTexts()
        {
            Invoke(nameof(PlayNextText), 20);
        }

        private void PlayNextText()
        {
            if (activeTextIndex != 0)
            {
                endingTexts[activeTextIndex].SetActive(false);
            }
            
            activeTextIndex++;
            endingTexts[activeTextIndex].SetActive(true);
            
            if (activeTextIndex < endingTexts.Count - 1)
            {
                PlayTexts();
            }
        }
    }

}
