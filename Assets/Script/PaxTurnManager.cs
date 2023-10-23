using UnityEngine;
using Photon;
using TMPro;

public class PaxTurnManager : PunBehaviour
{
    public static PaxTurnManager Instance { get; private set; }
    [SerializeField] private bool _isMasterTurn = true;
    [SerializeField] private bool _myTurn = false;
    [SerializeField] private float timeTurn = 20f;
    [SerializeField] private TextMeshProUGUI TurnDisplay;
    private CardHand _hand;
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
        WinManager.Instance.hasChampion();
    }
}
