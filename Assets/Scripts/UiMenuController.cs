using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiMenuController : MonoBehaviour
{

    public List<GameObject> FoodItems;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnDisableFoodItems()
    {
        foreach(GameObject UiItems in FoodItems)
        {
            UiItems.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
