using Photon;
using System.Collections;
using TMPro;
using UnityEngine;

public class VersusSet : PunBehaviour
{
    [SerializeField] private TMP_Text textVersus;

    private void Start()
    {
        StartCoroutine("SetVersusTextWhenTwoPlayers");
    }

    private IEnumerator SetVersusTextWhenTwoPlayers()
    {
        while (true)
        {
            if (PhotonNetwork.room.PlayerCount == 2)
            {
                string player1Name = PhotonNetwork.playerList[0].NickName;
                string player2Name = PhotonNetwork.playerList[1].NickName;
                textVersus.text = player1Name + " VS " + player2Name;
                yield break; // Saia da corrotina quando a condição for atendida.
            }
            yield return null; // Espere um frame antes de verificar novamente.
        }
    }
}
