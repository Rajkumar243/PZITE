using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class CharacterController : MonoBehaviour
{

    public AnimationController _animationController;


    public SkeletonAnimation Ambu;
    public SkeletonAnimation Ammu;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnChangeCharacterAmbu()
    {
        _animationController.skeletonAnimation = Ambu;
       Ammu.gameObject.SetActive(false);
       Ambu.gameObject.SetActive(true);
    }
    public void OnChangeCharacterAmmu()
    {
        _animationController.skeletonAnimation = Ammu;
       Ambu.gameObject.SetActive(false);
       Ammu.gameObject.SetActive(true);
    }
}
