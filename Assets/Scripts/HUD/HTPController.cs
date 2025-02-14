using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HTPController : MonoBehaviour
{
    public GameObject[] slides;
    int slideIndex;
    int currentScene;
    [SerializeField]LevelManager levelM;
    [SerializeField]GameObject countdownCanvas;

    [SerializeField]GameObject menuCanvas;
    [SerializeField]GameObject htpCanvas;


    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        foreach(GameObject slide in slides)
        {
            slide.SetActive(false);
        }
        slideIndex = 0;
        slides[slideIndex].SetActive(true);

    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow) && slideIndex < 7 && currentScene == 0)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.selectedButtom);
            slides[slideIndex].SetActive(false);
            slideIndex++;
            slides[slideIndex].SetActive(true);            
            
        }else if(Input.GetKeyDown(KeyCode.RightArrow) && slideIndex < 7 && currentScene != 0){
            AudioManager.instance.PlaySFX(AudioManager.instance.selectedButtom);
            slides[slideIndex].SetActive(false);
            slideIndex++;
            slides[slideIndex].SetActive(true);
        } else if(Input.GetKeyDown(KeyCode.RightArrow) && slideIndex == 7){
            AudioManager.instance.PlaySFX(AudioManager.instance.selectedButtom);
            FinalSlide();
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow) && slideIndex > 0)
        {
            LeftArrowAction();
        } else if (currentScene != 0 && Input.GetKeyDown(KeyCode.LeftArrow) && slideIndex == 0)
        {
            FinalSlide();
        }
    }

    public void ResetSlides()
    {
        foreach(GameObject slide in slides)
        {
            slide.SetActive(false);
        }
        slideIndex = 0;
        slides[slideIndex].SetActive(true);
    }

    void StartCountdown()
    {
        countdownCanvas.SetActive(true);
        this.gameObject.SetActive(false);
        levelM.StartTimerButton();
    }

    public void FinalSlide()
    {
        menuCanvas.SetActive(true);
        ResetSlides();
        htpCanvas.SetActive(false);
    }

    public void LeftArrowAction()
    {
        //lógica slide a la izquierda
        AudioManager.instance.PlaySFX(AudioManager.instance.selectedButtom);
        slides[slideIndex].SetActive(false);
        slideIndex--;
        slides[slideIndex].SetActive(true);
    }
}
