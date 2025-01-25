using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int levelID;
    public bool nextLevel;
    public int sceneIndex;
    public int currentSceneIndex;

    private void Start()
    {
        sceneIndex = 0;
    }

    private void Update()
    {
        if(nextLevel)
        {
            if (sceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                sceneIndex++;
                levelID = sceneIndex;
                SceneManager.LoadScene(sceneIndex);
                nextLevel = false;
                
            }
        }
    }

    public void NextScene()
    {
        sceneIndex++;
        SceneManager.LoadScene(sceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }



}
