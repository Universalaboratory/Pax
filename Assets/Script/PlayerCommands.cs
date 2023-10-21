
using UnityEngine;
using Photon;
using System.Collections.Generic;
//Responsavel por Executar comandos simples
public class PlayerCommands : PunBehaviour
{
    Card[] cards;
    private void Start()
    {
        cards = GameObject.FindObjectsOfType<Card>();
        foreach (Card card in cards)
        {
            card.OnCardselected += HandleCard;
        }
    }

    private void OnDisable()
    {
        foreach (Card card in cards)
        {
            card.OnCardselected -= HandleCard;
        }
    }
    private void Update()
    {
        //se aperto espa�o e esse script � do meu personagem, ent�o envio a mensagem para todos os outros jogadores(inclusive pra mim)
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (PhotonView.Get(this).isMine)
            {
                PhotonView.Get(this).RPC("ConsoleLog", PhotonTargets.All, "Ol� pessoal, sou o master?: " + PhotonNetwork.isMasterClient);
            }
        }
    }
    //aqui fa�o a RPC da carta selecionada
    public void HandleCard(CardData cardData)
    {
        if (PhotonView.Get(this).isMine)
        {
            PhotonView.Get(this).RPC("ConsoleLog", PhotonTargets.All, PhotonNetwork.player.NickName + " Selecionou a carta: " + cardData.nome + " com o dano de: " + cardData.dano);
        }
    }

    [PunRPC]
    private void ConsoleLog(string msg)
    {
        Debug.Log(msg);
    }
}
