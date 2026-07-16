using System.Collections;
using TMPro;
using UnityEngine;
public class RyanBodyTrigger : MonoBehaviour
{
    [Header("Dialogue")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    [Header("Player")]
    public MonoBehaviour playerMovement;
    public MonoBehaviour mouseLook;
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip panicSound;
    [Header("Camp Return")]
    public GameObject campReturnTrigger;
    [Header("Settings")]
    public float dialogueTime = 2.5f;
    private bool triggered = false;
    void Start()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
        if (campReturnTrigger != null)
            campReturnTrigger.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (triggered)
            return;
        if (!other.CompareTag("Player"))
            return;
        triggered = true;
        StartCoroutine(DialogueSequence());
    }
    IEnumerator DialogueSequence()
    {
        if (playerMovement != null)
            playerMovement.enabled = false;
        if (mouseLook != null)
            mouseLook.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (audioSource != null && panicSound != null)
            audioSource.PlayOneShot(panicSound);
        dialoguePanel.SetActive(true);
        dialogueText.text = "Shit...";
        yield return new WaitForSeconds(dialogueTime);
        dialogueText.text = "What... who did this?";
        yield return new WaitForSeconds(dialogueTime);
        dialogueText.text = "I need to get out of here.";
        yield return new WaitForSeconds(dialogueTime);
        dialoguePanel.SetActive(false);
        if (playerMovement != null)
            playerMovement.enabled = true;
        if (mouseLook != null)
            mouseLook.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (ObjectiveManager.Instance != null)
            ObjectiveManager.Instance.SetObjective("Return to the Campsite");
        yield return new WaitForSeconds(1f);
        if (campReturnTrigger != null)
            campReturnTrigger.SetActive(true);
        gameObject.SetActive(false);
    }
}