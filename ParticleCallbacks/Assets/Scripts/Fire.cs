using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour
{
    public List<ParticleHelper> particles;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 火焰慢慢燃烧起来
        foreach (var particleHelper in particles)
        {
            if (particleHelper.varyAlpha)
            {
                particleHelper.IncreaseAlpha();
            }
            if (particleHelper.varyEmission)
            {
                particleHelper.IncreaseEmission();
            }
            if (particleHelper.varyIntensity)
            {
                particleHelper.IncreaseIntensity();
            }
            if (particleHelper.varyRange)
            {
                particleHelper.IncreaseRange();
            }
        }
    }
}
