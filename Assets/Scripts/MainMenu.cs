using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Variables públicas
    bool isOld;
    public LevelManager levelM; 

    [Header("Canvas")]
    public GameObject hTPCanva; 
    public GameObject menuCanva; 

    void Start()
    {
        PlayerPrefs.DeleteAll();
        isOld = PlayerPrefs.GetInt("isOld") == 1; 
    }


    public void StartGame()
    {
        
        if (!isOld)
        {
            hTPCanva.SetActive(true);
            menuCanva.SetActive(false);
            PlayerPrefs.SetInt("isOld", 1); 
        }
        else
        {
            levelM.NextScene(); 
        }
    }
}