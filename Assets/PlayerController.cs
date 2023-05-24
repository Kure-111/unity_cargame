using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviourPunCallbacks
{
    SpawnManager spawnManager;
    private void Awake()
    {
        spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();
    }
    void Update()
    {
        if (photonView.IsMine)
        {

            if (Input.GetKeyDown(KeyCode.R))
            {
                spawnManager.Die();
            }
        }
    }
}
