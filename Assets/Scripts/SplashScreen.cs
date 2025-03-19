using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public List<GameObject> SplashImages;

    public string MainScene;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("OnplaySplashImage1", 0.5f);
    }

    void OnplaySplashImage1()
    {
        SplashImages[0].gameObject.SetActive(true);


        Invoke("OnLoadScene", 5f);
    }
  
       
    

    void OnLoadScene()
    {
        SceneManager.LoadScene(MainScene, LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
