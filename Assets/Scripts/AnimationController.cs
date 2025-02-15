using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using TMPro;
public class AnimationController : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;

  
    public List<string> animationNames;

    public List<string> alphabetsAnimationNames;

    public TMP_Text alphabetValue;

    public int CurrentalphabetValue;

    public Animator alphabetAnimation;

    public List<string> alphabetsTexts;


    public List<string> idelCharacterAnimation;
    public List<string> specialTouchAnimation;

    public List<string> eatingAnimation;

    public  Image foodfx;
    public GameObject foodSparkle;


    public List<GameObject> FoodObjects;
    public List<GameObject> AlphabetObjects;
    // Start is called before the first frame update
    void Start()
    {   
        // Collect all animations from the SkeletonAnimation's SkeletonData
        animationNames = GetAllAnimations();

        // Print out all animation names
        foreach (var animationName in animationNames)
        {
            Debug.Log("Animation: " + animationName);
        }

        PlayIdelAnimation();

    }

    private List<string> GetAllAnimations()
    {
        List<string> animationNames = new List<string>();

        if (skeletonAnimation != null && skeletonAnimation.Skeleton != null)
        {
            // Access SkeletonData from SkeletonAnimation
            var skeletonData = skeletonAnimation.Skeleton.Data;

            // Iterate through all animations in the skeleton's SkeletonData
            foreach (var animation in skeletonData.Animations)
            {
                animationNames.Add(animation.Name);
            }
        }
        else
        {
            Debug.LogWarning("SkeletonAnimation or Skeleton is null.");
        }

        return animationNames;
    }
    // Method to play the animation
    public void StartAnimationPlay()
    {
        // Ensure that the SkeletonAnimation component exists
        if (skeletonAnimation != null)
        {
            // Start the animation immediately when the script starts
            PlayAnimation();


        }
    }

    /// <Alphabets>
    public void PlayAnimation()
    {

        StopAllCoroutines();
        alphabetAnimation.Rebind();
        alphabetAnimation.Play("Start");
        StartCoroutine(PlayAnimationAfterDelay(5f, CurrentalphabetValue));

    }

    public void OnNextAlphabet()
    {

        if (CurrentalphabetValue < alphabetsAnimationNames.Count - 1)
        {
            CurrentalphabetValue++;
        }
        else
        {
            CurrentalphabetValue = 0;
        }

        StopAllCoroutines();
        StartCoroutine(PlayAnimationAfterDelay(5f, CurrentalphabetValue));
        alphabetAnimation.Rebind();
        alphabetAnimation.Play("Start");
        alphabetValue.text = alphabetsTexts[CurrentalphabetValue];
    }

    public void OnPreviousAlphabet()
    {
        if (CurrentalphabetValue > 0 && CurrentalphabetValue <= alphabetsAnimationNames.Count - 1)
        {
            CurrentalphabetValue--;

        }
        else
        {
            CurrentalphabetValue = 0;
        }
        StopAllCoroutines();
        StartCoroutine(PlayAnimationAfterDelay(5f, CurrentalphabetValue));
        alphabetAnimation.Rebind();
        alphabetAnimation.Play("Start");
        alphabetValue.text = alphabetsTexts[CurrentalphabetValue];

    }

    IEnumerator PlayAnimationAfterDelay(float delay, int value)
    {

        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Play the animation after the delay
        skeletonAnimation.state.SetAnimation(0, alphabetsAnimationNames[value], false);
        Debug.Log(alphabetsAnimationNames[value]);
    }


    public void stopAlphabet()
    {
        StopAllCoroutines();
        alphabetAnimation.Rebind();
        alphabetAnimation.StopPlayback();
       

    }
    /// </Alphabets>



    /// <IdelAnimation>
    public void PlayIdelAnimation()
    {
       int CurrentIdelAnimationvalue =  Random.Range(0, idelCharacterAnimation.Count);

        StartCoroutine(PlayIdelAnimationAfterDelay(5f, CurrentIdelAnimationvalue));

      
    } 
    
    public void PlayTouchAnimation()
    {
        StopAllCoroutines();
        StopCoroutine("PlayIdelAnimationAfterDelay");
        int CurrenttouchAnimationvalue =  Random.Range(0, specialTouchAnimation.Count);

        StartCoroutine(PlayTouchAnimationAfterDelay(1f, CurrenttouchAnimationvalue));

        PlayIdelAnimation();
    }


    IEnumerator PlayIdelAnimationAfterDelay(float delay, int value)
    {

        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Play the animation after the delay
        skeletonAnimation.state.SetAnimation(0, idelCharacterAnimation[value],false);
        Debug.Log(idelCharacterAnimation[value]);

        

    }
    /// </IdelAnimation>


    IEnumerator PlayTouchAnimationAfterDelay(float delay, int value)
    {

        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Play the animation after the delay
        skeletonAnimation.state.SetAnimation(0, specialTouchAnimation[value], false);
        Debug.Log(specialTouchAnimation[value]);

        Invoke("PlayIdelAnimation", 3f);

    }


    //Eating start

    public void OnChangeFoodFx(Sprite foodImage)
    {
        foodSparkle.SetActive(true);
        foodfx.sprite = foodImage;
        foodfx.transform.localPosition = new Vector3(0, -255, 0);
        foodfx.gameObject.SetActive(true);
        Invoke("SparkleOff", 2f);
    }
    public void SparkleOff()
    {
        foodSparkle.SetActive(false);
    }

    public void PlayEatingAnimation()
    {
        
        //eating -step
        StopAllCoroutines();
        StopCoroutine("PlayIdelAnimationEatingAfterDelay");
        StartCoroutine(EatAnimation(0.5f,0));



        //yummy -step

        // Assuming 'eatingAnimation' is an array or list of animation names (strings)
        Spine.Animation eatingAnim = skeletonAnimation.Skeleton.Data.FindAnimation(eatingAnimation[0]);
        float animationLength = eatingAnim?.Duration ?? 0f; // Duration of the animation in seconds
        StartCoroutine(EatAnimation(animationLength, 1));

  
    }



    IEnumerator EatAnimation(float delay, int value)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Play the animation after the delay
        skeletonAnimation.state.SetAnimation(0, eatingAnimation[value], false);
        Debug.Log(eatingAnimation[value]);
    }

    //Eating end




    //reset

    public void ResetAlphabets()
    {
        foreach(GameObject _gameobject in AlphabetObjects)
        {
            _gameobject.SetActive(false);
        } 
        
        foreach(GameObject _gameobject in FoodObjects)
        {
            _gameobject.SetActive(false);
        }

        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
