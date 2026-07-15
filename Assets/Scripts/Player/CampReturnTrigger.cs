using System.Collections;
using UnityEngine;

public class CampReturnTrigger : MonoBehaviour
{
    [Header("Objects")]
    public GameObject normalCar;
    public GameObject wreckedCar;
    public GameObject monster;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip monsterScream;

    [Header("Settings")]
    public float monsterRevealDelay = 2f;
    public float secondObjectiveDelay = 2f;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
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
    // Give the player a second to notice the wrecked campsite
    yield return new WaitForSeconds(monsterRevealDelay);

    // Swap cars
    if (normalCar != null)
        normalCar.SetActive(false);

    if (wreckedCar != null)
        wreckedCar.SetActive(true);

    // Reveal monster
    if (monster != null)
        monster.SetActive(true);

    // Monster scream
    if (audioSource != null && monsterScream != null)
        audioSource.PlayOneShot(monsterScream);

    // First objective
    if (ObjectiveManager.Instance != null)
        ObjectiveManager.Instance.SetObjective("GET TO THE BARN!");

    // Wait a little before the chase starts
    yield return new WaitForSeconds(secondObjectiveDelay);

    // Start the chase
    if (MonsterChase.Instance != null)
    {
        MonsterChase.Instance.StartChase();
    }

    // Change objective
    if (ObjectiveManager.Instance != null)
        ObjectiveManager.Instance.SetObjective("GET IN THE CAR!");
}
}