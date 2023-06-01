using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviourPunCallbacks
{
    private int playersFinished = 0;
    public static List<string> finishOrder = new List<string>();

    private void Start()
    {

        finishOrder = new List<string>();
    }

    public void PlayerFinished(string playerName, GameObject playerObject)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            playersFinished++;
            finishOrder.Add(playerName);
            CheckAllPlayersFinished();

            // Destroy the player object
            if (playerObject.GetComponent<PhotonView>().IsMine)
            {
                PhotonNetwork.Destroy(playerObject);
            }
        }
        else
        {
            photonView.RPC("NotifyPlayerFinished", RpcTarget.MasterClient, playerName);
        }
    }

    [PunRPC]
    private void NotifyPlayerFinished(string playerName)
    {
        playersFinished++;
        finishOrder.Add(playerName);
        CheckAllPlayersFinished();
        Debug.Log(playersFinished);
    }

    private void CheckAllPlayersFinished()
    {
        if (playersFinished + 1 >= PhotonNetwork.PlayerList.Length)
        {
            DisplayFinishOrder();
            LoadNextScene();
        }
    }

    private void DisplayFinishOrder()
    {
        GlobalGameData.finishOrder = finishOrder;
    }

    private void LoadNextScene()
    {
        // Replace with your scene transitioning code
        SceneManager.LoadScene("SampleScene");
    }
}
