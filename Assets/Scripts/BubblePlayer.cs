using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePlayer : MonoBehaviour
{
    public GameObject[] lifeCanvas;
    public GameObject gameOverCanvas;
    int life = 3;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        UpdateUI();
        life--;
        if(life <= 0)
        {
            Debug.Log("Perdiste");
            Time.timeScale = 0f;
            gameOverCanvas.SetActive(true);
        }
        //Animación de cambio o hit
    }

    void UpdateUI()
    {
        if(life == 0)
        {
            lifeCanvas[life].SetActive(false);
        } else {
            lifeCanvas[life-1].SetActive(false);
        }
        
    }

    
}
