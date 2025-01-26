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
        if(HUDManager.instance.isPaused == false)
        {
            transform.position += Vector3.down * speed;
        }
        
    }

}
