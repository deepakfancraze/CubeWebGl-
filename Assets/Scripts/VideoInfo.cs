using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public delegate void ScaleDownVideo();
public delegate void ManageTumbnailOnVideoEnded(bool isOn);
public class VideoInfo : MonoBehaviour
{
    public static ScaleDownVideo scaleDownVideo;
    public static ManageTumbnailOnVideoEnded manageTumbnailOnVideo;
    float videoLength;
    float startTime;
    [SerializeField]
    Animator cubeAnimator;
    [SerializeField]
    private string videoFileName;
    VideoPlayer videoplayer = null;
    State state;



    private void Awake()
    {
        videoplayer = GetComponent<VideoPlayer>();
    }

    void Start()
    {

        //videoLength = (float)videoplayer.clip.length;
        //Debug.Log("Video Length: " + videoLength);
    }
    private void OnEnable()
    {
        //VideoScreenController.videoPlay += StartVideo;
        //VideoScreenController.videoStop += StopVideo;
        //videoplayer.loopPointReached += CheckOver;
        IntroAnimController.videoPlay += StartVideo;
        SwipeMechanic.videoPlay += ResumeVideo;
        SwipeMechanic.videoPause += PauseVideo;

        EventManager.Subscribe(EventManager.EventType.StateDecided, OnStateDecided);
    }

    private void OnDisable()
    {
        //VideoScreenController.videoPlay -= StartVideo;
        //VideoScreenController.videoStop -= StopVideo;
        IntroAnimController.videoPlay -= StartVideo;
        SwipeMechanic.videoPlay -= ResumeVideo;
        SwipeMechanic.videoPause -= PauseVideo;
        videoplayer.loopPointReached -= CheckOver;

        EventManager.Unsubscribe(EventManager.EventType.StateDecided, OnStateDecided);

    }

    void OnStateDecided(object data)
    {
        state = (State)data;
    }

    [ContextMenu("Play")]
    private void ResumeVideo()
    {
        videoplayer.Play();
    }
    private void StartVideo()
    {
        videoplayer.frame = 0;
        videoplayer.Play();
        StartCoroutine(WaitForVideoFirstFrame());
    }

    IEnumerator WaitForVideoFirstFrame()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => videoplayer.isPlaying );

        if (state == State.Interaction)
        {
            videoplayer.SetDirectAudioMute(0, true);
            videoplayer.isLooping = true;
            if (manageTumbnailOnVideo != null)
                manageTumbnailOnVideo(false);
        }
        else
        {
            videoplayer.frame = 0;
            IntroAnimController.instance.currentAnimator.SetTrigger("PlayVideo");
        }
    }

    private void PauseVideo()
    {
        videoplayer.Pause();
    }
    private void StopVideo()
    {
        videoplayer.Stop();
        Debug.Log("stop, 1");
        videoplayer.frame = 0;
    }

    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        Debug.Log("video over");
        VideoEnded();
    }

    void VideoEnded()
    {
        Debug.Log("triger video end animation");
        if (cubeAnimator != null)
            cubeAnimator.SetTrigger("videoEnded"); // try setting bool and using update

        if (state != State.Interaction)
        {
            //if (manageTumbnailOnVideo != null)
            //{
            //    manageTumbnailOnVideo(true);
            //}
            if (scaleDownVideo != null)
                scaleDownVideo();
        }
    }


}
