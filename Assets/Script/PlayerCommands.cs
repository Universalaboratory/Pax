
using UnityEngine;
using Photon;
using System;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine.XR;
using System.Collections;
using DG.Tweening;

//Responsavel por Executar comandos simples
public class PlayerCommands : PunBehaviour
{
    [SerializeField] private CardData[] _cardsData;
    private Card[] _cards;
    private CardHand _hand;
    private Transform _logParent;
    [SerializeField] private GameObject _messageLog;
    [SerializeField] private GameObjectVariable _houseSelected;
    [SerializeField] private CardLibrary _cardLibrary;

    private void Start()
    {
        _logParent = GameObject.FindGameObjectWithTag("LogParent").transform;
        _cards = GameObject.FindObjectsOfType<Card>();

        _hand = FindObjectOfType<CardHand>();

        if (PhotonNetwork.isMasterClient)
        {
            int setDeck = UnityEngine.Random.Range(0, 2);
            PhotonView.Get(this).RPC("SetupDeck", PhotonTargets.AllBuffered, setDeck);
        }

        if (PhotonView.Get(this).isMine)
        {
            PaxTurnManager.Instance.SetupCommand(this);
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

    public void ToggleTurnRpc()
    {
        PhotonView.Get(this).RPC("ToggleTurn", PhotonTargets.All);
    }
    public void HandleCardUsed(CardData cardData, GameObject target)
    {
        if (PhotonView.Get(this).isMine)
        {
            if (PaxTurnManager.Instance.isMyTurn())
            {

                this.ToggleTurnRpc();
                CardDeck deck = _cardLibrary.Decks.Find(d => d.Deck.Contains(cardData));
                int deckIndex = _cardLibrary.Decks.IndexOf(deck);
                int indexCard = deck.Deck.FindIndex(c => c == cardData);

                PhotonView.Get(this).RPC("RenderCard", PhotonTargets.All, indexCard, deckIndex, _houseSelected.CurrentValue.tag);
            }
            //PhotonView.Get(this).RPC("ConsoleLog", PhotonTargets.All, PhotonNetwork.player.NickName + " Selecionou a carta: " + cardData.name + " com o dano de: " + cardData.damage + " e usou na posi��o: " + _houseSelected.CurrentValue.name + " do tabuleiro.");
        }
    }

    [PunRPC]
    public void RenderCard(int indexCard, int deckIndex, string houseTag)
    {
        CardDeck deck = _cardLibrary.Decks[deckIndex];
        CardData cardData = deck.Deck[indexCard];
        HouseManager houseSelected = GameObject.FindGameObjectWithTag(houseTag).GetComponent<HouseManager>();
        houseSelected.handleNewAction(cardData);

    }

    [PunRPC]
    public void SetupDeck(int deckIndex)
    {
        if (PhotonNetwork.isMasterClient)
        {
            _hand.ReceiveDeck(_cardLibrary.Decks[deckIndex]);
        }
        else
        {
            _hand.ReceiveDeck(_cardLibrary.Decks[deckIndex == 0 ? 1 : 0]);
        }
        GameObject cardConteiner = GameObject.FindGameObjectWithTag("CardConteiner");
        if (cardConteiner)
        {
            cardConteiner.transform.DOMoveY(202, 1f, false).SetEase(Ease.OutBack).OnComplete(() =>
            {
                foreach (Card card in _cards)
                {
                    card.OnCardselected += HandleCard;
                    card.OnCardUsed += HandleCardUsed;
                    card.gameObject.GetComponent<CardMover>().SetInitialPosition(card.gameObject.transform.position);
                }
            });
        }
    }

    [PunRPC]
    private void ToggleTurn()
    {
        if (!PaxTurnManager.Instance.isMyTurn())
        {
            _hand.DrawCard();

        }
        StartCoroutine(TestEnd());
        PaxTurnManager.Instance.ToggleTurn();
    }

    private IEnumerator TestEnd()
    {
        yield return new WaitForEndOfFrame();
        if (!_hand.HasCard())
        {
            PhotonView.Get(this).RPC("ShowWinner", PhotonTargets.All, _hand.CardDeck.Deck[0].cardConfig.cardType == CardType.GRECIA ? CardType.ROMA.ToString() : CardType.GRECIA.ToString());
        }
    }

    [PunRPC]
    private void ShowWinner(string winner)
    {
        WinManager.Instance.ShowWinner(winner);
    }


    [PunRPC]
    private void ConsoleLog(string msg)
    {
        // GameObject go = Instantiate(_messageLog);
        // go.GetComponent<TMPro.TMP_Text>().text = msg;
        // go.transform.SetParent(_logParent);
    }
}
