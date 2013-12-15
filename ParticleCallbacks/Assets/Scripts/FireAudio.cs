using UnityEngine;
using System.Collections;

public class FireAudio : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        audio.volume = particleSystem.startColor.a * 2f;
    }
}
