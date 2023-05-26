using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class DisplayNames : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI playerNamesText;

    void Update()
    {
        UpdatePlayerNames();
    }

    void UpdatePlayerNames()
    {
        playerNamesText.text = "";
        List<Player> playerList = new List<Player>();
        playerList.AddRange(PhotonNetwork.PlayerList);

        foreach (Player player in playerList)
        {
            playerNamesText.text += player.NickName + "\n";
        }
    }
}

