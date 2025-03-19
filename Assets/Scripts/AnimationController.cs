using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using TMPro;
public class AnimationController : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public AudioSource _audiosource;
  
    public List<string> animationNames;

    public List<string> alphabetsAnimationNames;

    public TMP_Text alphabetValue;

    public List<AudioClip>  alphabetAudioClips;
    public int CurrentalphabetAudioClipValue;
 

    public int CurrentalphabetValue;

    public Animator alphabetAnimation;

    public List<string> alphabetsTexts;


    //numeric value
    public List<string> NumberAnimationNames;
    public TMP_Text NumberValue;
    public List<AudioClip> NumberAudioClips;
    public int CurrentnumberAudioClipValue;


    public int CurrentNumberValue;

    public Animator numberAnimation;

    public List<string> numberTexts;
    //numeric value



    public List<string> idelCharacterAnimation;
    public List<string> specialTouchAnimation;
    public List<AudioClip> specialTouchAnimationVoice;


    public string GenriceatingAnimation;
    public List<string> eatingAnimation; //Eating_generic


    public List<string> EatingReactionAnimations;
    public List<AudioClip> EatingReactionVoice;

    public string CandyeatingAnimation;
    public bool IsCandyEat;

    public string IcecreameatingAnimation;
    public bool IsIceCreamEat;


    public  Image foodfx;
    public GameObject foodSparkle;

    public List<string> SleepingAnimation;
    public bool IsSleeping;


    public List<GameObject> FoodObjects;
    public List<GameObject> AlphabetObjects;
    // Start is called before the first frame update
    void Start()
    {
        _audiosource = this.GetComponent<AudioSource>();


        // Collect all animations from the SkeletonAnimation's SkeletonData
        animationNames = GetAllAnimations();

        // Print out all animation names
        foreach (var animationName in animationNames)
        {
            Debug.Log("Animation: " + animationName);
        }

        PlayIdelAnimation();

    }
    public void StopAllAnimations()
    {
        skeletonAnimation.AnimationState.ClearTracks(); // Stops all animations
        skeletonAnimation.skeleton.SetToSetupPose();   // Resets to setup pose (optional)
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
    
    public void PlayNumberAnimation()
    {

        StopAllCoroutines();
        numberAnimation.Rebind();
        numberAnimation.Play("Start");
        StartCoroutine(PlayNumberAnimationAfterDelay(5f, CurrentNumberValue));

    }
    //alphabets
    public void OnNextAlphabet()
    {

        if (CurrentalphabetValue < alphabetsAnimationNames.Count - 1)
        {
            CurrentalphabetValue++;
            CurrentalphabetAudioClipValue++;
        }
        else
        {
            CurrentalphabetValue = 0;
            CurrentalphabetAudioClipValue = 0;
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
            CurrentalphabetAudioClipValue--;
        }
        else
        {
            CurrentalphabetValue = 0;
            CurrentalphabetAudioClipValue = 0;
        }
        StopAllCoroutines();
        StartCoroutine(PlayAnimationAfterDelay(5f, CurrentalphabetValue));
        alphabetAnimation.Rebind();
        alphabetAnimation.Play("Start");
        alphabetValue.text = alphabetsTexts[CurrentalphabetValue];

       
    
    }



    //numbers
    public void OnNextNumber()
    {

        if (CurrentNumberValue < NumberAnimationNames.Count - 1)
        {
            CurrentNumberValue++;
            CurrentnumberAudioClipValue++;
        }
        else
        {
            CurrentNumberValue = 0;
            CurrentnumberAudioClipValue = 0;
        }

        StopAllCoroutines();
        StartCoroutine(PlayNumberAnimationAfterDelay(5f, CurrentNumberValue));
        numberAnimation.Rebind();
        numberAnimation.Play("Start");
        NumberValue.text = numberTexts[CurrentNumberValue];
    }

    public void OnPreviousNumber()
    {
        if (CurrentNumberValue > 0 && CurrentNumberValue <= NumberAnimationNames.Count - 1)
        {
            CurrentNumberValue--;
            CurrentnumberAudioClipValue--;
        }
        else
        {
            CurrentNumberValue = 0;
            CurrentnumberAudioClipValue = 0;
        }
        StopAllCoroutines();
        StartCoroutine(PlayNumberAnimationAfterDelay(5f, CurrentNumberValue));
        numberAnimation.Rebind();
        numberAnimation.Play("Start");
        NumberValue.text = numberTexts[CurrentNumberValue];



    }
    //numbers

    IEnumerator PlayAnimationAfterDelay(float delay, int value)
    {

        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Play the animation after the delay
        skeletonAnimation.state.SetAnimation(0, alphabetsAnimationNames[value], false);
        Debug.Log(alphabetsAnimationNames[value]);
        //voice 
        _audiosource.clip = alphabetAudioClips[CurrentalphabetAudioClipValue];
        _audiosource.Play();
    }
    
    IEnumerator PlayNumberAnimationAfterDelay(float delay, int value)
    {

        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Play the animation after the delay
        skeletonAnimation.state.SetAnimation(0, NumberAnimationNames[value], false);
        Debug.Log(NumberAnimationNames[value]);
        //voice 
        _audiosource.clip = NumberAudioClips[CurrentnumberAudioClipValue];
        _audiosource.Play();
    }


    public void stopAlphabet() //stop all animation
    {
        StopAllCoroutines();
        alphabetAnimation.Rebind();
        alphabetAnimation.StopPlayback();
      
    }


    public void ResetAlphabetsNumbers()
    {

        CurrentNumberValue = 0;
        CurrentnumberAudioClipValue = 0;
        NumberValue.text = "1";


        CurrentalphabetAudioClipValue = 0;
        CurrentalphabetValue = 0;
        alphabetValue.text = "A";
    }
    /// </Alphabets>



    /// <IdelAnimation>
    public void PlayIdelAnimation()
    {
       int CurrentIdelAnimationvalue =  Random.Range(0, idelCharacterAnimation.Count);

        StartCoroutine(PlayIdelAnimationAfterDelay(2f, CurrentIdelAnimationvalue));

      
    } 
    
    public void PlayTouchAnimation()
    {
        StopAllCoroutines();
        StopCoroutine("PlayIdelAnimationAfterDelay");
        int CurrenttouchAnimationvalue =  Random.Range(0, specialTouchAnimation.Count);

        StartCoroutine(PlayTouchAnimationAfterDelay(0f, CurrenttouchAnimationvalue));

      
    }


    IEnumerator PlayIdelAnimationAfterDelay(float delay, int value)
    {

        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Play the animation after the delay
        skeletonAnimation.state.SetAnimation(0, idelCharacterAnimation[value],false);
        Debug.Log(idelCharacterAnimation[value]);

        Spine.Animation CurrentTouchAnimation = skeletonAnimation.Skeleton.Data.FindAnimation(idelCharacterAnimation[value]);
        float animationLength = CurrentTouchAnimation?.Duration ?? 0f; // Duration of the animation in seconds
        Invoke("PlayIdelAnimation", animationLength);
  
    }
    /// </IdelAnimation>


    IEnumerator PlayTouchAnimationAfterDelay(float delay, int value)
    {

        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Play the animation after the delay
        skeletonAnimation.state.SetAnimation(0, specialTouchAnimation[value], false);

        if (specialTouchAnimationVoice[value]!=null)
        {
            _audiosource.clip = specialTouchAnimationVoice[value];
            _audiosource.Play();
        }

        Debug.Log(specialTouchAnimation[value]);


        Spine.Animation CurrentTouchAnimation = skeletonAnimation.Skeleton.Data.FindAnimation(specialTouchAnimation[value]);
        float animationLength = CurrentTouchAnimation?.Duration ?? 0f; // Duration of the animation in seconds
        Invoke("PlayIdelAnimation", animationLength);

    }


    //Eating start

    public void OnChangeFoodFx(Sprite foodImage)
    {
        foodSparkle.SetActive(true);
        foodfx.sprite = foodImage;
        Invoke("DelayedFoodEnable", 0.3f);
    }
    public void DelayedFoodEnable()
    {
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
        StartCoroutine(EatAnimation(0.0f,0));



        //yummy -step

        // Assuming 'eatingAnimation' is an array or list of animation names (strings)
        Spine.Animation eatingAnim = skeletonAnimation.Skeleton.Data.FindAnimation(eatingAnimation[0]);
        float animationLength = eatingAnim?.Duration ?? 0f; // Duration of the animation in seconds
        //StartCoroutine(EatAnimation(animationLength, 1));

    }



    public void OnchangeCandyEatingAnimation()
    {
        IsCandyEat = true;
    } 
    public void OnchangeIceCreamEatingAnimation()
    {
        IsIceCreamEat = true;
    }

  

    IEnumerator EatAnimation(float delay, int value)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Play the animation after the delay
       // skeletonAnimation.state.SetAnimation(0, eatingAnimation[value], false);
     //   Debug.Log(eatingAnimation[value]);

       
  

      //  eatingAnimation[0] = GenriceatingAnimation;


        // Assuming 'eatingAnimation' is an array or list of animation names (strings)

        if (!IsCandyEat && !IsIceCreamEat)
        {
            int currrenteatingAnimation = Random.Range(0, eatingAnimation.Count);
            skeletonAnimation.state.SetAnimation(0, eatingAnimation[currrenteatingAnimation], false);
            Debug.Log(eatingAnimation[currrenteatingAnimation]);
            Spine.Animation eatingAnim = skeletonAnimation.Skeleton.Data.FindAnimation(eatingAnimation[currrenteatingAnimation]);
            float animationLength = eatingAnim?.Duration ?? 0f; // Duration of the animation in seconds
            yield return new WaitForSeconds(animationLength + 1f);
            StartCoroutine("EatingReactionAnimation");
        }
        else if(IsCandyEat)
        {
            skeletonAnimation.state.SetAnimation(0, CandyeatingAnimation, false);
            Debug.Log(CandyeatingAnimation);
            Spine.Animation eatingAnim = skeletonAnimation.Skeleton.Data.FindAnimation(CandyeatingAnimation);
            float animationLength = eatingAnim?.Duration ?? 0f; // Duration of the animation in seconds
            yield return new WaitForSeconds(animationLength + 1f);
            StartCoroutine("EatingReactionAnimation");
            IsCandyEat = false;
        }
        else if(IsIceCreamEat)
        {
            skeletonAnimation.state.SetAnimation(0, IcecreameatingAnimation, false);
            Debug.Log(IcecreameatingAnimation);
            Spine.Animation eatingAnim = skeletonAnimation.Skeleton.Data.FindAnimation(IcecreameatingAnimation);
            float animationLength = eatingAnim?.Duration ?? 0f; // Duration of the animation in seconds
            yield return new WaitForSeconds(animationLength + 1f);
            StartCoroutine("EatingReactionAnimation");
            IsIceCreamEat = false;
        }
            

   
    }

    public void EatingReactionAnimation()
    {
        int currrenteatingReaction = Random.Range(0, EatingReactionAnimations.Count);
        skeletonAnimation.state.SetAnimation(0,EatingReactionAnimations[currrenteatingReaction], false);
        Debug.Log(EatingReactionAnimations[currrenteatingReaction]);
        //foodeating voice
            _audiosource.clip = EatingReactionVoice[currrenteatingReaction];
            _audiosource.Play();
        

        Invoke("PlayIdelAnimation", 8f);//play idle animation after eat food animation
    }


    //SleepingAnimation


    public void PlaySleepingAnimation()
    {
        StopAllAnimation();
        StopAllAnimations();
        StopAllCoroutines();
        CancelInvoke("PlayIdelAnimation");
        int currrentSleepingAnimation = Random.Range(0, SleepingAnimation.Count);
        skeletonAnimation.state.SetAnimation(0, SleepingAnimation[currrentSleepingAnimation], true);
        Debug.Log(SleepingAnimation[currrentSleepingAnimation]);


    }


    //SleepingAnimation





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

        ResetAlphabetsNumbers();


    }


    public void StopAllAnimation()
    {
        StopAllCoroutines();
        skeletonAnimation.AnimationState.SetEmptyAnimation(0, 0.2f); // Fades out over 0.2 seconds
        CancelInvoke();

        if (!IsInvoking("PlayIdelAnimation"))
        {
            Invoke("PlayIdelAnimation", 0.5f);
        }
           
    }

    // Update is called once per frame
    void Update()
    {

    }
}
