using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SignalSystem;
using TMPro;



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
    public NextLevelMenu nextLevelMenu;
    public BubbleManager bubbleManager;

    public List<LetterPrefab> prefabList = new List<LetterPrefab>();
    public List<InstantiateLetter> instancedBubbles;
    public List<GameObject> objectsToActivate;
    public int correctCount = 0;
    public int incorrectCount = 0;
    public int spawnedCounter = 0;
    public int streakCount = 0;
    public GameObject healEffect;
    public GameObject[] animBubble;
    private int randomIndex;
    private bool isSpawning;


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
        if (letterController.levelInfo.letters.Count == 0 && instancedBubbles.Count == 0 && hUDManager.livesCanvas.Length > 0 && !isSpawning && bubblePlayer.life > 0 && !bubblePlayer.isTakingDamage)
        {
            switch (PlayerPrefs.GetInt("sceneIndex"))
            {
                case 1:
                    PlayerPrefs.SetInt("typedKeysLevel1", correctCount + incorrectCount);
                    PlayerPrefs.SetInt("failedKeysLevel1", incorrectCount);
                    PlayerPrefs.Save();
                    break;
                case 2:
                    PlayerPrefs.SetInt("typedKeysLevel2", correctCount + incorrectCount);
                    PlayerPrefs.SetInt("failedKeysLevel2", incorrectCount);
                    PlayerPrefs.Save();
                    break;
                case 3:
                    PlayerPrefs.SetInt("typedKeysLevel3", correctCount + incorrectCount);
                    PlayerPrefs.SetInt("failedKeysLevel3", incorrectCount);
                    PlayerPrefs.Save();
                    break;
            }
            PlayerPrefs.SetInt("lastLevelId", levelManager.levelID + 1);
            bubbleManager.isActive = false;
            nextLevelMenu.gameObject.SetActive(true);
        }
    }

    void RandomSpawner()
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
            isSpawning = true;
            if (letterObject.specialCharacter) {
                textController.ShowTextWithFade("A special character is approaching!");
                Debug.Log(letterController.levelInfo.letters.Count == 0 && instancedBubbles.Count == 0 && hUDManager.livesCanvas.Length > 0);
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
            isSpawning = false;
        }
        yield return null;
    }
    public void CheckLetter(string letter)
    {
        int lifesCount = 0;
        for (int i = 0; i < hUDManager.livesCanvas.Length; i++)
        {
            if (hUDManager.livesCanvas != null && hUDManager.livesCanvas[i].activeInHierarchy)
            {
                lifesCount++;
            }
        }
        if (lifesCount == 0 || !bubbleManager.isActive)
        {
            return;
        }

        InstantiateLetter bubbleToRemove = instancedBubbles.Find(bubble => bubble.instanceLetter  == letter);

        if (bubbleToRemove != null)
        {
            AudioManager.instance.PlayRandomAudio();
            AnimRandom();
            Instantiate(animBubble[randomIndex], bubbleToRemove.instance.gameObject.transform.position, bubbleToRemove.instance.gameObject.transform.rotation);
            instancedBubbles.Remove(bubbleToRemove);
            Destroy(bubbleToRemove.instance);
            spawnedCounter += 1;
            correctCount++;
            streakCount++;
            Debug.Log($"Acierto: {letter}. Aciertos totales: {correctCount}");
        } else
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.failureSound);
            bubblePlayer.TakeDamage();
        } 
        Debug.Log($"Racha: {streakCount}.");

        if (streakCount % 5 == 0 && streakCount != 0)
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

        int index = streakCount == 0 ? 0 : ((streakCount - 1) % 25 / 5);

        if (index < objectsToActivate.Count && objectsToActivate[index] != null)
        {
            objectsToActivate[index].SetActive(true);
            
            if (index == objectsToActivate.Count - 1)
            {
                StartCoroutine(StreakEnumerator());
                bubblePlayer.Heal();
            }

        }
    }

    private IEnumerator StreakEnumerator()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.healSound);
        healEffect.SetActive(true);
        Invoke("DisableHealEffect", 0.5f);
        yield return new WaitForSeconds(1f);

        foreach (GameObject item in objectsToActivate)
        {

            item.SetActive(false);

        }

    }

    void DisableHealEffect()
    {
        healEffect.SetActive(false);
    }

    public void DisableStreakUI()
    {
        foreach (GameObject item in objectsToActivate)
        {
            item.SetActive(false);
        }
    }




    public void RemoveLetter(string letter)
    {
        var bubbleToRemove = instancedBubbles.Find(bubble => bubble.instanceLetter == letter);

        if (bubbleToRemove != null)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.failureSound);
            instancedBubbles.Remove(bubbleToRemove);
            Destroy(bubbleToRemove.instance);
            spawnedCounter += 1;
            incorrectCount++;
            scoreManager.SetRemaining(spawnedCounter, letterController.total);
            textController.UpdateCanvasScore();
        }
    }

    public void AnimRandom()
    {
        if (animBubble.Length > 0)
        {

            randomIndex = Random.Range(0, animBubble.Length);


            
            
        }
        else
        {
            Debug.LogWarning("No hay GameObjects en la lista.");
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


