using SignalSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BubblePlayer : MonoBehaviour
{
    public int life = 3;
    public float range;

    public BubbleSpawn bubbleSpawn;
    public GameObject muerteAnim;
    public ScoreManager scoreManager;
    public TextController textController;
    public bool isTakingDamage;

    void Start()
    {
        range = 45f;
        gameObject.transform.localScale = new Vector3(8, 8, 1);
    }

    public void TakeDamage()
    {
        isTakingDamage = true;
        bubbleSpawn.incorrectCount++;
        bubbleSpawn.DisableStreakUI();
        bubbleSpawn.streakCount = 0;
        Debug.Log($"Errores totales: {bubbleSpawn.incorrectCount}");
        if (life > 0) {
            HUDManager.instance.UpdateUI(life);
            life--;
        }
        scoreManager.IncreaseScore(bubbleSpawn.streakCount);
        scoreManager.SetAccuracy(bubbleSpawn.correctCount, bubbleSpawn.incorrectCount);
        scoreManager.SetRemaining(bubbleSpawn.spawnedCounter, bubbleSpawn.letterController.total);
        textController.UpdateCanvasScore();

        if (life <= 0)
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
        isTakingDamage = false;
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
