using UnityEngine;
using Photon;
using TMPro;
using System.Collections.Generic;
using Unity.VisualScripting;

public class PaxTurnManager : PunBehaviour
{
    public static PaxTurnManager Instance { get; private set; }
    [SerializeField] private bool _isMasterTurn = true;
    [SerializeField] private bool _myTurn = false;
    [SerializeField] private float timeTurn = 20f;
    [SerializeField] private TextMeshProUGUI TurnDisplay;
    private CardHand _hand;

    [SerializeField] private List<GameObject> _cardFieldList;
    [SerializeField] private int _cardFieldListSize;
    [SerializeField] private int _turnCounter;

    private Color translucent = new Color(1f, 1f, 1f, 0.6f);
    private Color transparent = new Color(1f, 1f, 1f, 0f);

    [SerializeField] private float _turnTimer = 31f;
    [SerializeField] private TextMeshProUGUI _UITimerText;

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

        _isMasterTurn = true;
        
        if (PhotonNetwork.isMasterClient)
        {
            _myTurn = true;
        }

        UpdateUI();
    }

    private void Start()
    {
        _cardFieldList = new List<GameObject>();

        for (int i = 1; i < 6; i++)
        {
            for (int j = 1; j < 4; j++)
            {
                _cardFieldList.Add(GameObject.FindGameObjectWithTag($"{i}{j}"));
            }
        }

        _turnCounter = 1;
        _cardFieldListSize = _cardFieldList.Capacity - 1;
        TurnManager();
    }

    private void Update()
    {
        _turnTimer -= Time.deltaTime;
        
        if (_turnTimer < 10)
            _UITimerText.text = $"Timer: \n 00:0{(int)_turnTimer}";
        else
            _UITimerText.text = $"Timer: \n 00:{(int)_turnTimer}";

        if (_turnTimer <= 0)
            ToggleTurn();
    }

    public bool isMyTurn()
    {
        return _myTurn;
    }
    
    private void UpdateUI()
    {
        if (_myTurn)
        {
            TurnDisplay.text = $"{PhotonNetwork.player.NickName} Turn";
        }
        else
        {
            foreach (var player in PhotonNetwork.playerList)
            {
                if (player != PhotonNetwork.player)
                {
                    TurnDisplay.text = $"{player.NickName} Turn";
                }
            }
        }
    }

    public void ToggleTurn()
    {
        _turnTimer = 31f;
        _turnCounter++;
        _isMasterTurn = !_isMasterTurn;
        _myTurn = false;

        if (PhotonNetwork.isMasterClient && _isMasterTurn)
        {
            _myTurn = true;
            TurnDisplay.text = $"{PhotonNetwork.player.NickName} Turn";
        }
        if (!PhotonNetwork.isMasterClient && !_isMasterTurn)
        {
            _myTurn = true;
        }

        UpdateUI();

        TurnManager();

        WinManager.Instance.hasChampion();
    }

    private void TurnManager()
    {
        if (_turnCounter > 5)
            return;
        else if (_turnCounter == 5)
            TurnFieldManager(0, _cardFieldListSize, '+');
        else if (_turnCounter == 4)
            TurnFieldManager(_cardFieldListSize - 1, _cardFieldListSize - 7, '-');
        else if (_turnCounter == 3)
            TurnFieldManager(0, 6, '+');
        else if (_turnCounter == 2)
            TurnFieldManager(_cardFieldListSize - 1, _cardFieldListSize - 4, '-');
        else if (_turnCounter == 1)
            TurnFieldManager(0, 3, '+');
    }

    private void TurnFieldManager(int initRange, int endRange, char operation)
    {
        if (operation == '+')
        {
            for (int i = initRange; i < _cardFieldListSize; i++)
            {
                if (i < endRange)
                {
                    _cardFieldList[i].GetComponent<HouseSelector>().CanPlay = true;
                    _cardFieldList[i].GetComponent<Renderer>().material.SetColor("_Color", transparent);
                }
                else
                {
                    _cardFieldList[i].GetComponent<HouseSelector>().CanPlay = false;
                    _cardFieldList[i].GetComponent<Renderer>().material.SetColor("_Color", translucent);
                }
            }
        }
        else if (operation == '-')
        {
            for (int i = initRange; i >= 0; i--)
            {
                if (i > endRange)
                {
                    _cardFieldList[i].GetComponent<HouseSelector>().CanPlay = true;
                    _cardFieldList[i].GetComponent<Renderer>().material.SetColor("_Color", transparent);
                }
                else
                {
                    _cardFieldList[i].GetComponent<HouseSelector>().CanPlay = false;
                    _cardFieldList[i].GetComponent<Renderer>().material.SetColor("_Color", translucent);
                }
            }
        }
        else
            Debug.LogError("Erro na Tentativa da operação. Só podem ser utilizados os operadores " +
                "(+) ou (-).");
    }
}