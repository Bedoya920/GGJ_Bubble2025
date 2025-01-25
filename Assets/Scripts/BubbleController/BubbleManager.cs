using SignalSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    [Header("Bubble Variables")]
    public bool isActive;

    [Header("Game components")]
    public LetterController letterController;
    public LetterObject currentLetter;
    public BubbleSpawn bubbleSpawn;
    // spawner

    private void Start()
    {
        
    }
    public void SelectLevel(int levelId)
    {
        letterController.SetLevelInfo(levelId);
        isActive = true;
        //mas cosas
    }

    public IEnumerator StartLevel() 
    {
        while (isActive) {
            letterController.ShuffleLettersInLevel();
            currentLetter = letterController.levelInfo.letters[0];
            bubbleSpawn.SpawnLetter(currentLetter);
            letterController.SubstractPoolLetter(currentLetter.letter);
            yield return new WaitForSeconds(2f);
            if (letterController.GetLetterCount() == 0)
            {
                isActive = false;
            }
        }    
        //Cuando finaliza el lvl?
    }




}
