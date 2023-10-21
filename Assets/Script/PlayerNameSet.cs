using UnityEngine;

//Responsável  por carregar ou atualizar o nickname do player
public class PlayerNameSet : MonoBehaviour
{
    private string _name;
    private void Start()
    {
        if (PlayerPrefs.HasKey("playerNickName"))
        {
            _name = PlayerPrefs.GetString("playerNickName");
            PhotonNetwork.playerName = _name;
            GetComponent<TMPro.TMP_InputField>().SetTextWithoutNotify(_name);
        }
    }
    public void SetPlayerName(string value)
    {
        _name = (value != null || value != "") ? value : "Guest";
        PhotonNetwork.playerName = _name;

        PlayerPrefs.SetString("playerNickName", _name);
    }
}
