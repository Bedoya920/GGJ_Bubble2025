using SignalSystem;
using System.Collections;
using System.Collections.Generic;
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
            isActive = true;
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
                isActive = false;
            }
        }
    }

}
