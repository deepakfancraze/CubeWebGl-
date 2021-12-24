using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AudioLoader : MonoBehaviour
{

    AudioSource audioSource;
    AudioClip myClip;
    static AudioLoader instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Debug.Log("Starting to download the audio...");
    }

    public static void GetAudio(Action ac, string isMute, string url)
    {
        if (!string.IsNullOrEmpty(url))
        {
            instance.StartCoroutine(instance.GetAudioClip(url, ac));
        }
        //if (isMute == "FALSE")
        //{
        //}
        //else
        //{
        //    instance.audioSource.clip = null;
        //}
    }
    private void OnEnable()
    {
        CubeDataController.onEverythingLoaded += OnEverythingLoaded;
    }

    private void OnDisable()
    {

        CubeDataController.onEverythingLoaded -= OnEverythingLoaded;
    }

    void OnEverythingLoaded()
    {

    }

    IEnumerator GetAudioClip(string url, Action ac)
    {
        //Debug.LogError("Audio Url _________"+url);
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                myClip = DownloadHandlerAudioClip.GetContent(www);
                audioSource.clip = myClip;
                ac?.Invoke();
                Debug.Log("Audio is playing.");
            }
        }
    }


    // public void pauseAudio(){
    //     audioSource.Pause();
    // }

    // public void playAudio(){
    //     audioSource.Play();
    // }

    // public void stopAudio(){
    //     audioSource.Stop();

    // }
}