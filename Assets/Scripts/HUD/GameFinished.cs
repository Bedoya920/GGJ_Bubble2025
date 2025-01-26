using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFinished : MonoBehaviour
{
    public TextController textController;    
    void Start()
    {
        textController.UpdateStatsCanvas();        
    }

    public void GoMenu()
    {
        LevelManager.instance.Menu();
    }
}
