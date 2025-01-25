using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int levelID;
    public int sceneIndex;

    public BubbleManager bubbleManager;

    private void Awake()
    {
        levelID = PlayerPrefs.GetInt("levelId");
        sceneIndex = PlayerPrefs.GetInt("sceneIndex");
    }

    private void Start()
    {
        Debug.Log("levelId" + levelID);
        bubbleManager.SelectLevel(levelID);
        StartCoroutine(bubbleManager.StartLevel());
    }

    public void NextScene()
    {
        sceneIndex++;
        levelID++;
        PlayerPrefs.SetInt("levelId", PlayerPrefs.GetInt("levelId") + 1);
        PlayerPrefs.SetInt("sceneIndex", PlayerPrefs.GetInt("sceneIndex") + 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene(PlayerPrefs.GetInt("sceneIndex"));
        Debug.Log("levelId" + PlayerPrefs.GetInt("levelId"));        
    }

    // Para probar mi rey
    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
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
