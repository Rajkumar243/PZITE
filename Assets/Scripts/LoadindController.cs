using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingController : MonoBehaviour
{
    [Header("UI")]
    public Slider progressBar;
    public Text progressText; // Optional percentage

    [Header("Settings")]
    public string sceneToLoad = "MainScene";
    public float loadSpeed = 0.5f;

    private float targetProgress = 0f;

    private void Start()
    {
        Application.backgroundLoadingPriority = ThreadPriority.High;
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneToLoad);
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            targetProgress = Mathf.Clamp01(op.progress / 0.9f);

            progressBar.value = Mathf.MoveTowards(progressBar.value, targetProgress, Time.deltaTime * loadSpeed);

            if (progressText != null)
                progressText.text = Mathf.RoundToInt(progressBar.value * 100) + "%";

            if (op.progress >= 0.9f && progressBar.value >= 0.99f)
            {
                yield return new WaitForSeconds(1f);
                op.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
