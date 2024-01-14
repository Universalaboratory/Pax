using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Desistir : MonoBehaviour
{
    public GameObject painelDesistir;

    void Start()
    {
        painelDesistir.SetActive(false);
    }

    
    void Update()
    {
        
    }


    public void ativarPainel()
    {
        painelDesistir.SetActive(true);
    }
    public void desativarPainel()
    {
        painelDesistir.SetActive(false);
    }
    public void voltarMenu()
    {
        SceneManager.LoadScene("Main");
    }
}
