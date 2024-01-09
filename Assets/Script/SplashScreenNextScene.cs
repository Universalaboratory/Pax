using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenNextScene : MonoBehaviour
{
    private float _timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NextScene());
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > 3f  && Input.anyKeyDown)
        {
            GoToMenu();
        }
    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(10.06f);

        GoToMenu();
    }

    private void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
