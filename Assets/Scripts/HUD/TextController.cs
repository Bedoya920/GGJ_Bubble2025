using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI; // Necesario para trabajar con TextMeshProUGUI

public class TextController : MonoBehaviour
{
    [Header("Text Fade")]
    [SerializeField] private TextMeshProUGUI myText;
    [SerializeField] private GameObject gameObjectText;
    [SerializeField] private float fadeDuration = 1f;

    [SerializeField] private TextMeshProUGUI scoreValue;
    [SerializeField] public TextMeshProUGUI remainingValue;
    [SerializeField] private TextMeshProUGUI accuracyValue;

    [SerializeField] private Text accuracyText;
    [SerializeField] private Text typedText;
    [SerializeField] private Text failedText;

    public ScoreManager scoreManager;

    private void Start()
    {
        if (gameObjectText) {
            gameObjectText.SetActive(false);
        }
    }

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

    public void UpdateCanvasScore() 
    {
        scoreValue.text = scoreManager.score.ToString();
        remainingValue.text = scoreManager.remaining.ToString();
        accuracyValue.text = scoreManager.accuracy.ToString() + "%";
    }

    public void UpdateStatsCanvas()
    {
        int totalTyped = PlayerPrefs.GetInt("typedKeysLevel1") + PlayerPrefs.GetInt("typedKeysLevel2") + PlayerPrefs.GetInt("typedKeysLevel3");
        int totalFailed = PlayerPrefs.GetInt("failedKeysLevel1") + PlayerPrefs.GetInt("failedKeysLevel2") + PlayerPrefs.GetInt("failedKeysLevel3");
        Debug.Log($"{totalTyped}");
        if (totalTyped > 0) {
            StartCoroutine(AnimateTextChange(accuracyText, 100 - (totalFailed * 100 / totalTyped), "%"));
            StartCoroutine(AnimateTextChange(typedText, totalTyped, ""));
            StartCoroutine(AnimateTextChange(failedText, totalFailed, ""));
        }
    }

    private IEnumerator AnimateTextChange(Text textElement, int targetValue, string sufix)
    {
        int currentValue = 0;
        if (int.TryParse(textElement.text.Replace("%", ""), out int parsedValue))
        {
            currentValue = parsedValue;
        }

        float duration = 2f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            int newValue = Mathf.RoundToInt(Mathf.Lerp(currentValue, targetValue, elapsedTime / duration));
            textElement.text = targetValue == currentValue ? targetValue.ToString() : $"{newValue}" + (textElement == accuracyText ? $" {sufix}" : "");
            yield return null;
        }

        textElement.text = targetValue + (textElement == accuracyText ? " %" : "");
    }
}

[System.Serializable]

public class TxtLines
{
    public string texts;
    public string actorName;
}