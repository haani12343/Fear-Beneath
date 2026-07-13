using System.Collections;
using UnityEngine;
using TMPro;

public class FlashlightManager : MonoBehaviour
{
    public static FlashlightManager Instance;

    [Header("UI")]
    public GameObject flashlightIcon;
    public GameObject pickupText;

    [Header("Flashlight")]
    public Light flashlight;

    private bool hasFlashlight = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        flashlightIcon.SetActive(false);
        pickupText.SetActive(false);

        flashlight.enabled = false;
    }

    void Update()
    {
        if (!hasFlashlight)
            return;

        if (Input.GetKeyDown(KeyCode.T))
        {
            flashlight.enabled = !flashlight.enabled;
        }
    }

    public void GiveFlashlight()
    {
        StartCoroutine(GiveFlashlightRoutine());
    }

    IEnumerator GiveFlashlightRoutine()
    {
        hasFlashlight = true;

        flashlightIcon.SetActive(true);

        pickupText.SetActive(true);

        flashlight.enabled = true;

        yield return new WaitForSeconds(2f);

        pickupText.SetActive(false);
    }
}