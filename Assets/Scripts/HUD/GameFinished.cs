using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameFinished : MonoBehaviour
{
    public TextController textController;  
    public Button buttonGoHome;

    void Start()
    {
        textController.UpdateStatsCanvas();     
        Invoke("ActivateButton",8.5f);   
    }

    public void GoMenu()
    {
        SceneManager.LoadScene(0);
    }

    void ActivateButton()
    {
        buttonGoHome.interactable = true;
    }
}
