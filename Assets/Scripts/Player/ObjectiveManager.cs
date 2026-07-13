using UnityEngine;
using TMPro;
using System.Collections;
public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance;
    [Header("UI")]
    public GameObject objectivePanel;
    public TextMeshProUGUI objectiveText;
    [Header("Settings")]
    public float displayTime = 4f;
    private Coroutine currentRoutine;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        objectivePanel.SetActive(false);
    }
    public void SetObjective(string newObjective)
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);
        currentRoutine = StartCoroutine(ShowObjective(newObjective));
    }
    IEnumerator ShowObjective(string text)
    {
        objectivePanel.SetActive(true);
        objectiveText.text = "OBJECTIVE\n\n" + text;
        yield return new WaitForSeconds(displayTime);
        objectivePanel.SetActive(false);
    }
}