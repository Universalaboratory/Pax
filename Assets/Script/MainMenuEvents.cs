using UnityEngine;

public class MainMenuEvents : MonoBehaviour
{
    [Header("Menu Buttons")]
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject exitButton;
    [SerializeField] private GameObject newGameButton;
    [SerializeField] private GameObject optionsButton;
    [SerializeField] private GameObject backButton;

    private void Start()
    {
        newGameButton.SetActive(false);
        optionsButton.SetActive(false);
        backButton.SetActive(false);
    }

    public void OnClickPlay()
    {
        playButton.SetActive(false);
        exitButton.SetActive(false);
        newGameButton.SetActive(true);
        optionsButton.SetActive(true);
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
        // Load multiplayer hall Scene
    }

    public void OnClickOptions()
    {
        // Load Options Scene
    }

    public void OnClickBack()
    {
        playButton.SetActive(true);
        exitButton.SetActive(true);
        newGameButton.SetActive(false);
        optionsButton.SetActive(false);
        backButton.SetActive(false);
    }
}