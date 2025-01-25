using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundObjects : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float timeAlive;

    void Start()
    {
        Destroy(gameObject, timeAlive);
    }

    void Update()
    {
        transform.position += Vector3.down * speed;
    }

}
