using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    private void Start()
    {
        transition = GetComponentInChildren<Animator>();
    }
    public void LoadLevel(int idx)
    {
        StartCoroutine(LoadLevelCorroutine(idx));
    }
    public void LoadLevel(string name)
    {
        StartCoroutine(LoadLevelCorroutine(name));
    }

    IEnumerator LoadLevelCorroutine(int idx)
    {
        transition.SetTrigger("start");

        yield return new WaitForSeconds(transitionTime);
        
        SceneManager.LoadScene(idx);
    }
    IEnumerator LoadLevelCorroutine(string name)
    {
        transition.SetTrigger("start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(name);
    }
}
