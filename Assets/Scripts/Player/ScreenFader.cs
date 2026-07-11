using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class ScreenFader : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 2f;
    IEnumerator Start()
    {
        Color color = fadeImage.color;
        color.a = 1f;
        fadeImage.color = color;
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            fadeImage.color = color;

            yield return null;
        }
        color.a = 0f;
        fadeImage.color = color;
    }
}