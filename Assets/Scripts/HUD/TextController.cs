using UnityEngine;
using TMPro;
using System.Collections; // Necesario para trabajar con TextMeshProUGUI

public class TextController : MonoBehaviour
{
    [Header("Text Fade")]
    [SerializeField] private TextMeshProUGUI myText;
    [SerializeField] private GameObject gameObjectText;
    [SerializeField] private float fadeDuration = 1f;

    private void Start()
    {
        gameObjectText.SetActive(false);
    }

    // Método para hacer visible el texto gradualmente
    public void ShowTextWithFade(string newText)
    {
        if (myText != null)
        {
            gameObjectText.SetActive(true);
            myText.text = newText;
            StartCoroutine(FadeInText());
        }
    }

    private IEnumerator FadeInText()
    {
        Color textColor = myText.color;
        textColor.a = 0f;
        myText.color = textColor;

        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            textColor.a = Mathf.Lerp(0f, 1f, timeElapsed / fadeDuration);
            myText.color = textColor;
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        textColor.a = 1f;
        myText.color = textColor;

        timeElapsed = 0f;
        while (timeElapsed < fadeDuration)
        {
            textColor.a = Mathf.Lerp(1f, 0f, timeElapsed / fadeDuration);
            myText.color = textColor;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        gameObjectText.SetActive(false);
    }
}

[System.Serializable]

public class TxtLines
{
    public string texts;
    public string actorName;
}