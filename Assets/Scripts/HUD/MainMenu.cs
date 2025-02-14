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

    [Header("Botón")]
    public GameObject hTPButton;
    public GameObject continueButton;

    void Start()
    {
        
        PlayerPrefs.DeleteKey("levelId");
        PlayerPrefs.DeleteKey("sceneIndex");
        isOld = PlayerPrefs.GetInt("isOld") == 1; 
    

        if(isOld)
        {
            hTPButton.SetActive(true);
            continueButton.SetActive(true);
        }
    }

    public void StartGame()
    {
        
        if (!isOld)
        {
            hTPCanva.SetActive(true);
            menuCanva.SetActive(false);
            PlayerPrefs.SetInt("isOld", 1); 
            isOld = true;
        }
        else
        {
            levelM.NextScene(); 
        }
    }
}