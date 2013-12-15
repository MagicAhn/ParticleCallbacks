using System;
using UnityEngine;
using System.Collections;

public class DropletImpactAudio : MonoBehaviour
{
    // 水滴击打 Lid的声音
    public AudioClip[] waterHits;
    // 播放声音的概率 （因为 产生的 粒子碰撞实在太多了）
    public Int32 playOnIn;

    private ParticleSystem[] sprinklers;
    private ParticleSystem.CollisionEvent[][] collisionEvents;

    void Awake()
    {
        sprinklers = GetComponentsInChildren<ParticleSystem>();
    }

    // Use this for initialization
    void Start()
    {
        collisionEvents = new ParticleSystem.CollisionEvent[sprinklers.Length][];
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnParticleCollision(GameObject other)
    {
        // particlesystem 击中 Tag 为 Barrel 或者 FireBarrel的 gameobject时 发出声音
        if (other.tag.Equals("Barrel") || other.tag.Equals("FireBarrel"))
        {
            for (int i = 0; i < collisionEvents.Length; i++)
            {
                collisionEvents[i] = new ParticleSystem.CollisionEvent[sprinklers[i].safeCollisionEventSize];
            }
            for (int i = 0; i < collisionEvents.Length; i++)
            {
                sprinklers[i].GetCollisionEvents(gameObject, collisionEvents[i]);
            }
            for (int i = 0; i < collisionEvents.Length; i++)
            {
                for (int j = 0; j < collisionEvents[i].Length; j++)
                {
                    Int32 chance = UnityEngine.Random.Range(0, playOnIn);
                    // 凑巧撞上了，那就播发一段 clip吧
                    if (chance == playOnIn - 1)
                    {
                        // 
                        Int32 clipIndex = UnityEngine.Random.Range(0, waterHits.Length);
                        AudioSource.PlayClipAtPoint(waterHits[clipIndex], collisionEvents[i][j].intersection);
                    }
                }
            }
        }
    }
}
