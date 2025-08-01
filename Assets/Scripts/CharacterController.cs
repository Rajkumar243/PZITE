using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;
public class CharacterController : MonoBehaviour
{

    public AnimationController _animationController;


    public SkeletonAnimation Ambu;
    public SkeletonAnimation Ammu;

    public Sprite AmbuBlured;
    public Sprite AmmuBlured;

    public List<Image> CharacterImage;
   
    // Start is called before the first frame update
    void Start()
    {
        _animationController.skeletonAnimation = Ambu;
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
       
        
        for (int i = 0; i < CharacterImage.Count; i++)
        {
           CharacterImage[i].sprite = AmbuBlured;
        }

    }
    public void OnChangeCharacterAmmu()
    {
        _animationController.skeletonAnimation = Ammu;
       Ambu.gameObject.SetActive(false);
       Ammu.gameObject.SetActive(true);

        for (int i = 0; i < CharacterImage.Count; i++)
        {
            CharacterImage[i].sprite = AmmuBlured;
        }

    }
}
