using UnityEngine;
using Photon;

public class PaxTurnManager : PunBehaviour
{
    public static PaxTurnManager Instance { get; private set; }
    [SerializeField] private bool _isMasterTurn = true;
    [SerializeField] private bool _myTurn = false;
    [SerializeField] private float timeTurn = 20f;
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
    }
    public bool isMyTurn()
    {
        return _myTurn;
    }

    [PunRPC]
    public void ToggleTurn()
    {
        _isMasterTurn = !_isMasterTurn;
        _myTurn = false;
        if(PhotonNetwork.isMasterClient && _isMasterTurn)
        {
            _myTurn = true;
        }
        if(!PhotonNetwork.isMasterClient && !_isMasterTurn)
        {
            _myTurn = true;
        }
        
    }
}
