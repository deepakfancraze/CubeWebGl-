using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTriggers : MonoBehaviour
{
    [SerializeField] MomentSequenceController sequenceController;
    [SerializeField] Animator cubeAnimator;
    [SerializeField] RuntimeAnimatorController swipeController;
    [SerializeField] RuntimeAnimatorController cubeRotateController;
    [SerializeField] VideoRecorder videoRecorder;
    [SerializeField] IntroAnimController introAnimController;
    public static Action animationsHaveEnded;
    public static VideoPlay videoPlay;
    public static VideoPause videoPause;
    public static ManageTumbnailOnVideoEnded manageTumbnailOnVideo;

    private Animator currentBallAnimator;
    public void AnimationEnd()
    {
        Debug.Log("Animation Ended");
        sequenceController.MomentEnded();

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            NextScreen();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            PreviousScreen();
        }
    }

    private void OnEnable()
    {
        CubeDataController.setDefaultAnimationBeforeStart += SetDefaultBallAnimation;
        EventManager.Subscribe(EventManager.EventType.StateDecided, OnStateDecided);
        SwipeController.swipeLeft += OnSwipeLeft;
        SwipeController.swipeRight += OnSwipeRight;
        IntroAnimController.ballRotate += StartBallRotatingAnimation;
        IntroAnimController.cubeRotate += RotateCube;
        CubeDataController.onEverythingLoaded += EverythingLoaded;
    }
    private void OnDisable()
    {
        CubeDataController.setDefaultAnimationBeforeStart -= SetDefaultBallAnimation;
        EventManager.Unsubscribe(EventManager.EventType.StateDecided, OnStateDecided);
        SwipeController.swipeLeft -= OnSwipeLeft;
        SwipeController.swipeRight -= OnSwipeRight;
        IntroAnimController.ballRotate -= StartBallRotatingAnimation;
        IntroAnimController.cubeRotate -= RotateCube;
        CubeDataController.onEverythingLoaded -= EverythingLoaded;

    }


    void OnStateDecided(object data)
    {
        State state = (State)data;
        if (state == State.Interaction)
            this.GetComponent<Animator>().runtimeAnimatorController = swipeController;
        else
            this.GetComponent<Animator>().runtimeAnimatorController = cubeRotateController;

    }
    private void RotateCube()
    {
        if (StateController.GetState() == State.Recording || StateController.GetState() == State.Video)
        {
            this.GetComponent<Animator>().SetTrigger("startRotating");
        }
    }



    private void EverythingLoaded()
    {
        StartIntroAnimation();
    }

    public void NextScreen()
    {
        cubeAnimator.SetTrigger("nextScreen");
    }

    public void PreviousScreen()
    {
        cubeAnimator.SetTrigger("previousScreen");
    }

    public void StartBallRotatingAnimation(string rarity,Animator ball)
    {
        switch (rarity)
        {
            case "EPIC":
                ball.SetTrigger("startRotate");
                currentBallAnimator = ball;
                break;
            case "GENESIS":
                ball.SetTrigger("startRotate");
                currentBallAnimator = ball;

                break;
            case "LEGENDARY":
                ball.SetTrigger("startRotate");
                currentBallAnimator = ball;

                break;
            case "PLATINUM":
                ball.SetTrigger("startRotate");
                currentBallAnimator = ball;

                break;
            case "RARE":
                ball.SetTrigger("startRotate");
                currentBallAnimator = ball;

                break;
            case "COMMON":
                ball.SetTrigger("startRotate");
                currentBallAnimator = ball;

                break;

        }
    }

    public void SetDefaultBallAnimation()
    {
        if (currentBallAnimator != null)
        {
            currentBallAnimator.SetTrigger("setDefault");
        }
    }


    public void StartIntroAnimation()
    {
        cubeAnimator.SetTrigger("startIntro");
        EventManager.Dispatch(EventManager.EventType.IntroAnimStarted, null);
    }

    //public void IntroAnimFinished()
    //{
    //    VideoScreenController.canSwipe = true;
    //}

    public void CubeRotationFinished()
    {
        if (animationsHaveEnded != null)
        {
            animationsHaveEnded.Invoke();
        }
    }
    void OnSwipeUp()
    {
    }

    void OnSwipeDown()
    {

    }

    void OnSwipeLeft()
    {
        NextScreen();
    }

    void OnSwipeRight()
    {
        PreviousScreen();
    }

    public void PlayVideoInteraction()
    {
        if (videoPlay != null)
        {
            videoPlay();
        }
    }

    public void PauseVideo()
    {
        Debug.LogWarning("video pause");
        if (videoPause != null)
        {
            videoPause();
        }
    }

    void ScreenshotFromSideAnimEvent()
    {
        EventManager.Dispatch(EventManager.EventType.CanMakeScreenshot, null);
    }

    void ScreenshotFromBottomAnimEvent()
    {
        EventManager.Dispatch(EventManager.EventType.MakeScreenshotWithBottomCamera, null);
    }
}