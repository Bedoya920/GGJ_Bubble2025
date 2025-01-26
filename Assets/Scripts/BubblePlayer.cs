using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePlayer : MonoBehaviour
{
    int life = 3;
    public float range;

    public BubbleSpawn bubbleSpawn;

    void Start()
    {
        range = 45f;
        gameObject.transform.localScale = new Vector3(8, 8, 1);
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
        HUDManager.instance.HitEffect();
        
        CameraJiggle cameraJiggle = Camera.main.GetComponent<CameraJiggle>();
        cameraJiggle.Jiggle(0.5f, 1f, 10);

        if(life == 2)
        {
            gameObject.transform.localScale = new Vector3(5, 5, 1);
            range=35f;
        }
        if(life==1)
        {
            gameObject.transform.localScale = new Vector3(3, 3, 1);
            range = 28f;

        }
        //Animación de cambio o hit
    }

    public void DeleteLetter(string letter) {
        bubbleSpawn.RemoveLetter(letter);
    }

    
    

    
}
