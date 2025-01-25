using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawn : MonoBehaviour
{
    public Transform[] spawners;
    public GameObject enemy;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RandomSpawner();
        }
    }

    void RandomSpawner() //Poner parametro para la letra o pasar el prefab al spawner
    {
        int randomNum = Random.Range(0, spawners.Length);
        Instantiate(enemy, spawners[randomNum].position, spawners[randomNum].rotation);
    }
}
