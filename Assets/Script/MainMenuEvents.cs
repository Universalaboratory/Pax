using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuEvents : MonoBehaviour
{
    [Header("Menu Buttons")]
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject exitButton;
    [SerializeField] private GameObject newGameButton;
    [SerializeField] private GameObject backButton;

    private void Start()
    {
        newGameButton.SetActive(false);
        backButton.SetActive(false);
    }

    public void OnClickPlay()
    {
        playButton.SetActive(false);
        exitButton.SetActive(false);
        newGameButton.SetActive(true);
        backButton.SetActive(true);
    }

    public void OnClickExit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }

    public void OnClickNewGame()
    {
        StartCoroutine("LoadGameHall");
    }

    IEnumerator LoadGameHall()
    {
        yield return new WaitForSecondsRealtime(1);
        SceneManager.LoadScene(1);
    }

    public void OnClickSettings()
    {
        // Load Options Scene
    }

    public void OnClickBack()
    {
        playButton.SetActive(true);
        exitButton.SetActive(true);
        newGameButton.SetActive(false);
        backButton.SetActive(false);
    }
}
