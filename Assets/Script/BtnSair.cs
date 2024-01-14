using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnSair : MonoBehaviour
{
    public GameObject painelSair;
    void Start()
    {
        painelSair.SetActive(false);
    }

    
    void Update()
    {
        
    }

    public void ativarPainel()
    {
        painelSair.SetActive(true);
    }

    public void desativarPainel()
    {
        painelSair.SetActive(false);
    }

    public void sairDeSena()
    {
        SceneManager.LoadScene("Main");
    }
}
