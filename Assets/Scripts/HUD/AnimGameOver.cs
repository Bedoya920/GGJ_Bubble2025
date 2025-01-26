using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimGameOver : MonoBehaviour
{
    public void CallPause()
    {
        LevelManager.instance.PauseGame();
    }
}
