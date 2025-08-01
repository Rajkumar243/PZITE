using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadindController : MonoBehaviour
{
    public Slider progressBar;
    public string sceneToLoad = "YourSceneName";

    private float targetProgress = 0;

    void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            // Step 1: Calculate target progress (0 to 1)
            targetProgress = Mathf.Clamp01(operation.progress / 0.9f);

            // Step 2: Smoothly move the slider
            progressBar.value = Mathf.MoveTowards(progressBar.value, targetProgress, Time.deltaTime * 0f); // speed = 0.5f

            // Optional: wait before activating scene
            if (operation.progress >= 0.9f && progressBar.value >= 0.99f)
            {
                yield return new WaitForSeconds(1.5f); // optional delay
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    void Update()
    {
        // Smooth transition continues even if coroutine is paused
        progressBar.value = Mathf.MoveTowards(progressBar.value, targetProgress, Time.deltaTime * 0.5f);
    }

}
