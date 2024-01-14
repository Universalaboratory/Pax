using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuEvents : MonoBehaviour
{
    [Header("Menu Buttons")]
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject exitButton;
    [SerializeField] private GameObject newGameButton;
    [SerializeField] private GameObject settingsButton;
    [SerializeField] private GameObject backButton;
    [SerializeField] private LevelLoader levelLoader;
    private void Start()
    {
        levelLoader = FindObjectOfType<LevelLoader>();
        if (newGameButton)
        newGameButton.SetActive(false);
        if(settingsButton)
        settingsButton.SetActive(false);
        if(backButton)
        backButton.SetActive(false);
    }

    public void OnClickPlay()
    {
        playButton.SetActive(false);
        exitButton.SetActive(false);
        newGameButton.SetActive(true);
        //settingsButton.SetActive(true);
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
        if (levelLoader)
        {
            levelLoader.LoadLevel(2);
        }
        else
        {
            StartCoroutine("LoadGameHall");
        }
        
    }

    IEnumerator LoadGameHall()
    {
        yield return new WaitForSecondsRealtime(1);
        LoadNewScene(2);
    }

    public void LoadNewScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
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
        settingsButton.SetActive(false);
        backButton.SetActive(false);
    }
}
