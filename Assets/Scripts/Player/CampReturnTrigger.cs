using System.Collections;
using UnityEngine;
using TMPro;
public class CampReturnTrigger : MonoBehaviour
{
    [Header("Objects")]
    public GameObject normalCar;
    public GameObject wreckedCar;
    public GameObject monster;
    [Header("Monster Audio")]
    public AudioSource audioSource;
    public AudioClip monsterScream;
    [Header("Background Music")]
    public AudioSource backgroundMusicSource;
    [Header("Heartbeat")]
    public AudioSource heartbeatSource;
    [Header("Dialogue")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public float dialogueTime = 2.5f;
    [Header("Settings")]
    public float monsterRevealDelay = 2f;
    private bool triggered = false;
    void OnTriggerEnter(Collider other)
    {
        if (triggered)
            return;
        if (!other.CompareTag("Player"))
            return;
        triggered = true;
        StartCoroutine(ReturnSequence());
    }
    IEnumerator ReturnSequence()
    {
        yield return new WaitForSeconds(monsterRevealDelay);
        if (normalCar != null)
            normalCar.SetActive(false);
        if (wreckedCar != null)
            wreckedCar.SetActive(true);
        if (monster != null)
            monster.SetActive(true);
        if (audioSource != null && monsterScream != null)
            audioSource.PlayOneShot(monsterScream);
        if (backgroundMusicSource != null && backgroundMusicSource.isPlaying)
            backgroundMusicSource.Stop();
        if (heartbeatSource != null)
        {
            heartbeatSource.loop = true;
            heartbeatSource.Play();
        }
        if (dialoguePanel != null)
            dialoguePanel.SetActive(true);
        if (dialogueText != null)
        {
            dialogueText.text = "What... what the hell is that?!";
            yield return new WaitForSeconds(dialogueTime);
        }
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
        if (ObjectiveManager.Instance != null)
            ObjectiveManager.Instance.SetObjective("RUN! GET BACK TO THE BARN!");
        if (EscapeCarTrigger.Instance != null)
            EscapeCarTrigger.Instance.UnlockEscapeCar();
        yield return new WaitForSeconds(3f);
        if (ObjectiveManager.Instance != null)
            ObjectiveManager.Instance.SetObjective("START THE CAR AND GET OUT!");
    }
}