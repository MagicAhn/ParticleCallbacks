using System;
using UnityEngine;
using System.Collections;

public class Sprinkler : MonoBehaviour
{
    // sprinkler 包含的所有 particle system
    private ParticleSystem[] sprinklers;
    // 设置 sprinkler 的高度
    private Single heightAboveFloor;
    // 用于粒子碰撞检测的 barrel
    private GameObject barrel;
    // barrel 上附加的 Fire脚本
    private Fire fire;
    // sprinkler 所有的 particlesystem 在发生粒子碰撞时所产生的 所有的 CollisionEvent（二维数组）
    private ParticleSystem.CollisionEvent[][] collisionEvents;

    void Awake()
    {
        sprinklers = GetComponentsInChildren<ParticleSystem>();
        barrel = GameObject.FindGameObjectWithTag("FireBarrel");
        fire = barrel.GetComponent<Fire>();
    }

    // Use this for initialization
    void Start()
    {
        heightAboveFloor = transform.position.y;
        collisionEvents = new ParticleSystem.CollisionEvent[sprinklers.Length][];
    }

    /// <summary>
    /// 当 水溅到 FireBarrel上，才生的 粒子碰撞
    /// </summary>
    /// <param name="other"></param>
    void OnParticleCollision(GameObject other)
    {
        if (other.tag.Equals("FireBarrel"))
        {
            // 设置 第二维的 length
            for (int i = 0; i < collisionEvents.Length; i++)
            {
                collisionEvents[i] = new ParticleSystem.CollisionEvent[sprinklers[i].safeCollisionEventSize];
            }
            // 得到每一个 ParticleSystem 所接收得到 CollisionEvent
            for (int i = 0; i < collisionEvents.Length; i++)
            {
                sprinklers[i].GetCollisionEvents(gameObject, collisionEvents[i]);
            }
            for (int i = 0; i < collisionEvents.Length; i++)
            {
                for (int j = 0; j < collisionEvents[i].Length; j++)
                {
                    // 火焰慢慢熄灭下去
                    foreach (var particleHelper in fire.particles)
                    {
                        if (particleHelper.varyAlpha)
                        {
                            particleHelper.DecreaseAlpha();
                        }
                        if (particleHelper.varyEmission)
                        {
                            particleHelper.DecreaseEmission();
                        }
                        if (particleHelper.varyIntensity)
                        {
                            particleHelper.DecreaseIntensity();
                        }
                        if (particleHelper.varyRange)
                        {
                            particleHelper.DecreaseRange();
                        }
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 鼠标 左键 长按，如果 Ray接触到了 ground，则将 Sprinkler 移到该位置的上方
        // 鼠标 左键 长按，播放所有 particle system
        // 否则，停止播放所有 particle system
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] raycastHits = Physics.RaycastAll(ray);
            foreach (var raycastHit in raycastHits)
            {
                if (raycastHit.collider.name.Equals("ground"))
                {
                    transform.position = raycastHit.point + new Vector3(0, heightAboveFloor, 0);
                }
            }
            // 判断 Particle System 是否正在播放
            if (!sprinklers[0].isPlaying)
            {
                for (int i = 0; i < sprinklers.Length; i++)
                {
                    sprinklers[i].Play();
                }
            }
        }
        else
        {
            if (sprinklers[0].isPlaying)
            {
                for (int i = 0; i < sprinklers.Length; i++)
                {
                    sprinklers[i].Stop();
                }
            }
        }
    }
}
