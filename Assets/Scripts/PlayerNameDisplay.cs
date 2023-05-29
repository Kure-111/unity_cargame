using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class PlayerNameDisplay : MonoBehaviourPunCallbacks
{
    public TextMeshPro nameText;

    private void Start()
    {
        if (photonView.IsMine)
        {
            nameText.text = PhotonNetwork.NickName;
        }
        else
        {
            nameText.text = photonView.Owner.NickName;
        }
    }
}