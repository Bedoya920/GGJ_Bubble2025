using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBubble : MonoBehaviour
{
    public Transform player;
    [SerializeField] float speed;
    [SerializeField] float rangeEnemy;

    void Awake()
    {
        GameObject target = GameObject.FindWithTag("Player");
        if(target != null)
        {
            rangeEnemy = target.GetComponent<BubblePlayer>().range;
            player = target.transform;
            
        }
    }

    
    void LateUpdate()
    {
        Vector3 direction = player.position - transform.position;
        transform.position += (direction * speed) * Time.deltaTime;

        float distance = Vector3.Distance(transform.position, player.position);

        if(distance <= rangeEnemy)
        {
            //Agregar lógica de muerte
            player.gameObject.GetComponent<BubblePlayer>().TakeDamage();
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, rangeEnemy);
    }


    
}
