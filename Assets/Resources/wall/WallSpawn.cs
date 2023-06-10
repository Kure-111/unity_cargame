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
            // プレイヤーの後ろに壁を出現させる


            spawnPosition.x += 2.5f;
            spawnPosition.z += 2;
            spawnPosition.y -= 1.05f;

            // y軸に沿って90度回転させる
            Quaternion rotation = Quaternion.Euler(0, 90, 0);

            Instantiate(wallPrefab, spawnPosition, rotation);

            // 壁が出現したのでフラグを更新
            wallSpawned = true;
        }
    }
}
