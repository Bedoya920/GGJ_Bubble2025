using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SignalSystem;
using TMPro;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class BubbleSpawn : MonoBehaviour
{
    public Transform[] spawners;
    public Transform[] leftSpawners;
    public Transform[] rightSpawners;
    public GameObject enemy;
    public bool isFinished;
    public LetterController letterController;
    public HUDManager hUDManager;
    public LevelManager levelManager;
    public BubblePlayer bubblePlayer;
    public TextController textController;
    public ScoreManager scoreManager;

    public List<LetterPrefab> prefabList = new List<LetterPrefab>();
    public List<InstantiateLetter> instancedBubbles;
    public List<GameObject> objectsToActivate;
    private int correctCount = 0;
    private int incorrectCount = 0;
    private int spawnedCounter = 0;
    public int streakCount = 0;


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

    private void LateUpdate()
    {
        if (letterController.isLoaded)
        {
            CheckConditions();
        }
        
    }

    private void CheckConditions()
    {
        if (letterController.levelInfo.letters.Count == 0 && instancedBubbles.Count == 0 && hUDManager.livesCanvas.Length > 0)
        {
            levelManager.NextScene();
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
            StartCoroutine(InstantiatePrefab(letterObject, spawner));
        }
    }

    private IEnumerator InstantiatePrefab(LetterObject letterObject, Transform spawner)
    { 
        Debug.Log("Buscando" + letterObject.letter);
        var prefabObject = prefabList.Find(element => element.letter == letterObject.letter);
        Debug.Log(prefabObject);
        if (prefabObject != null && prefabObject.prefab != null)
        {
            if (letterObject.specialCharacter) {
                textController.ShowTextWithFade("Se acerca un caracter especial!");
                yield return new WaitForSeconds(1f);
            }
            var prefab = Instantiate(prefabObject.prefab, spawner.position, spawner.rotation);
            InstantiateLetter newLetter = new InstantiateLetter();
            string targetLetter = letterObject.letter;
            switch (targetLetter)
            {
                case "coma":
                    targetLetter = ",";
                    break;
                case "dot":
                    targetLetter = ".";
                    break;
                case "space":
                    targetLetter = " ";
                    break;
            }
            newLetter.instanceLetter = targetLetter;
            newLetter.instance = prefab;
            instancedBubbles.Add(newLetter);
        }
        yield return null;
    }
    public void CheckLetter(string letter)
    {
        var bubbleToRemove = instancedBubbles.Find(bubble => bubble.instanceLetter  == letter);

        if (bubbleToRemove != null)
        {
            instancedBubbles.Remove(bubbleToRemove);
            Destroy(bubbleToRemove.instance);
            spawnedCounter += 1;
            correctCount++;
            streakCount++;
            Debug.Log($"Acierto: {letter}. Aciertos totales: {correctCount}");
        }
        else
        {
            incorrectCount++;
            streakCount = 0;
            Debug.Log($"Error: {letter}. Errores totales: {incorrectCount}");
            bubblePlayer.TakeDamage();
        }
        Debug.Log($"Racha: {streakCount}.");

        if (streakCount % 5 == 0)
        {
            StreakSystem();
        }

        scoreManager.IncreaseScore(streakCount);
        scoreManager.SetAccuracy(correctCount, incorrectCount);
        scoreManager.SetRemaining(spawnedCounter, letterController.total);
        textController.UpdateCanvasScore();
    }

    public void StreakSystem()
    {
   
        int index = streakCount == 0 ? 0 : (streakCount / 5) - 1;

        if (index < objectsToActivate.Count && objectsToActivate[index] != null)
        {
            objectsToActivate[index].SetActive(true);
        }
    }

    public void RemoveLetter(string letter)
    {
        var bubbleToRemove = instancedBubbles.Find(bubble => bubble.instanceLetter == letter);

        if (bubbleToRemove != null)
        {
            instancedBubbles.Remove(bubbleToRemove);
            Destroy(bubbleToRemove.instance);
            spawnedCounter += 1;
            incorrectCount++;
            scoreManager.SetRemaining(spawnedCounter, letterController.total);
            textController.UpdateCanvasScore();
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


