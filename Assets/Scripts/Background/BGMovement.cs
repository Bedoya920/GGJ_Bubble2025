using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMovement : MonoBehaviour
{
    public float speed = 6f;

    void Update()
    {
        float moveAmount = speed *Time.deltaTime;
        transform.position += new Vector3(0, moveAmount, 0);
    }
}
