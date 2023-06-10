using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WallSpawn : MonoBehaviourPunCallbacks
{
    public GameObject wallPrefab; // 壁のPrefabを指定
    private bool wallSpawned = false; // 壁が出現したかどうかをチェックするフラグ

    void Update()
    {
        if (!photonView.IsMine) return;
        Vector3 spawnPosition = transform.position;

        if (Input.GetKeyDown(KeyCode.Return) && !wallSpawned)
        {
            spawnPosition.x += 2.5f;
            spawnPosition.z += 2;
            spawnPosition.y -= 1.05f;

            Quaternion rotation = Quaternion.Euler(0, 90, 0);

            // ネットワーク経由で壁を生成する
            PhotonNetwork.Instantiate(wallPrefab.name, spawnPosition, rotation);

            wallSpawned = true;
        }
    }
}
