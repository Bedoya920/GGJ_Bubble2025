using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePlayer : MonoBehaviour
{
    int life = 3;
    public float range;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        HUDManager.instance.UpdateUI(life);
        
        life--;
        if(life <= 0)
        {
            Debug.Log("Perdiste");
            Time.timeScale = 0f;
            HUDManager.instance.GameOver();
            
        }
        //Animación de cambio o hit
    }

    
    

    
}
