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
                SceneManager.LoadScene(sceneIndex);
                nextLevel = false;
            }
        }
    }

    private void NextScene()
    {
        sceneIndex++;
        SceneManager.LoadScene(sceneIndex);
    }



}
