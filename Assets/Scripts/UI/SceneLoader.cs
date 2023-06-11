using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private GameObject loadingScreen;
        [SerializeField] private Slider slider;
        
        public void StartNextScene()
        {
            StartCoroutine(LoadNextSceneAsync());
        }

        IEnumerator LoadNextSceneAsync()
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            loadingScreen.SetActive(true);

            while (!operation.isDone)
            {
                slider.value = Mathf.Clamp01(operation.progress / .9f);
                yield return null;
            }
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
