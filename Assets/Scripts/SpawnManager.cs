using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviourPunCallbacks
{
    public Transform[] spawnPositons;
    private List<Transform> availableSpawnPositions;

    public GameObject playerPrefab;
    private GameObject player;
    public float respawnInterval = 5f;

    private void Start()
    {
        // スポーンポイントをコピーして利用可能なスポーンポイントリストを作成
        availableSpawnPositions = new List<Transform>(spawnPositons);

        if (PhotonNetwork.IsConnected)
        {
            SpawnPlayer();
        }
    }

    public Transform GetSpawnPoint()
    {
        // リストからランダムに選んで位置情報を返す
        int spawnIndex = PhotonNetwork.LocalPlayer.ActorNumber % availableSpawnPositions.Count;
        Transform chosenSpawnPoint = availableSpawnPositions[spawnIndex];

        // 選んだスポーンポイントを利用可能リストから削除
        availableSpawnPositions.RemoveAt(spawnIndex);

        return chosenSpawnPoint;
    }

    public void SpawnPlayer()
    {
        Transform spawnPoint = GetSpawnPoint();
        player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation);
    }
    public void Die()
    {

        if (player != null)
        {
            //5秒後にリスポーンさせる
            Invoke("SpawnPlayer", 1f);
        }
        //playerをネットワーク上から削除
        PhotonNetwork.Destroy(player);

    }
}
