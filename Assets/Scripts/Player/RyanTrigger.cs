using TMPro;
using UnityEngine;
public class RyanDialogue : MonoBehaviour
{
    [Header("UI")]
    public GameObject talkPrompt;
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    [Header("Player")]
    public MonoBehaviour playerMovement;
    public MonoBehaviour mouseLook;
    private string[] dialogueLines =
    {
        "You: Man... I'm exhausted.",
        "Ryan: Yeah... It's been one hell of a drive.",
        "Ryan: We've got a long hike tomorrow.",
        "Ryan: Get some sleep. We'll head out first thing in the morning."
    };
    private bool playerNear = false;
    private bool dialogueStarted = false;
    private bool canTalk = false;   // Locked until camp is built
    private int currentLine = 0;
    void Start()
    {
        talkPrompt.SetActive(false);
        dialoguePanel.SetActive(false);
    }
    public void EnableTalking()
    {
        canTalk = true;
        if (playerNear)
            talkPrompt.SetActive(true);
    }
    void Update()
    {
        if (playerNear && canTalk && !dialogueStarted && Input.GetKeyDown(KeyCode.E))
        {
            StartDialogue();
        }
        if (dialogueStarted && Input.GetMouseButtonDown(0))
        {
            NextLine();
        }
    }
    void StartDialogue()
    {
        dialogueStarted = true;
        talkPrompt.SetActive(false);
        dialoguePanel.SetActive(true);
        if (playerMovement != null)
            playerMovement.enabled = false;
        if (mouseLook != null)
            mouseLook.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        currentLine = 0;
        dialogueText.text = dialogueLines[currentLine];
    }
    void NextLine()
    {
        currentLine++;
        if (currentLine >= dialogueLines.Length)
        {
            EndDialogue();
            return;
        }
        dialogueText.text = dialogueLines[currentLine];
    }
    void EndDialogue()
    {
        dialogueStarted = false;
        dialoguePanel.SetActive(false);
        if (playerMovement != null)
            playerMovement.enabled = true;
        if (mouseLook != null)
            mouseLook.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (ObjectiveManager.Instance != null)
            ObjectiveManager.Instance.SetObjective("Sleep in the Tent");
        TentTrigger tent = FindFirstObjectByType<TentTrigger>();
        if (tent != null)
            tent.EnableSleeping();
    }
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        playerNear = true;
        if (canTalk && !dialogueStarted)
            talkPrompt.SetActive(true);
    }
    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        playerNear = false;
        talkPrompt.SetActive(false);
    }
}