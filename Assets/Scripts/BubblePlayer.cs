using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePlayer : MonoBehaviour
{
    int life = 3;
    public float range;

    public BubbleSpawn bubbleSpawn;

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
        HUDManager.instance.HitEffect();
        //Animación de cambio o hit
    }

    public void DeleteLetter(string letter) {
        bubbleSpawn.RemoveLetter(letter);
    }

    
    

    
}
