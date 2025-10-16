using UnityEngine;
using UnityEngine.SceneManagement;

public class BootLoader : MonoBehaviour
{
    void Start()
    {
        // Directly load your main loading scene
        SceneManager.LoadSceneAsync("SplashImages");
    }
}
