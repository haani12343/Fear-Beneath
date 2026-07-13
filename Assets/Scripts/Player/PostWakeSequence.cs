using System.Collections;
using TMPro;
using UnityEngine;

public class PostWakeSequence : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource musicSource;
    public AudioClip backgroundMusic;

    public AudioSource screamSource;
    public AudioClip distantScream;

    [Header("Dialogue")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    [Header("Settings")]
    public float musicDelay = 2f;
    public float screamDelay = 10f;
    public float dialogueTime = 2.5f;

    private bool started = false;

    void Start()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }

    public void StartSequence()
    {
        if (started)
            return;

        started = true;
        StartCoroutine(Sequence());
    }

    IEnumerator Sequence()
    {
        // Small delay after waking up
        yield return new WaitForSeconds(musicDelay);

        // Give the player the flashlight
        if (FlashlightManager.Instance != null)
        {
            FlashlightManager.Instance.GiveFlashlight();
        }

        // Start background music
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }

        // Wait before Ryan screams
        yield return new WaitForSeconds(screamDelay);

        // Play distant scream
        if (screamSource != null && distantScream != null)
        {
            screamSource.clip = distantScream;
            screamSource.Play();
        }

        // Show dialogue
        if (dialoguePanel != null)
            dialoguePanel.SetActive(true);

        if (dialogueText != null)
        {
            dialogueText.text = "Was that Ryan screaming?";
            yield return new WaitForSeconds(dialogueTime);

            dialogueText.text = "I need to find him...";
            yield return new WaitForSeconds(dialogueTime);

            dialogueText.text = "And get the hell out of this place as soon as possible.";
            yield return new WaitForSeconds(dialogueTime);
        }

        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);

        if (ObjectiveManager.Instance != null)
        {
            ObjectiveManager.Instance.SetObjective("Find Ryan");
        }
    }
}