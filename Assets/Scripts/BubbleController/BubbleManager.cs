using SignalSystem;
using System;
using System.Collections;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    [Header("Bubble Variables")]
    public bool isActive;
    public float letterSpawnTime;

    [Header("Game components")]
    public LetterController letterController;
    public LetterObject currentLetter;
    public BubbleSpawn bubbleSpawn;
    public LevelManager levelManager;

    public void SelectLevel(int levelId)
    {
        if (letterController != null)
        {
            letterController.SetLevelInfo(levelId);
            StartCoroutine(levelManager.ActivateCount());
        }
    }

    public IEnumerator StartLevel() 
    {
        while (isActive) {
            letterController.ShuffleLettersInLevel();
            currentLetter = letterController.levelInfo.letters[0];
            bubbleSpawn.SpawnLetter(currentLetter);
            letterController.SubstractPoolLetter(currentLetter.letter);
            yield return new WaitForSeconds(letterSpawnTime);
            if (letterController.GetLetterCount() == 0)
            {
                break;
            }
        }
    }

    public IEnumerator ReduceSpawnTime()
    {
        float startSpawnTime = 2f;
        float spawnTimeLimit = 1f;
        float reduceTime = 5f;
        switch (PlayerPrefs.GetInt("levelId"))
        {
            case 1:
                startSpawnTime = 2f;
                spawnTimeLimit = 1f;
                reduceTime = 5f;
                break;
            case 2:
                startSpawnTime = 1.6f;
                spawnTimeLimit = 0.85f;
                reduceTime = 4f;
                break;
            case 3:
                startSpawnTime = 1.2f;
                spawnTimeLimit = 0.7f;
                reduceTime = 3f;
                break;
        }
        letterSpawnTime = startSpawnTime;
        while (letterSpawnTime > spawnTimeLimit)
        {
            yield return new WaitForSeconds(reduceTime);
            letterSpawnTime = Math.Min(letterSpawnTime - 0.2f, spawnTimeLimit);
        }
    }

}
