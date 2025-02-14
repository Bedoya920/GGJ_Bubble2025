using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;

    [Header("Canvas")]
    public GameObject[] livesCanvas;
    public GameObject gameOverCanvas;
    public GameObject pauseCanvas;


    public GameObject hitEffect;

    public bool isPaused = false;

    public BubbleSpawn bubbleSpawn;



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

    private void Start()
    {
        bubbleSpawn.nextLevelOn = false;
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause(); 
        }
    }

    void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame(); 
        }
        else
        {
            PauseGame(); 
        }
    }

    void PauseGame()
    {
        if(!bubbleSpawn.nextLevelOn)
        {
            Time.timeScale = 0;
            isPaused = true;
            pauseCanvas.SetActive(true);
            Debug.Log("Juego pausado");
        }

    }

    void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        RestartCanvas();
        pauseCanvas.SetActive(false);
        Debug.Log("Juego reanudado");
    }

    public void RestartCanvas() {
        if (pauseCanvas != null)
        {
            Transform quitMenu = pauseCanvas.transform.Find("QuitMenu");
            Transform mainMenu = pauseCanvas.transform.Find("Menu");
            Transform configMenu = pauseCanvas.transform.Find("Config");
            Transform htpMenu = pauseCanvas.transform.Find("ComoJugar");
            if (quitMenu != null && mainMenu != null && configMenu != null && htpMenu != null)
            {
                HTPController menuhtp = htpMenu.GetComponent<HTPController>();
                quitMenu.gameObject.SetActive(false);
                configMenu.gameObject.SetActive(false);
                if (menuhtp != null) {
                    Debug.Log("htp", menuhtp);
                    menuhtp.FinalSlide();
                }
                mainMenu.gameObject.SetActive(true);
                
            }
            else
            {
                Debug.LogWarning("No se encontró el GameObject 'QuitMenu' dentro de 'Game Paused'.");
            }
        }
        else
        {
            Debug.LogWarning("No se encontró el GameObject 'Game Paused'.");
        }
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

    public void UpdateHealUI(int life)
    {
        if (life == 1)
        {
            livesCanvas[life].SetActive(true);
        }
        else if (life == 2)
        {
            livesCanvas[life].SetActive(true);
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

    void ShowPause()
    {
        pauseCanvas.SetActive(true);
    }

    

    

}
