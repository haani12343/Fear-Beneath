using System.Collections;
using UnityEngine;
public class TimeOfDayManager : MonoBehaviour
{
    public static TimeOfDayManager Instance;
    [Header("Directional Light")]
    public Light sun;
    [Header("Skyboxes")]
    public Material eveningSkybox;
    public Material nightSkybox;
    [Header("Sun Settings")]
    public float eveningIntensity = 1f;
    public float nightIntensity = 0.08f;
    public Color warmSunColor = new Color(1f, 0.75f, 0.5f);
    public Color coldMoonColor = new Color(0.6f, 0.75f, 1f);
    [Header("Sun Rotation")]
    public Vector3 eveningRotation = new Vector3(20f, -30f, 0f);
    public Vector3 nightRotation = new Vector3(5f, -30f, 0f);
    [Header("Audio")]
    public AudioSource forestAmbience;
    public AudioSource cricketAmbience;
    [Header("Fog")]
    public Color eveningFogColor = new Color(0.6f, 0.5f, 0.45f);
    public Color nightFogColor = new Color(0.12f, 0.14f, 0.18f);
    public float eveningFogDensity = 0.002f;
    public float nightFogDensity = 0.008f;
    [Header("Transition")]
    public float transitionTime = 5f;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        RenderSettings.skybox = eveningSkybox;
        sun.intensity = eveningIntensity;
        sun.color = warmSunColor;
        sun.transform.rotation = Quaternion.Euler(eveningRotation);
        RenderSettings.fogColor = eveningFogColor;
        RenderSettings.fogDensity = eveningFogDensity;
        forestAmbience.volume = 1f;
        cricketAmbience.volume = 0f;
        if (!forestAmbience.isPlaying)
            forestAmbience.Play();
        if (!cricketAmbience.isPlaying)
            cricketAmbience.Play();
    }
    public void SetNight()
    {
        StopAllCoroutines();
        StartCoroutine(NightTransition());
    }
    IEnumerator NightTransition()
    {
        RenderSettings.skybox = nightSkybox;
        float startIntensity = sun.intensity;
        Color startColor = sun.color;
        Quaternion startRotation = sun.transform.rotation;
        float timer = 0f;
        while (timer < transitionTime)
        {
            timer += Time.deltaTime;
            float t = timer / transitionTime;
            sun.intensity = Mathf.Lerp(startIntensity, nightIntensity, t);
            sun.color = Color.Lerp(startColor, coldMoonColor, t);
            sun.transform.rotation = Quaternion.Slerp(
                startRotation,
                Quaternion.Euler(nightRotation),
                t);
            RenderSettings.fogColor = Color.Lerp(
                eveningFogColor,
                nightFogColor,
                t);
            RenderSettings.fogDensity = Mathf.Lerp(
                eveningFogDensity,
                nightFogDensity,
                t);
            forestAmbience.volume = Mathf.Lerp(1f, 0f, t);
            cricketAmbience.volume = Mathf.Lerp(0f, 1f, t);
            DynamicGI.UpdateEnvironment();
            yield return null;
        }
        sun.intensity = nightIntensity;
        sun.color = coldMoonColor;
        sun.transform.rotation = Quaternion.Euler(nightRotation);
        forestAmbience.volume = 0f;
        cricketAmbience.volume = 1f;
        RenderSettings.skybox = nightSkybox;
        DynamicGI.UpdateEnvironment();
    }
}