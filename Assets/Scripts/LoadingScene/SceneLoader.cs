using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.LoadingScene
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private Slider slider;

        public void LoadScene(int index)
        {
            StartCoroutine(LoadAsynchronously(index));
        }

        private IEnumerator LoadAsynchronously(int sceneIndex)
        {
            var operation = SceneManager.LoadSceneAsync(sceneIndex);

            while (!operation.isDone)
            {
                if (slider != null)
                {
                    var progress = Mathf.Clamp01(operation.progress / 0.9f);
            
                    slider.value = progress;
                }
                
                yield return null;
            }
        }
    }
}

