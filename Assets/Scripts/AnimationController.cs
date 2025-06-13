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

    public string AlphabetIdelAnimation;
    public List<string> specialTouchAnimation;
    public List<AudioClip> specialTouchAnimationVoice;


    public string GenriceatingAnimation;

    [System.Serializable]
    public class EatingAnimation
    {
        public string animationName;
        public AudioClip clip;
    }

    public List<EatingAnimation> eatingAnimation; //Eating_generic

   

    public List<string> EatingReactionAnimations;
    public List<AudioClip> EatingReactionVoice;

    public List<AnimationClip> FoodFxAnimation;
    public GameObject FoodFxImage;

    [System.Serializable]
    public class CandyeatingAnimation
    {
        public string animationName;
        public AudioClip clip;
    }
    public CandyeatingAnimation Candyeating; //Eating_generic



    //public string CandyeatingAnimation;
    public bool IsCandyEat;

    public string IcecreameatingAnimation;
    public bool IsIceCreamEat;
    
    
    public string MilkeatingAnimation;
    public bool IsMilkEat;

    public bool IsCerealEat;
    public GameObject CerealBowl;
    public GameObject CerealBowlSpoon;
    public AudioClip CerealAudioCilp;

    public  Image foodfx;
    public  Image foodfxSpoon;


    public GameObject foodSparkle;

    public List<string> SleepingAnimation;
    public bool IsSleeping;


    public string WakeupAnimation;


    public List<string> EmotionAnimations;



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
        _audiosource.Stop();
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

    public void AlphabetsIdelAnimation()
    {
        CancelInvoke("PlayIdelAnimation");

        Debug.Log("alphabets Idel");
        skeletonAnimation.state.SetAnimation(0, idelCharacterAnimation[3], false);
        Spine.Animation CurrentTouchAnimation = skeletonAnimation.Skeleton.Data.FindAnimation(idelCharacterAnimation[3]);
        float animationLength = CurrentTouchAnimation?.Duration ?? 0f; // Duration of the animation in seconds
        CancelInvoke("AlphabetsIdelAnimation");
        Invoke("AlphabetsIdelAnimation", animationLength +0.5f);
    }




    public void PlayIdelAnimation()
    {
       int CurrentIdelAnimationvalue =  Random.Range(0, idelCharacterAnimation.Count);

        StartCoroutine(PlayIdelAnimationAfterDelay(0.5f, CurrentIdelAnimationvalue));

      
    } 
    
    public void PlayTouchAnimation()
    {
        StopAllCoroutines();
        StopCoroutine("PlayIdelAnimationAfterDelay");

        string currentAnimationName = skeletonAnimation.AnimationState.GetCurrent(0)?.Animation.Name;

        if (currentAnimationName== "Sleeping_new" || currentAnimationName == "Sleeping_new_v1")
        {

            skeletonAnimation.state.SetAnimation(0, "waking_up", false);
            PlayIdelAnimation();

        }
        else
        {
            int CurrenttouchAnimationvalue = Random.Range(0, specialTouchAnimation.Count);
            StartCoroutine(PlayTouchAnimationAfterDelay(0f, CurrenttouchAnimationvalue));

        }


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
    public void FoodFxAnimationClip(string clipName)
    {
        AnimationClip clip = FoodFxAnimation.Find(c => c.name == clipName);

        if (clip != null )
        {
            FoodFxImage.GetComponent<Animator>().StopPlayback();
            
            FoodFxImage.GetComponent<Animator>().Play(clipName);
        }
        else
        {
            Debug.LogWarning($"Animator or clip '{clipName}' not found.");
        }
    }

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
        Invoke("SparkleOff", 1.5f);
    }

    public void SpoonOnChangeFoodFx(Sprite foodImage)
    {
        foodSparkle.SetActive(true);
        foodfxSpoon.sprite = foodImage;
        Invoke("DelayedSpoonFoodEnable", 0.3f);
    }
    public void DelayedSpoonFoodEnable()
    {
        foodfx.transform.localPosition = new Vector3(0, -255, 0);
        foodfxSpoon.gameObject.SetActive(true);
        Invoke("SparkleOff", 1f);
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
        Spine.Animation eatingAnim = skeletonAnimation.Skeleton.Data.FindAnimation(eatingAnimation[0].animationName);
        _audiosource.clip = eatingAnimation[0].clip;
        _audiosource.Play();
        float animationLength = eatingAnim?.Duration ?? 0f; // Duration of the animation in seconds
       

    }



    public void OnchangeCandyEatingAnimation()
    {
        IsCandyEat = true;

        IsMilkEat = false;
        IsIceCreamEat = false;
        IsCerealEat = false;
    } 
    public void OnchangeIceCreamEatingAnimation()
    {
        IsIceCreamEat = true;

        IsMilkEat = false;
        IsCerealEat = false;
        IsCandyEat = false;
    }
    
    public void OnchangeMilkEatingAnimation()
    {
        IsMilkEat = true;

        IsCerealEat = false;
        IsIceCreamEat = false;
        IsCandyEat = false;
    }

    public void OnchangeCerealEatingAnimation()
    {
         IsCerealEat = true;
        foodfxSpoon.gameObject.SetActive(true);
        IsMilkEat = false;
        IsIceCreamEat = false;
        IsCandyEat = false;
    }


    IEnumerator EatAnimation(float delay, int value)
    {
        CancelInvoke("PlayIdelAnimation");
        

        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Play the animation after the delay
        // skeletonAnimation.state.SetAnimation(0, eatingAnimation[value], false);
        //   Debug.Log(eatingAnimation[value]);




        //  eatingAnimation[0] = GenriceatingAnimation;


        // Assuming 'eatingAnimation' is an array or list of animation names (strings)
       

        if (!IsCandyEat && !IsIceCreamEat && !IsMilkEat && !IsCerealEat)
        {
            int currrenteatingAnimation = Random.Range(0, eatingAnimation.Count);
            skeletonAnimation.state.SetAnimation(0, eatingAnimation[currrenteatingAnimation].animationName, false);
            _audiosource.clip = eatingAnimation[currrenteatingAnimation].clip;
            _audiosource.Play();
            Debug.Log(eatingAnimation[currrenteatingAnimation]);
         
            
            Spine.Animation eatingAnim = skeletonAnimation.Skeleton.Data.FindAnimation(eatingAnimation[currrenteatingAnimation].animationName);
            float animationLength = eatingAnim?.Duration ?? 0f; // Duration of the animation in seconds
            yield return new WaitForSeconds(animationLength + 1f);
            StartCoroutine("EatingReactionAnimation");
        }
        else if(IsCandyEat)
        {
            skeletonAnimation.state.SetAnimation(0,Candyeating.animationName, false);
            Debug.Log(Candyeating.animationName);

            //foodeating voice
            _audiosource.clip = Candyeating.clip;
           _audiosource.Play();
            Debug.Log(Candyeating.clip);


            IsCandyEat = false;
            Spine.Animation eatingAnim = skeletonAnimation.Skeleton.Data.FindAnimation(Candyeating.animationName);
            float animationLength = eatingAnim?.Duration ?? 0f; // Duration of the animation in seconds
            yield return new WaitForSeconds(animationLength + 1f);
            StartCoroutine("EatingReactionAnimation");

            


        }
        else if(IsIceCreamEat)
        {
            skeletonAnimation.state.SetAnimation(0, IcecreameatingAnimation, false);
            Debug.Log(IcecreameatingAnimation);
            IsIceCreamEat = false;
            Spine.Animation eatingAnim = skeletonAnimation.Skeleton.Data.FindAnimation(IcecreameatingAnimation);
            float animationLength = eatingAnim?.Duration ?? 0f; // Duration of the animation in seconds
            yield return new WaitForSeconds(animationLength + 1f);
            StartCoroutine("EatingReactionAnimation");
          
        }
        else if(IsMilkEat)
        {
            skeletonAnimation.state.SetAnimation(0, MilkeatingAnimation, false);
            Debug.Log(MilkeatingAnimation);
            IsMilkEat = false;
            Spine.Animation eatingAnim = skeletonAnimation.Skeleton.Data.FindAnimation(MilkeatingAnimation);
            float animationLength = eatingAnim?.Duration ?? 0f; // Duration of the animation in seconds
            yield return new WaitForSeconds(animationLength + 1f);
            StartCoroutine("EatingReactionAnimation");
          
        }
        else if (IsCerealEat)
        {
            IsCerealEat = false;
            Debug.Log("here");
            int currrenteatingAnimation = Random.Range(0, eatingAnimation.Count);
            skeletonAnimation.state.SetAnimation(0, eatingAnimation[currrenteatingAnimation].animationName, false);
            Debug.Log(eatingAnimation[currrenteatingAnimation]);

            //foodeating voice
            _audiosource.clip = CerealAudioCilp;
            _audiosource.Play();
            Debug.Log(CerealAudioCilp);


            Spine.Animation eatingAnim = skeletonAnimation.Skeleton.Data.FindAnimation(eatingAnimation[currrenteatingAnimation].animationName);
            float animationLength = eatingAnim?.Duration ?? 0f; // Duration of the animation in seconds
            CerealBowl.SetActive(false);//disable the cereal bowl
            CerealBowlSpoon.SetActive(true);//enable the cereal bowlspoon
            yield return new WaitForSeconds(animationLength + 1f);
            StartCoroutine("EatingReactionAnimation");
            CerealBowlSpoon.SetActive(false);//disable the cereal bowlspoon
            foodfxSpoon.gameObject.SetActive(false);

        }


    }

    public void EatingReactionAnimation()
    {
      //  CerealBowlSpoon.SetActive(false);//disable the cereal bowl


        int currrenteatingReaction = Random.Range(0, EatingReactionAnimations.Count);
        skeletonAnimation.state.SetAnimation(0,EatingReactionAnimations[currrenteatingReaction], false);
        Debug.Log(EatingReactionAnimations[currrenteatingReaction]);
        //foodeating voice
            _audiosource.clip = EatingReactionVoice[currrenteatingReaction];
            _audiosource.Play();

        Debug.Log("here");
       
        Spine.Animation eatingAnim = skeletonAnimation.Skeleton.Data.FindAnimation(EatingReactionAnimations[currrenteatingReaction]);
        float animationLength = eatingAnim?.Duration ?? 0f; // Duration of the animation in seconds
        Invoke("PlayIdelAnimation", animationLength + 1f);//play idle animation after eat food animation
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


    //EmotionAnimations
    public void OnEmotionAnimationsPlay(int currrentEmotionAnimation)
    {
        CancelInvoke("PlayIdelAnimation");
        skeletonAnimation.state.SetAnimation(0, EmotionAnimations[currrentEmotionAnimation], true);
        Debug.Log(EmotionAnimations[currrentEmotionAnimation]);
        //EmotionAnimations voice
        // _audiosource.clip = EatingReactionVoice[currrenteatingReaction];
        // _audiosource.Play();


        Invoke("PlayIdelAnimation", 8f);//play idle animation after eat food animation
    }




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

        _audiosource.Stop();
           
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
