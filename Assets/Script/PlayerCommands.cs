
using UnityEngine;
using Photon;
using System;
using ExitGames.Client.Photon.StructWrapping;

//Responsavel por Executar comandos simples
public class PlayerCommands : PunBehaviour
{
    [SerializeField]private CardData[] _cardsData;
    private Card[] _cards;

    private Transform _logParent;
    [SerializeField] private GameObject _messageLog;
    [SerializeField] private GameObjectVariable _houseSelected;
    [SerializeField] private CardLibrary _cardLibrary;

    private void Start()
    {
        _logParent = GameObject.FindGameObjectWithTag("LogParent").transform;
        _cards = GameObject.FindObjectsOfType<Card>();
        foreach (Card card in _cards)
        {
            card.OnCardselected += HandleCard;
            card.OnCardUsed += HandleCardUsed;
        }
    }

    private void OnDisable()
    {
        foreach (Card card in _cards)
        {
            card.OnCardselected -= HandleCard;
            card.OnCardUsed -= HandleCardUsed;
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
            PhotonView.Get(this).RPC("ConsoleLog", PhotonTargets.All, PhotonNetwork.player.NickName + " Selecionou a carta: " + cardData.cardName + " com o dano de: " + cardData.damage);
        }
    }
    public void HandleCardUsed(CardData cardData, GameObject target)
    {
        if (PhotonView.Get(this).isMine)
        {
            if (PaxTurnManager.Instance.isMyTurn())
            {

                PhotonView.Get(this).RPC("ToggleTurn", PhotonTargets.All);
                int indexCard = _cardLibrary.cards.FindIndex(c => c == cardData);
                PhotonView.Get(this).RPC("RenderCard", PhotonTargets.All, indexCard, _houseSelected.CurrentValue.tag);
            }
            PhotonView.Get(this).RPC("ConsoleLog", PhotonTargets.All, PhotonNetwork.player.NickName + " Selecionou a carta: " + cardData.name + " com o dano de: " + cardData.damage + " e usou na posi��o: " + _houseSelected.CurrentValue.name + " do tabuleiro.");
        }
    }

    [PunRPC]
    public void RenderCard(int indexCard, string houseTag)
    {
        CardData cardData = _cardLibrary.cards[indexCard];
        HouseManager houseSelected = GameObject.FindGameObjectWithTag(houseTag).GetComponent<HouseManager>();
        houseSelected.handleNewAction(cardData);

    }

    [PunRPC]
    private void ToggleTurn()
    {
        PaxTurnManager.Instance.ToggleTurn();
    }
    [PunRPC]
    private void ConsoleLog(string msg)
    {
        GameObject go = Instantiate(_messageLog);
        go.GetComponent<TMPro.TMP_Text>().text = msg;
        go.transform.SetParent(_logParent);
    }
}
