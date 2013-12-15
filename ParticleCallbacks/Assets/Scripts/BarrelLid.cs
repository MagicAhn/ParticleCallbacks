using System;
using UnityEngine;
using System.Collections;

/// <summary>
/// 水滴击打 Barrel Lid
/// </summary>
public class BarrelLid : MonoBehaviour
{
    // 水滴 击打 Barrel Lid的 力
    public Single dropletForce;
    // Lid 上的 rigidbody
    public Rigidbody rig;
    // sprinkler 所有的 ParticleSystem
    private ParticleSystem[] sprinklers;
    // sprinkler 所有的 ParticleSystem在发生 粒子碰撞时的 collisionEvents
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
        // 水滴只有滴在 Barrel Lid上才会有的 效果
        if (other.tag.Equals("Barrel"))
        {
            // 设置 第二位 的 length
            for (int i = 0; i < collisionEvents.Length; i++)
            {
                collisionEvents[i] = new ParticleSystem.CollisionEvent[sprinklers[i].safeCollisionEventSize];
            }
            for (int i = 0; i < collisionEvents.Length; i++)
            {
                sprinklers[i].GetCollisionEvents(gameObject, collisionEvents[i]);
            }
            // 给 水滴集中Lid的 点 一个 向下的力（所以 Lid 必须是 rigidbody）
            // collisionEvent[i][j].intersection 即为这个点
            for (int i = 0; i < collisionEvents.Length; i++)
            {
                for (int j = 0; j < collisionEvents[i].Length; j++)
                {
                    rig.AddForceAtPosition(Vector3.down * dropletForce, collisionEvents[i][j].intersection);
                }
            }
        }
    }
}
