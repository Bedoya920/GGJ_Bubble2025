using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SignalSystem;

public class BubbleSpawn : MonoBehaviour
{
    public Transform[] spawners;
    public Transform[] leftSpawners;
    public Transform[] rightSpawners;
    public GameObject enemy;

    public List<LetterPrefab> prefabList = new List<LetterPrefab>();
    public List<InstantiateLetter> instancedBubbles;

    private int correctCount = 0;
    private int incorrectCount = 0;

    void Start()
    {

    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            // Obtener la tecla presionada
            string keyPressed = Input.inputString.ToLower();

            if (!string.IsNullOrEmpty(keyPressed))
            {
                CheckLetter(keyPressed);
            }
        }
    }

    void RandomSpawner() //Poner parametro para la letra o pasar el prefab al spawner
    {
        int randomNum = Random.Range(0, spawners.Length);
        Instantiate(enemy, spawners[randomNum].position, spawners[randomNum].rotation);
    }

    public void SpawnLetter(LetterObject letterObject)
    {
        Transform spawner = null;

        if (letterObject.position == "left")
        {
            int randomNum = Random.Range(0, leftSpawners.Length);
            spawner = leftSpawners[randomNum];
        }
        else if (letterObject.position == "right")
        {
            int randomNum = Random.Range(0, rightSpawners.Length);
            spawner = rightSpawners[randomNum];
        }

        if (spawner != null)
        {
            InstantiatePrefab(letterObject, spawner);
        }
    }

    private void InstantiatePrefab(LetterObject letterObject, Transform spawner)
    {
        var prefabObject = prefabList.Find(element => element.letter == letterObject.letter);
        // si definimos los mismos numeros de spawn en cada posicion. int randomNum = Random.Range(0, leftSpawners.Length);
        if (prefabObject != null && prefabObject.prefab != null)
        {
            var prefab = Instantiate(prefabObject.prefab, spawner.position, spawner.rotation);
            InstantiateLetter newLetter = new InstantiateLetter();
            newLetter.instanceLetter = letterObject.letter;
            newLetter.instance = prefab;
            instancedBubbles.Add(newLetter);
        }
    }
    private void CheckLetter(string letter)
    {
        var bubbleToRemove = instancedBubbles.Find(bubble => bubble.instanceLetter  == letter);

        if (bubbleToRemove != null)
        {
            instancedBubbles.Remove(bubbleToRemove);
            Destroy(bubbleToRemove.instance);
            correctCount++;
            Debug.Log($"Acierto: {letter}. Aciertos totales: {correctCount}");
        }
        else
        {
            // Si no se encuentra, sumar a la variable de errores
            incorrectCount++;
            Debug.Log($"Error: {letter}. Errores totales: {incorrectCount}");
        }
    }
}

[System.Serializable]
public class LetterPrefab
{
    public string letter;
    public GameObject prefab;
}

[System.Serializable]
public class InstantiateLetter
{
    public string instanceLetter;
    public GameObject instance;
}


