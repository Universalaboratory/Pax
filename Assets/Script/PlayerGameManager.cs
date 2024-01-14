using Photon;
using UnityEngine;
using UnityEngine.SceneManagement;

//Responsável por instanciar os jogadores
public class PlayerGameManager : PunBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject playerInstantiated = null;
    [SerializeField] private LevelLoader levelLoader;
    private void Start()
    {
        levelLoader = FindObjectOfType<LevelLoader>();
        if (!PhotonNetwork.connected)
        {
            levelLoader.LoadLevel("Main");
            //SceneManager.LoadScene("Main");
            return;
        }

        if (_player != null)
        {
            playerInstantiated = PhotonNetwork.Instantiate(this._player.name, this._player.transform.position, Quaternion.identity, 0) as GameObject;
        }
    }
    //assim que um dos players forem desconectados, o outro também sai
    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        Debug.Log("teste");
        base.OnLeftRoom();
        levelLoader.LoadLevel("Main");
        //SceneManager.LoadScene("Main");
    }
    public void PlayerDisconnect()
    {
        PhotonNetwork.LeaveRoom();
    }
}
