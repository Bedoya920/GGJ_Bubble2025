using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public GameObject[] objects;
    public GameObject[] spawners;
    [SerializeField]float cooldownObj;
    public GameObject objectContainer;

    void Start()
    {
        InvokeRepeating("CreateObjects", 1f, cooldownObj);
    }

    void CreateObjects()
    {
        int numRandom1 = Random.Range(0, objects.Length);
        int numRandom2 = Random.Range(0, spawners.Length);

        GameObject instantiatedObject = Instantiate(objects[numRandom1], spawners[numRandom2].transform.position, spawners[numRandom2].transform.rotation);
        instantiatedObject.transform.SetParent(objectContainer.transform);
    }
}
