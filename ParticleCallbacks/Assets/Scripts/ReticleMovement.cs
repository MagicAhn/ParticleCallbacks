using UnityEngine;
using System.Collections;

/// <summary>
/// 通过鼠标 控制 Projector移动
/// </summary>
public class ReticleMovement : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 确定 projector的 位置
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);
        foreach (var raycastHit in hits)
        {
            if (raycastHit.collider.name.Equals("ground"))
            {
                transform.position = new Vector3(raycastHit.point.x,5f,raycastHit.point.z);
            }
        }
    }
}
