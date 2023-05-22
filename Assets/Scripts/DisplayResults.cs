using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using ExitGames.Client.Photon;
using System.Collections.Generic;

public class DisplayResults : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI resultsText;

    void Start()
    {
        // マスタークライアントであれば、リザルトをすべてのクライアントに同期
        if (PhotonNetwork.IsMasterClient)
        {
            ExitGames.Client.Photon.Hashtable props = new ExitGames.Client.Photon.Hashtable
            {
                { "FinishOrder", GlobalGameData.finishOrder.ToArray() }
            };
            PhotonNetwork.CurrentRoom.SetCustomProperties(props);
        }
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        // ルームプロパティが更新されたらリザルトを更新
        if (propertiesThatChanged.ContainsKey("FinishOrder"))
        {
            string[] finishOrder = (string[])propertiesThatChanged["FinishOrder"];
            GlobalGameData.finishOrder = new List<string>(finishOrder);
            DisplayResultsLocal();
        }
    }

    void DisplayResultsLocal()
    {
        if (GlobalGameData.finishOrder == null) return;

        resultsText.text = "";

        for (int i = 0; i < GlobalGameData.finishOrder.Count; i++)
        {
            resultsText.text += (i + 1) + ". " + GlobalGameData.finishOrder[i] + "\n";
        }
    }
}
