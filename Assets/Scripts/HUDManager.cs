using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;
    public GameObject gameOverCanvas;
    public GameObject[] livesCanvas;


    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject); // Evita duplicados eliminando esta instancia
            return;
        } else {
            instance = this;
        }
    }

    void Update()
    {
        
    }

    public void UpdateUI(int life)
    {
        if(life == 0)
        {
            livesCanvas[life].SetActive(false);
        } else {
            livesCanvas[life-1].SetActive(false);
        }
        
    }

    public void GameOver()
    {
        gameOverCanvas.SetActive(true);
    }
}
