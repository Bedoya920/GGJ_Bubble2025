using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePlayer : MonoBehaviour
{
    public int life = 3;
    public float range;

    public BubbleSpawn bubbleSpawn;
    public GameObject muerteAnim;

    void Start()
    {
        range = 45f;
        gameObject.transform.localScale = new Vector3(8, 8, 1);
    }

    public void TakeDamage()
    {
        if (life > 0) {
            HUDManager.instance.UpdateUI(life);
            life--;
        }
        
        if(life <= 0)
        {
            //Parte cuando pierde
            muerteAnim.SetActive(true);
            HUDManager.instance.GameOver();
            
        }
        HUDManager.instance.HitEffect();
        
        CameraJiggle cameraJiggle = Camera.main.GetComponent<CameraJiggle>();
        cameraJiggle.Jiggle(0.5f, 1f, 10);

        if(life == 2)
        {
            gameObject.transform.localScale = new Vector3(5, 5, 1);
            range = 30f;
        }
        if(life==1)
        {
            gameObject.transform.localScale = new Vector3(3, 3, 1);
            range = 25f;

        }
        //Animación de cambio o hit
    }

    public void Heal()
    {
        if(life > 2) 
        {
            return;        
        }

        HUDManager.instance.UpdateHealUI(life);

        life++;

        if (life == 2)
        {
            gameObject.transform.localScale = new Vector3(5, 5, 1);
            range = 35f;
        }
        if (life == 3)
        {
            gameObject.transform.localScale = new Vector3(8, 8, 1);
            range = 45f;

        }
        //Animación de cambio o hit
    }

    public void DeleteLetter(string letter) {
        bubbleSpawn.RemoveLetter(letter);
    }

    

    
    

    
}
