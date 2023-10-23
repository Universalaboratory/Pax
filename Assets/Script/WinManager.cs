using System.Collections.Generic;
using UnityEngine;


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

    public bool _hasChampion = false;

    public GameObject resultGo;
    public TMPro.TMP_Text textoResultado;

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
    }
    public void ShowWinner(string winner)
    {
        string text = winner + " Venceu!!";
        textoResultado.text = text;
        resultGo.SetActive(true);
        _hasChampion = true;
    }

    private void FixedUpdate()
    {
        if (!_hasChampion)
        {
            hasChampion();
        }
    }

    public void hasChampion()
    {
        CardType cardType = CardType.ROMA;

        if (CheckRowsForWinner(cardType) || CheckColumnsForWinner(cardType) || CheckDiagonalsForWinner(cardType))
        {
            ShowWinner(CardType.ROMA.ToString());
        }

        cardType = CardType.GRECIA;

        if (CheckRowsForWinner(cardType) || CheckColumnsForWinner(cardType) || CheckDiagonalsForWinner(cardType))
        {
            ShowWinner(CardType.GRECIA.ToString());
        }
    }

    private bool CheckRowsForWinner(CardType cardType)
    {
        if (topLine[0].GetCardData()?.cardConfig.cardType == cardType &&
                topLine[1].GetCardData()?.cardConfig.cardType == cardType &&
                topLine[2].GetCardData()?.cardConfig.cardType == cardType)
        {
            return true;
        }

        if (bottomLine[0].GetCardData()?.cardConfig.cardType == cardType &&
                bottomLine[1].GetCardData()?.cardConfig.cardType == cardType &&
                bottomLine[2].GetCardData()?.cardConfig.cardType == cardType)
        {
            return true;
        }

        if (centerLine[0].GetCardData()?.cardConfig.cardType == cardType &&
                centerLine[1].GetCardData()?.cardConfig.cardType == cardType &&
                centerLine[2].GetCardData()?.cardConfig.cardType == cardType)
        {
            return true;
        }

        return false;
    }

    private bool CheckColumnsForWinner(CardType cardType)
    {
        if (topLine[0].GetCardData()?.cardConfig.cardType == cardType &&
                centerLine[0].GetCardData()?.cardConfig.cardType == cardType &&
                bottomLine[0].GetCardData()?.cardConfig.cardType == cardType)
        {
            return true;
        }

        if (topLine[1].GetCardData()?.cardConfig.cardType == cardType &&
                centerLine[1].GetCardData()?.cardConfig.cardType == cardType &&
                bottomLine[1].GetCardData()?.cardConfig.cardType == cardType)
        {
            return true;
        }

        if (topLine[2].GetCardData()?.cardConfig.cardType == cardType &&
                centerLine[2].GetCardData()?.cardConfig.cardType == cardType &&
                bottomLine[2].GetCardData()?.cardConfig.cardType == cardType)
        {
            return true;
        }
        return false;
    }

    private bool CheckDiagonalsForWinner(CardType cardType)
    {
        if (
            (topLine[0].GetCardData()?.cardConfig.cardType == cardType &&
             centerLine[1].GetCardData()?.cardConfig.cardType == cardType &&
             bottomLine[2].GetCardData()?.cardConfig.cardType == cardType)

             ||
            (topLine[2].GetCardData()?.cardConfig.cardType == cardType &&
             centerLine[1].GetCardData()?.cardConfig.cardType == cardType &&
             bottomLine[0].GetCardData()?.cardConfig.cardType == cardType))
        {
            return true;
        }
        return false;
    }
}
