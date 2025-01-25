using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;
    public GameObject[] livesCanvas;
    public GameObject gameOverCanvas;

    public GameObject hitEffect;
    


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

    public void HitEffect()
    {
        hitEffect.SetActive(true);
        Invoke("DisableHitEffect", 0.5f);
    }

    void DisableHitEffect()
    {
        hitEffect.SetActive(false);
    }
}
