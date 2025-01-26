using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HTPController : MonoBehaviour
{
    public GameObject[] slides;
    int slideIndex;
    int currentScene;
    [SerializeField]GameObject countdownCanvas;

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
        if(Input.GetKeyDown(KeyCode.RightArrow) && slideIndex < 2 && currentScene == 0)
        {
            slides[slideIndex].SetActive(false);
            slideIndex++;
            slides[slideIndex].SetActive(true);
            
            
        }else if(Input.GetKeyDown(KeyCode.RightArrow) && slideIndex < 2 && currentScene != 0){
            slides[slideIndex].SetActive(false);
            slideIndex++;
            slides[slideIndex].SetActive(true);
            

        } else if(Input.GetKeyDown(KeyCode.RightArrow) && slideIndex == 2 && currentScene == 0){
            Debug.Log("Hola");
            StartCountdown();

        }

        if(Input.GetKeyDown(KeyCode.LeftArrow) && slideIndex > 0)
        {
            //lógica slide a la izquierda
            slides[slideIndex].SetActive(false);
            slideIndex--;
            slides[slideIndex].SetActive(true);
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

    }
}
