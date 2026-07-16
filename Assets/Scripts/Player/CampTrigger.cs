using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class CampTrigger : MonoBehaviour
{
    [Header("Camp")]
    public GameObject campSetup;
    [Header("UI")]
    public GameObject promptUI;
    public Image fadeImage;
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip setupSound;
    [Header("Fade")]
    public float fadeDuration = 3f;
    [Header("NPCs")]
    public Transform ryan;
    public Transform jake;
    public Transform alex;
    [Header("Camp Positions")]
    public Transform ryanCampPosition;
    public Transform jakeCampPosition;
    public Transform alexCampPosition;
    private bool playerInside = false;
    private bool campBuilt = false;
    void Start()
    {
        campSetup.SetActive(false);
        promptUI.SetActive(false);
        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            c.a = 0f;
            fadeImage.color = c;
        }
    }
    void Update()
    {
        if (playerInside && !campBuilt && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(BuildCamp());
        }
    }
    IEnumerator BuildCamp()
    {
        campBuilt = true;
        promptUI.SetActive(false);
        yield return StartCoroutine(Fade(0f, 1f));
        if (audioSource != null && setupSound != null)
        {
            audioSource.clip = setupSound;
            audioSource.loop = false;
            audioSource.Play();
            yield return new WaitForSeconds(setupSound.length);
            audioSource.Stop();
        }
        if (campSetup != null)
            campSetup.SetActive(true);
        if (ryan != null && ryanCampPosition != null)
        {
            ryan.position = ryanCampPosition.position;
            ryan.rotation = ryanCampPosition.rotation;
        }
        if (jake != null && jakeCampPosition != null)
        {
            jake.position = jakeCampPosition.position;
            jake.rotation = jakeCampPosition.rotation;
        }
        if (alex != null && alexCampPosition != null)
        {
            alex.position = alexCampPosition.position;
            alex.rotation = alexCampPosition.rotation;
        }
        if (TimeOfDayManager.Instance != null)
        {
            TimeOfDayManager.Instance.SetNight();
        }
        RyanDialogue dialogue = FindFirstObjectByType<RyanDialogue>();
        if (dialogue != null)
        {
            dialogue.EnableTalking();
        }
        yield return StartCoroutine(Fade(1f, 0f));
        if (ObjectiveManager.Instance != null)
        {
            ObjectiveManager.Instance.SetObjective("Talk to Ryan");
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
        if (other.CompareTag("Player") && !campBuilt)
        {
            playerInside = true;
            promptUI.SetActive(true);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            promptUI.SetActive(false);
        }
    }
}