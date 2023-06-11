using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviourPunCallbacks
{
    [System.Serializable]
    public class PlayerPrefabPair
    {
        public string nickname; // プレイヤーのニックネーム
        public GameObject prefab; // プレイヤーのニックネームに対応するPrefab
    }

    public Transform[] spawnPositons;
    private List<Transform> availableSpawnPositions;
    public GameObject playerPrefab;


    // PlayerPrefabPairのリストを作成し、Unityのインスペクタから設定できるようにします。
    public List<PlayerPrefabPair> playerPrefabs;

    public GameObject masterPlayerPrefab;
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
        // マスタープレイヤーであればmasterPlayerPrefabを、それ以外はplayerPrefabを生成する
        if (PhotonNetwork.IsMasterClient)
        {
            player = PhotonNetwork.Instantiate(masterPlayerPrefab.name, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            // プレイヤー名に応じたPrefabをスポーンさせる
            string playerName = PhotonNetwork.LocalPlayer.NickName;
            GameObject prefab = playerPrefabs.Find(p => p.nickname == playerName)?.prefab;
            if (prefab != null)
            {
                player = PhotonNetwork.Instantiate(prefab.name, spawnPoint.position, spawnPoint.rotation);
            }
            else
            {
                player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation);
            }
        }
    }

    public void Die()
    {
        if (player != null)
        {
            //respawnInterval秒後にリスポーンさせる
            Invoke("SpawnPlayer", respawnInterval);
            //playerをネットワーク上から削除
            PhotonNetwork.Destroy(player);
        }
    }
}
