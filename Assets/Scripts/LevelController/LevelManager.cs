using SignalSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int levelID;
    public int sceneIndex;

    public BubbleManager bubbleManager;
    public LetterController letterController;
    public BubbleSpawn bubbleSpawn;

    public Text timerText; 
    public List<Color> colors; 
    public int totalTime = 10; 
    private void Awake()
    {
        levelID = PlayerPrefs.GetInt("levelId");
        sceneIndex = PlayerPrefs.GetInt("sceneIndex");
    }

    private void Start()
    {
        Debug.Log("levelId" + levelID);
        bubbleManager.SelectLevel(levelID);
        StartCoroutine(bubbleManager.StartLevel());
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
        Debug.Log("levelId" + PlayerPrefs.GetInt("levelId"));        
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
        SceneManager.LoadScene(0);
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
    public void StartTimerButton()
    {
        StartCoroutine(StartTimer());
    }

}
