using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    [Header("Main Menu Buttons")]
    public GameObject playButton;
    public GameObject settingsButton;
    public GameObject quitButton;
    [Header("Settings")]
    public GameObject settingsPanel;
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void OpenSettings()
    {
        playButton.SetActive(false);
        settingsButton.SetActive(false);
        quitButton.SetActive(false);
        settingsPanel.SetActive(true);
    }
    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        playButton.SetActive(true);
        settingsButton.SetActive(true);
        quitButton.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}