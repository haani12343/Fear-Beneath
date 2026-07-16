using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class MonsterKillTrigger : MonoBehaviour
{
    [Header("UI")]
    public Image fadeImage;
    public GameObject deathPanel;
    public TextMeshProUGUI deathText;
    [Header("Hide Gameplay UI")]
    public GameObject objectiveUI;
    public GameObject dialoguePanel;
    public GameObject talkPrompt;
    public GameObject sleepPrompt;
    public GameObject flashlightUI;
    public GameObject carPrompt;
    [Header("Player")]
    public MonoBehaviour playerMovement;
    public MonoBehaviour mouseLook;
    [Header("Audio")]
    public AudioSource heartbeatSource;
    public AudioSource monsterAudioSource;
    public AudioClip monsterScream;
    [Header("Settings")]
    public float fadeDuration = 2f;
    public float deathScreenTime = 3f;
    private bool dead = false;
    void Start()
    {
        if (deathPanel != null)
            deathPanel.SetActive(false);
        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            c.a = 0f;
            fadeImage.color = c;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (dead)
            return;
        if (!other.CompareTag("Player"))
            return;
        dead = true;
        StartCoroutine(DeathSequence());
    }
    IEnumerator DeathSequence()
    {
        if (objectiveUI != null)
            objectiveUI.SetActive(false);
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
        if (talkPrompt != null)
            talkPrompt.SetActive(false);
        if (sleepPrompt != null)
            sleepPrompt.SetActive(false);
        if (flashlightUI != null)
            flashlightUI.SetActive(false);
        if (carPrompt != null)
            carPrompt.SetActive(false);
        if (playerMovement != null)
            playerMovement.enabled = false;
        if (mouseLook != null)
            mouseLook.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (heartbeatSource != null)
            heartbeatSource.Stop();
        if (monsterAudioSource != null && monsterScream != null)
            monsterAudioSource.PlayOneShot(monsterScream);
        float timer = 0f;
        Color c = fadeImage.color;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            c.a = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            fadeImage.color = c;
            yield return null;
        }
        if (deathPanel != null)
            deathPanel.SetActive(true);
        if (deathText != null)
            deathText.text = "<color=red><size=90>YOU DIED</size></color>";
        yield return new WaitForSeconds(deathScreenTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}