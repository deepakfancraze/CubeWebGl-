using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Video;

public class VideoLoader : MonoBehaviour
{
    

    static IEnumerator LoadVideoCoroutine(Action<VideoClip> action, string url)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        //action?.Invoke(www.downloadHandler.data)
    }
}
