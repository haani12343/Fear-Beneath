using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EndingManager : MonoBehaviour
{
    public static EndingManager Instance;
    [Header("UI")]
    public Image fadeImage;
    public GameObject endingPanel;
    public TextMeshProUGUI endingText;
    [Header("Hide During Ending")]
    public GameObject flashlightUI;
    [Header("Heartbeat")]
    public AudioSource heartbeatSource;
    [Header("Player")]
    public MonoBehaviour playerMovement;
    public MonoBehaviour mouseLook;
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip carDoor;
    public AudioClip engineStart;
    public AudioClip engineRev;
    public AudioClip tireScreech;
    public AudioClip monsterScream;
    [Header("Settings")]
    public float fadeDuration = 2f;
    public float textDuration = 4f;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        if (endingPanel != null)
            endingPanel.SetActive(false);
        Color c = fadeImage.color;
        c.a = 0f;
        fadeImage.color = c;
    }
    public void StartEnding()
    {
        StartCoroutine(EndingSequence());
    }
    IEnumerator EndingSequence()
    {
        if (flashlightUI != null)
            flashlightUI.SetActive(false);
        if (heartbeatSource != null)
            heartbeatSource.Stop();
        if (playerMovement != null)
            playerMovement.enabled = false;
        if (mouseLook != null)
            mouseLook.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        yield return StartCoroutine(Fade(0f, 1f));
        if (carDoor != null)
        {
            audioSource.PlayOneShot(carDoor);
            yield return new WaitForSeconds(carDoor.length);
        }
        if (engineStart != null)
        {
            audioSource.PlayOneShot(engineStart);
            yield return new WaitForSeconds(engineStart.length);
        }
        if (engineRev != null)
        {
            audioSource.PlayOneShot(engineRev);
            yield return new WaitForSeconds(1f);
        }
        if (monsterScream != null)
            audioSource.PlayOneShot(monsterScream);
        if (tireScreech != null)
        {
            audioSource.PlayOneShot(tireScreech);
            yield return new WaitForSeconds(2f);
        }
        if (endingPanel != null)
            endingPanel.SetActive(true);
        endingText.text = "I never told anyone what truly happened that night.";
        yield return new WaitForSeconds(textDuration);
        endingText.text = "The police never believed my story.";
        yield return new WaitForSeconds(textDuration);
        endingText.text = "They called it an accident... but I know what I saw.";
        yield return new WaitForSeconds(textDuration);
        endingText.text = "Sometimes... I still hear that scream echoing through the trees.";
        yield return new WaitForSeconds(textDuration);
        endingText.text = "Whatever was out there...\n\nis still waiting.";
        yield return new WaitForSeconds(textDuration);
        endingText.text =
            "<size=70>FEAR BENEATH</size>\n\nEpisode 1\n\n<size=60>THE END</size>";
    }
    IEnumerator Fade(float start, float end)
    {
        float timer = 0f;
        Color c = fadeImage.color;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            c.a = Mathf.Lerp(start, end, timer / fadeDuration);
            fadeImage.color = c;
            yield return null;
        }
        c.a = end;
        fadeImage.color = c;
    }
}