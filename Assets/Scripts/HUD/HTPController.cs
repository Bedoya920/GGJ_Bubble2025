using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HTPController : MonoBehaviour
{
    public GameObject[] slides;
    int slideIndex;

    void Start()
    {
        foreach(GameObject slide in slides)
        {
            slide.SetActive(false);
        }
        slideIndex = 0;
        slides[slideIndex].SetActive(true);

    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow) && slideIndex < 2)
        {
            slides[slideIndex].SetActive(false);
            slideIndex++;
            slides[slideIndex].SetActive(true);
            //lógica slide a la derecha
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
}
