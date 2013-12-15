using System;
using UnityEngine;
using System.Collections;

public class FadeInAudio : MonoBehaviour
{
    public Single fade = 0.0f;

    void Awake()
    {
        AudioListener.volume = 0;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.volume = fade;
    }
}
