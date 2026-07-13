using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TentTrigger : MonoBehaviour
{
    [Header("UI")]
    public GameObject sleepPrompt;
    public GameObject timeText;
    public Image fadeImage;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip screamSound;

    [Header("Alive NPCs")]
    public GameObject ryan;
    public GameObject alex;
    public GameObject jake;

    [Header("Dead NPCs")]
    public GameObject deadRyan;
    public GameObject deadAlex;
    public GameObject deadJake;

    [Header("Post Wake Sequence")]
    public PostWakeSequence postWakeSequence;

    [Header("Settings")]
    public float fadeDuration = 2f;
    public float blackScreenDelay = 2f;
    public float timeTextDuration = 3f;
    public float beforeScreamDelay = 1f;

    private bool playerInside = false;
    private bool sleeping = false;

    void Start()
    {
        sleepPrompt.SetActive(false);
        timeText.SetActive(false);

        if (deadRyan != null)
            deadRyan.SetActive(false);

        if (deadAlex != null)
            deadAlex.SetActive(false);

        if (deadJake != null)
            deadJake.SetActive(false);

        Color c = fadeImage.color;
        c.a = 0f;
        fadeImage.color = c;
    }

    void Update()
    {
        if (playerInside && !sleeping && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(SleepSequence());
        }
    }

    IEnumerator SleepSequence()
    {
        sleeping = true;
        sleepPrompt.SetActive(false);

        // Fade to black
        yield return StartCoroutine(Fade(0f, 1f));

        // Black screen delay
        yield return new WaitForSeconds(blackScreenDelay);

        // Show 3:00 AM
        timeText.SetActive(true);
        yield return new WaitForSeconds(timeTextDuration);
        timeText.SetActive(false);

        yield return new WaitForSeconds(beforeScreamDelay);

        // Play nightmare scream
        if (audioSource != null && screamSound != null)
        {
            audioSource.clip = screamSound;
            audioSource.Play();

            while (audioSource.isPlaying)
            {
                yield return null;
            }
        }

        // Hide alive NPCs
        if (ryan != null)
            ryan.SetActive(false);

        if (alex != null)
            alex.SetActive(false);

        if (jake != null)
            jake.SetActive(false);

        // Show dead NPCs
        if (deadAlex != null)
            deadAlex.SetActive(true);

        if (deadJake != null)
            deadJake.SetActive(true);

        // Ryan is still missing
        if (deadRyan != null)
            deadRyan.SetActive(false);

        // Fade back in
        yield return StartCoroutine(Fade(1f, 0f));

        // Start the post wake sequence
        if (postWakeSequence != null)
        {
            postWakeSequence.StartSequence();
        }
    }

    IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float timer = 0f;
        Color color = fadeImage.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;

            color.a = Mathf.Lerp(startAlpha, endAlpha, timer / fadeDuration);
            fadeImage.color = color;

            yield return null;
        }

        color.a = endAlpha;
        fadeImage.color = color;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !sleeping)
        {
            playerInside = true;
            sleepPrompt.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            sleepPrompt.SetActive(false);
        }
    }
}