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
        // las burbujas se spawnean aleatoriamente.
        while (isActive) {
            letterController.ShuffleLettersInLevel();
            currentLetter = letterController.levelInfo.letters[0];
            // FUNCION SPAWNER (LetterObject letter)
            letterController.SubstractPoolLetter(currentLetter.letter);
            yield return new WaitForSeconds(1f);
            if (letterController.GetLetterCount() == 0)
            {
                isActive = false;
            }
        }           
    }




}
