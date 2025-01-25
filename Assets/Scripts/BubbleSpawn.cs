using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawn : MonoBehaviour
{
    public Transform[] spawners;
    public Transform[] leftSpawners;
    public Transform[] rightSpawners;
    public GameObject enemy;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RandomSpawner();
        }
    }

    void RandomSpawner() //Poner parametro para la letra o pasar el prefab al spawner
    {
        int randomNum = Random.Range(0, spawners.Length);
        Instantiate(enemy, spawners[randomNum].position, spawners[randomNum].rotation);
    }

    public void SpawnLetter(string position)
    {
        // si definimos los mismos numeros de spawn en cada posicion. int randomNum = Random.Range(0, leftSpawners.Length);
        if (position == "left")
        {
            int randomNum = Random.Range(0, leftSpawners.Length);
            Instantiate(enemy, leftSpawners[randomNum].position, leftSpawners[randomNum].rotation);
        }
        else if (position == "right") {
            int randomNum = Random.Range(0, rightSpawners.Length);
            Instantiate(enemy, rightSpawners[randomNum].position, rightSpawners[randomNum].rotation);
        }
    }
}


