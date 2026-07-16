using UnityEngine;
public class EscapeCarTrigger : MonoBehaviour
{
    public static EscapeCarTrigger Instance;
    [Header("UI")]
    public GameObject carPrompt;
    private bool playerInside = false;
    private bool unlocked = false;
    private bool endingStarted = false;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        if (carPrompt != null)
            carPrompt.SetActive(false);
    }
    void Update()
    {
        if (!unlocked || endingStarted)
            return;
        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            endingStarted = true;
            if (carPrompt != null)
                carPrompt.SetActive(false);
            if (EndingManager.Instance != null)
            {
                EndingManager.Instance.StartEnding();
            }
        }
    }
    public void UnlockEscapeCar()
    {
        unlocked = true;
        Debug.Log("Escape Car Unlocked!");
    }
    void OnTriggerEnter(Collider other)
    {
        if (!unlocked)
            return;
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            if (carPrompt != null)
                carPrompt.SetActive(true);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            if (carPrompt != null)
                carPrompt.SetActive(false);
        }
    }
}