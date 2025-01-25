using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePlayer : MonoBehaviour
{
    float range = 5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmo2D()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, range);
    }
}
