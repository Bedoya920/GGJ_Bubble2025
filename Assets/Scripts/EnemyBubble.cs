using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBubble : MonoBehaviour
{
    public Transform target;
    [SerializeField] float speed;
    [SerializeField] float range;

    void Start()
    {
        
    }

    
    void Update()
    {
        Vector3 direction = target.position - transform.position;
        transform.position += (direction * speed) * Time.deltaTime;

        float distance = Vector3.Distance(transform.position, target.position);

        if(distance <= range)
        {
            Debug.Log("Llegué");
            Destroy(gameObject);
        }
    }

    
}
