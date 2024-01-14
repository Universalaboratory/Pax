using UnityEngine;
using Photon;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Game Loader deriva de PunBehaviour, é parecido com o MonoBehaviour, só que possui callbacks do photon (métodos com override)
[RequireComponent(typeof(PhotonView))]
public class GameLoader : PunBehaviour
{
    [SerializeField] private TMPro.TMP_InputField _nicknameField;
    [SerializeField] private TMPro.TMP_Text _playersInRoom;
    [SerializeField] private Button _buttonStartGame;
    [SerializeField] private Button _buttonJoinLobby;
    [SerializeField] private GameObject _lobyWindow;
    [SerializeField] private GameObject _mainWindow;
    [SerializeField] private GameObject _imageLoop;
    [SerializeField] private LevelLoader levelLoader;
    private bool isConnecting;

    private void Start()
    {
        levelLoader = FindAnyObjectByType<LevelLoader>();
    }
    private void Awake()
    {
        PhotonNetwork.autoJoinLobby = false;
        PhotonNetwork.automaticallySyncScene = true;
        //botão startGame só estará disponível para o usuário dono da sala (Master)
        _buttonStartGame.interactable = false;
        _buttonJoinLobby.interactable = true;
        _imageLoop.SetActive(false);
        _mainWindow.SetActive(true);
        _lobyWindow.SetActive(false);
    }

    public void Connect()
    {        
        isConnecting = true;

        _imageLoop.SetActive(true);
        _buttonJoinLobby.interactable = false;
        //caso já tenha connectado com o servidor, inicia o processo de entrar em uma sala, senão, conecta com o servidor
        if (PhotonNetwork.connected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectToRegion(CloudRegionCode.sa, "1", null);
        }
    }
    
    public override void OnConnectedToMaster()
    {
        if (isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 2 }, null);
    }

    public override void OnDisconnectedFromPhoton()
    {
        isConnecting = false;
    }

    //chamado quando o player local é desconectado
    public override void OnLeftRoom()
    {
        _buttonJoinLobby.interactable = true;
        _mainWindow.SetActive(true);
        _lobyWindow.SetActive(false);
        _imageLoop.SetActive(false);
        levelLoader.LoadLevel("Main");
        //SceneManager.LoadScene("Main");
    }

    //chamado quando algum player é desconectado
    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        _buttonJoinLobby.interactable = true;
        _buttonStartGame.interactable = false;
        _imageLoop.SetActive(false);
        base.OnPhotonPlayerDisconnected(otherPlayer);
        if (PhotonNetwork.connected)
        {
            this.UpdatePlayerList();
        }
    }

    public void LeftRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnJoinedRoom()
    {
        _mainWindow.SetActive(false);
        _lobyWindow.SetActive(true);

        this.photonView.RPC("LoadPlayersView", PhotonTargets.AllBuffered);
        _imageLoop.SetActive(false);
        _buttonJoinLobby.interactable = true;
    }

    public void LoadGame(string gameName)
    {
        _buttonJoinLobby.interactable = false;
        _imageLoop.SetActive(true);
        this.photonView.RPC("LoadScene", PhotonTargets.AllBuffered, gameName);
    }

    //chamado através de OnJoinedRoom(), AllBuffered permite que todos executem o código assim que todos estiverem com os recursos carregados
    [PunRPC]
    public void LoadPlayersView()
    {
        this.UpdatePlayerList();
    }

    public void UpdatePlayerList()
    {
        string players = "";
        foreach (PhotonPlayer player in PhotonNetwork.playerList)
        {
            players += (player.NickName + "\n");
        }
        _playersInRoom.text = players;

        if (PhotonNetwork.room.PlayerCount == 2)
        {

            if (PhotonNetwork.isMasterClient)
            {
                _buttonStartGame.interactable = true;
            }
            else
            {
                _buttonStartGame.interactable = false;
            }
        }
    }

    [PunRPC]
    public void LoadScene(string gameName)
    {
        PhotonNetwork.LoadLevel(gameName);
    }
}
