using SignalSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public int levelID;
    public int sceneIndex;

    public BubbleManager bubbleManager;
    public LetterController letterController;
    public BubbleSpawn bubbleSpawn;

    // public Text timerText; // anterior
    public TMP_Text timerText;
    public List<Color> colors; 
    public int totalTime = 10; 
    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        } else {
            instance = this;
        }
        
        levelID = PlayerPrefs.GetInt("levelId");
        sceneIndex = PlayerPrefs.GetInt("sceneIndex");
    }

    private void Start()
    {
        StopAllCoroutines();
        Time.timeScale = 1.0f;
        ResetScenePrefs();
        if (PlayerPrefs.GetInt("sceneIndex") > 0)
        {
            PlayerPrefs.SetInt("lastLevelId", levelID);
            PlayerPrefs.Save();
        }        
        bubbleManager.SelectLevel(levelID);
        //StartCoroutine(StartTimer());
    }


    public void NextScene()
    {
        sceneIndex++;
        levelID++;
        PlayerPrefs.SetInt("levelId", PlayerPrefs.GetInt("levelId") + 1);
        PlayerPrefs.SetInt("sceneIndex", PlayerPrefs.GetInt("sceneIndex") + 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene(PlayerPrefs.GetInt("sceneIndex"));
        //Debug.Log("levelId" + PlayerPrefs.GetInt("levelId"));        
    }

    public void Continue()
    {
        
        PlayerPrefs.SetInt("levelId", PlayerPrefs.GetInt("lastLevelId"));
        PlayerPrefs.SetInt("sceneIndex", PlayerPrefs.GetInt("lastLevelId"));
        PlayerPrefs.Save();
        SceneManager.LoadScene(PlayerPrefs.GetInt("sceneIndex"));
        //Debug.Log("levelId" + PlayerPrefs.GetInt("levelId"));
    }

    // Para probar mi rey
    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }

    public void Menu()
    {
        PlayerPrefs.DeleteKey("levelId");
        PlayerPrefs.DeleteKey("sceneIndex");
        SceneManager.LoadScene(0);
    }

    public void RestarLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        bubbleManager.SelectLevel(levelID);
        StartCoroutine(ActivateCount());
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(0.5f);

        int timeLeft = totalTime;

        while (timeLeft >= 0)
        {
            
            string timerString = timeLeft.ToString("0");

            
            string coloredText = "";
            for (int i = 0; i < timerString.Length; i++)
            {
                if (i == timerString.Length - 1) // Solo el �ltimo d�gito (el de la derecha)
                {
                    // Asigna un color de la lista al d�gito que cambia
                    Color color = colors[timeLeft % colors.Count]; // Usa el tiempo restante para elegir el color
                    coloredText += $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{timerString[i]}</color>";
                    AudioManager.instance.SelectedButtonSound();
                }
                else
                {
                    // El otro d�gito permanece con el color predeterminado (blanco o el que tenga el texto)
                    coloredText += timerString[i];
                }
            }

            
            timerText.text = coloredText;

            
            yield return new WaitForSeconds(1f);

            
            timeLeft--;
        }

        yield return new WaitForSeconds(1f);
        NextScene();
    }

    public IEnumerator ActivateCount()
    {
        timerText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        int timeLeft = totalTime;

        while (timeLeft >= 0)
        {

            string timerString = timeLeft.ToString("0");


            string coloredText = "";
            for (int i = 0; i < timerString.Length; i++)
            {
                if (i == timerString.Length - 1) // Solo el �ltimo d�gito (el de la derecha)
                {
                    // Asigna un color de la lista al d�gito que cambia
                    Color color = colors[timeLeft % colors.Count];
                    coloredText += $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{timerString[i]}</color>";
                    AudioManager.instance.SelectedButtonSound();
                }
                else
                {
                    // El otro d�gito permanece con el color predeterminado (blanco o el que tenga el texto)
                    coloredText += timerString[i];
                }
            }


            timerText.text = coloredText;


            yield return new WaitForSeconds(1f);


            timeLeft--;
        }
        bubbleManager.isActive = true;
        StartCoroutine(bubbleManager.StartLevel());
        StartCoroutine(bubbleManager.ReduceSpawnTime());
        timerText.gameObject.SetActive(false);
    }

    private void ResetScenePrefs() 
    {
        switch (sceneIndex) 
        {
            case 1:
                PlayerPrefs.SetInt("typedKeysLevel1", 0);
                PlayerPrefs.SetInt("failedKeysLevel1", 0);
                PlayerPrefs.Save();
                break;
            case 2:
                PlayerPrefs.SetInt("typedKeysLevel2", 0);
                PlayerPrefs.SetInt("failedKeysLevel2", 0);
                PlayerPrefs.Save();
                break;
            case 3:
                PlayerPrefs.SetInt("typedKeysLevel3", 0);
                PlayerPrefs.SetInt("failedKeysLevel3", 0);
                PlayerPrefs.Save();
                break;
        }        
    }
    public void StartTimerButton()
    {
        StartCoroutine(StartTimer());
    }

}
