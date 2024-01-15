using System.Collections.Generic;
using Sunflyer.Audio;
using UnityEngine;
using UnityEngine.Events;

public class GameInfo
{
    public bool hasChampion;
    public string message;
    public GameInfo(bool hasChampion, string message)
    {
        this.message = message;
        this.hasChampion = hasChampion;
    }
}

public class WinManager : MonoBehaviour
{
    public static WinManager Instance { get; private set; }
    public List<HouseManager> topLine;
    public List<HouseManager> centerLine;
    public List<HouseManager> bottomLine;
    public AudioInstanceReference Sound;
    [SerializeField] private AudioPlayer Win, Lose;
    public CardHand Hand => _hand;
    private CardHand _hand;
    public System.Action<bool> OnWin;
    public UnityAction OnFinish;
    public bool _hasChampion = false;

    public GameObject resultGo;
    public TMPro.TMP_Text textoResultado;
    private MusicPlayer _musicPlayer;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        _hand = FindObjectOfType<CardHand>();
        _musicPlayer = FindObjectOfType<MusicPlayer>();
        Sound.SetupInstance();
    }

    public void ShowWinner(string winner)
    {
        if (_hasChampion)
        {
            return;
        }
        OnFinish?.Invoke();
        OnWin?.Invoke(Won(winner));
        Sound.SetParameterByName("Stinger", Won(winner) ? 0 : 1);
        Sound.PlayAudio();

        if (Won(winner))
        {
            Lose.PlayAudio();
        }
        else
        {
            Win.PlayAudio();
        }

        _musicPlayer.StopAudio();

        string text = (Won(winner) ? "You win!!" : " You lost!! ") + "\n\n" + winner + " Win!!";

        textoResultado.text = text;
        resultGo.SetActive(true);
        _hasChampion = true;
    }

    private bool Won(string winner)
    {
        return _hand.Cards[0].cardData.cardConfig.cardType.ToString() == winner;
    }

    private void FixedUpdate()
    {
        if (!_hasChampion)
        {
            hasChampion();
        }
    }

    public bool hasChampion()
    {
        CardType cardType = CardType.SPARTA;

        //if (CheckRowsForWinner(cardType) || CheckColumnsForWinner(cardType) || CheckDiagonalsForWinner(cardType))
        if (CheckRowsForWinner(cardType))
        {
            ShowWinner(CardType.SPARTA.ToString());
            return true;
        }

        cardType = CardType.ATHENS;

        //if (CheckRowsForWinner(cardType) || CheckColumnsForWinner(cardType) || CheckDiagonalsForWinner(cardType))
        if (CheckRowsForWinner(cardType))
        {
            ShowWinner(CardType.SPARTA.ToString());
            return true;
        }

        return false;
    }

    public bool HASCHAMPION()
    {
        CardType cardType = CardType.SPARTA;
       
        if (CheckRowsForWinner(cardType))
        {
            return true;
        }

        cardType = CardType.ATHENS;

        if (CheckRowsForWinner(cardType))
        {
            return true;
        }

        return false;
    }

    private bool CheckRowsForWinner(CardType cardType)
    {
        if (topLine[0].GetCardData()?.cardConfig.cardType == cardType &&
                topLine[1].GetCardData()?.cardConfig.cardType == cardType &&
                topLine[2].GetCardData()?.cardConfig.cardType == cardType &&
                topLine[3].GetCardData()?.cardConfig.cardType == cardType &&
                topLine[4].GetCardData()?.cardConfig.cardType == cardType)
        {
            return true;
        }

        if (bottomLine[0].GetCardData()?.cardConfig.cardType == cardType &&
                bottomLine[1].GetCardData()?.cardConfig.cardType == cardType &&
                bottomLine[2].GetCardData()?.cardConfig.cardType == cardType &&
                bottomLine[3].GetCardData()?.cardConfig.cardType == cardType &&
                bottomLine[4].GetCardData()?.cardConfig.cardType == cardType)
        {
            return true;
        }

        if (centerLine[0].GetCardData()?.cardConfig.cardType == cardType &&
                centerLine[1].GetCardData()?.cardConfig.cardType == cardType &&
                centerLine[2].GetCardData()?.cardConfig.cardType == cardType &&
                centerLine[3].GetCardData()?.cardConfig.cardType == cardType &&
                centerLine[4].GetCardData()?.cardConfig.cardType == cardType)
        {
            return true;
        }

        return false;
    }

    //private bool CheckColumnsForWinner(CardType cardType)
    //{
    //    if (topLine[0].GetCardData()?.cardConfig.cardType == cardType &&
    //            centerLine[0].GetCardData()?.cardConfig.cardType == cardType &&
    //            bottomLine[0].GetCardData()?.cardConfig.cardType == cardType)
    //    {
    //        return true;
    //    }

    //    if (topLine[1].GetCardData()?.cardConfig.cardType == cardType &&
    //            centerLine[1].GetCardData()?.cardConfig.cardType == cardType &&
    //            bottomLine[1].GetCardData()?.cardConfig.cardType == cardType)
    //    {
    //        return true;
    //    }

    //    if (topLine[2].GetCardData()?.cardConfig.cardType == cardType &&
    //            centerLine[2].GetCardData()?.cardConfig.cardType == cardType &&
    //            bottomLine[2].GetCardData()?.cardConfig.cardType == cardType)
    //    {
    //        return true;
    //    }
    //    return false;
    //}

    //private bool CheckDiagonalsForWinner(CardType cardType)
    //{
    //    if (
    //        (topLine[0].GetCardData()?.cardConfig.cardType == cardType &&
    //         centerLine[1].GetCardData()?.cardConfig.cardType == cardType &&
    //         bottomLine[2].GetCardData()?.cardConfig.cardType == cardType)

    //         ||
    //        (topLine[2].GetCardData()?.cardConfig.cardType == cardType &&
    //         centerLine[1].GetCardData()?.cardConfig.cardType == cardType &&
    //         bottomLine[0].GetCardData()?.cardConfig.cardType == cardType))
    //    {
    //        return true;
    //    }
    //    return false;
    //}
}