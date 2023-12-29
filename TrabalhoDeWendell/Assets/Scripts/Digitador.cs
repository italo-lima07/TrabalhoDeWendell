using System.Collections;
using TMPro;
using UnityEngine;

public class Digitador : MonoBehaviour
{
    public TextMeshProUGUI[] textElements;
    public float typingSpeed = 0.05f;
    public float commaPauseDuration = 0.2f;
    public float periodPauseDuration = 0.6f;

    private int currentTextIndex = 0;
    private TextMeshProUGUI currentTextElement;

    private void Start()
    {
        foreach (TextMeshProUGUI textElement in textElements)
        {
            textElement.gameObject.SetActive(false);
        }

        currentTextElement = textElements[currentTextIndex];
        currentTextElement.gameObject.SetActive(true);

        StartCoroutine(StartTyping());
    }

    private IEnumerator StartTyping()
    {
        string originalText = currentTextElement.text;
        currentTextElement.text = string.Empty;

        foreach (char letter in originalText)
        {
            currentTextElement.text += letter;

            if (letter == ',')
            {
                yield return new WaitForSeconds(commaPauseDuration);
            }
            else if (letter == '.')
            {
                yield return new WaitForSeconds(periodPauseDuration);
            }
            else
            {
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        yield return new WaitForSeconds(1f); // Tempo de espera após exibir completamente o texto

        currentTextElement.gameObject.SetActive(false);

        currentTextIndex++;
        if (currentTextIndex < textElements.Length)
        {
            currentTextElement = textElements[currentTextIndex];
            currentTextElement.gameObject.SetActive(true);
            StartCoroutine(StartTyping());
        }
    }
}