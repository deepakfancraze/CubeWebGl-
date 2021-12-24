using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void BallRotate(string ballCategory, Animator ball);
public delegate void CubeRotate();
public delegate void PlayVideoInAnimation();
public delegate void VideoPlay();
public delegate void VideoStop();
public delegate void VideoPause();
public class IntroAnimController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] RuntimeAnimatorController legendryAnimController;
    [SerializeField] RuntimeAnimatorController platinumAnimController;
    [SerializeField] RuntimeAnimatorController genisisAnimController;
    [SerializeField] RuntimeAnimatorController epicAnimController;
    [SerializeField] RuntimeAnimatorController rareAnimController;
    [SerializeField] RuntimeAnimatorController commonAnimController;
    [SerializeField] VideoScreenController videoScreenController;
    [SerializeField] GameObject videoThumbnail;

    public bool isCubeRotaionForTesting;
    public static BallRotate ballRotate;
    public static VideoPlay videoPlay;
    public static VideoStop videoStop;
    public static CubeRotate cubeRotate;


    internal Animator currentAnimator;

    public static IntroAnimController instance;
    string catagory;
    Animator ball;

    public static bool audioIsPlaying;
    public bool canSwipe;
    private void Awake()
    {
        instance = this;
        currentAnimator = this.GetComponent<Animator>();
        //instance.currentAnimator.SetTrigger("startIntro");
    }

    private void OnEnable()
    {
        CubeDataController.setDefaultAnimationBeforeStart += SetDefaultIntroAnimation;
        CubeDataController.onEverythingLoaded += OnEverythingLoaded;
        EventManager.Subscribe(EventManager.EventType.StateDecided, OnStateDecided);
        VideoInfo.scaleDownVideo += ScaleDwonOnVideoFinished;
        VideoInfo.manageTumbnailOnVideo += HandleThumbnail;
        AnimationTriggers.manageTumbnailOnVideo += HandleThumbnail;

    }

    private void OnDisable()
    {
        CubeDataController.setDefaultAnimationBeforeStart -= SetDefaultIntroAnimation;
        CubeDataController.onEverythingLoaded -= OnEverythingLoaded;
        EventManager.Unsubscribe(EventManager.EventType.StateDecided, OnStateDecided);
        VideoInfo.scaleDownVideo -= ScaleDwonOnVideoFinished;
        VideoInfo.manageTumbnailOnVideo -= HandleThumbnail;
        AnimationTriggers.manageTumbnailOnVideo -= HandleThumbnail;

    }

    void OnStateDecided(object data)
    {
        State state = (State)data;
        isCubeRotaionForTesting = state == State.Video || state == State.Recording;
    }

    public static void SetIntroAnimController(string rarity)
    {
        Debug.Log("Rarity _____________" + rarity);
        instance.catagory = rarity;
        instance.ball = instance.gameObject.transform.Find("FInalCubeModel/Balls").GetComponentInChildren<Animator>();
        switch (rarity)
        {
            case "EPIC":
                instance.currentAnimator.runtimeAnimatorController = instance.epicAnimController;
                break;
            case "GENESIS":
                instance.currentAnimator.runtimeAnimatorController = instance.genisisAnimController;
                break;
            case "LEGENDARY":
                instance.currentAnimator.runtimeAnimatorController = instance.legendryAnimController;

                break;
            case "PLATINUM":
                instance.currentAnimator.runtimeAnimatorController = instance.platinumAnimController;
                break;
            case "RARE":
                instance.currentAnimator.runtimeAnimatorController = instance.rareAnimController;
                break;
            case "COMMON":
                instance.currentAnimator.runtimeAnimatorController = instance.commonAnimController;
                break;
        }
    }

    void OnEverythingLoaded()
    {
        instance.currentAnimator.SetTrigger("startIntro");
        EventManager.Dispatch(EventManager.EventType.IntroAnimStarted, null);
    }

    void SetDefaultIntroAnimation()
    {
        instance.currentAnimator.SetTrigger("setDefault");
    }
    public void BallRotating()
    {
        if (ballRotate != null)
        {
            ballRotate(catagory,ball);
        }
    }

    void IntroAnimationFinished()
    {

    }

    public void CubeIntroAnim()
    {
        if (StateController.GetState() == State.Recording || StateController.GetState() == State.Video)
        {
            instance.currentAnimator.SetTrigger("NonInteractive");
        }
        else
        {
            instance.currentAnimator.SetTrigger("Interactive");
        }
    }


    public void PlayVideoAutoMode()
    {

        if (videoPlay != null)
        {
            videoPlay();
        }
    }

    public void StartInteraction()
    {
        canSwipe = true;
        if (videoPlay != null)
        {
            videoPlay();
        }
    }



    private void HandleThumbnail(bool isOn)
    {
        videoThumbnail.gameObject.SetActive(isOn);
        if (!isOn)
        {
            CubeDataController.audioCanBeEnabled?.Invoke();
            audioIsPlaying = true;
        }
        else
        {
            CubeDataController.audioCanBeDisabled?.Invoke();
            audioIsPlaying = false;
        }
    }

    void AudioCanBeEnabledAnimEvent()
    {
        CubeDataController.audioCanBeEnabled?.Invoke();
        audioIsPlaying = true;
    }

    private void ScaleDwonOnVideoFinished()
    {
        instance.currentAnimator.SetTrigger("StopVideo");
        StartCoroutine(WaitForScaleDown());

    }

    private IEnumerator WaitForScaleDown()
    {
        yield return new WaitForSeconds(2);
        if (cubeRotate != null)
            cubeRotate();
    }

    void PlayVideoRecording()
    {
        if (StateController.GetState() != State.Interaction)
        {
            if (VideoRecorder.recordingState != RecordingState.ScreenshotRecording)
            {
                if (videoPlay != null)
                {
                    videoPlay();
                }
            }
            else
            {
                StartCoroutine(SkipVideoPLayerOnScreenShots());
            }
        }

    }



    private IEnumerator SkipVideoPLayerOnScreenShots()
    {
        yield return new WaitForSeconds(5);
        if (cubeRotate != null)
            cubeRotate();
    }

}
