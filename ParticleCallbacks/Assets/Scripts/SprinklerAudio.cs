using System;
using System.Configuration;
using UnityEngine;
using System.Collections;

/// <summary>
/// sprinkler 洒水的声音
/// </summary>
public class SprinklerAudio : MonoBehaviour
{
    // 音量渐退的速度
    public Single fadeSpeed;
    public AudioSource audioStart;
    public AudioSource audioLoop;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 按下 鼠标左键 播放 audioStart
        // 松开 鼠标左键 播放 audioLoop
        if (Input.GetMouseButtonDown(0))
        {
            audioStart.Play();

            StopCoroutine("FadeOut");
            audioLoop.volume = 1;
            audioLoop.PlayDelayed(audioStart.clip.length);
        }
        if(Input.GetMouseButtonUp(0))
        {
            audioStart.Stop();

            StartCoroutine(FadeOut());
        }
    }

    // 异步实现 音量的 fadeout
    IEnumerator FadeOut()
    {
        while (audioLoop.volume > 0f)
        {
            audioLoop.volume -= fadeSpeed * Time.deltaTime;
            yield return null;
        }
    }
}
