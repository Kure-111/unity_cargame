using UnityEngine;
using Photon.Pun;
using UnityStandardAssets.Vehicles.Car;

public class WorldStop : MonoBehaviour
{
    public AudioSource stopSoundSource; // ストップ音のオーディオソース
    public GameObject visibleObject; // 停止した車の近くに表示したいオブジェクト
    private CarController carController;
    private PhotonView photonView;
    private bool isCarStopped = false; // 車が停止しているかどうかのフラグ
    private GameObject spawnedObject; // 生成したオブジェクトへの参照
    private float originalTopSpeed; // 車の元の最高速度

    void Start()
    {
        carController = GetComponent<CarController>();
        photonView = GetComponent<PhotonView>();
        originalTopSpeed = carController.m_Topspeed; // 車の元の最高速度を保存します
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && PhotonNetwork.IsMasterClient)
        {
            ToggleCarStop(); // すべての車の停止/再開を切り替える
        }
    }

    [PunRPC]
    public void ToggleCarStop()
    {
        isCarStopped = !isCarStopped; // フラグを反転させる

        // ここで車の速度を0に設定または元に戻します
        if (carController != null && !photonView.IsMine) // Qを押したプレイヤー（MasterClient）の車は除く
        {
            carController.m_Topspeed = isCarStopped ? 0 : originalTopSpeed; // isCarStoppedがtrueなら0、そうでなければ元の速度に戻します

            // 効果音を再生または停止します
            if (isCarStopped && !stopSoundSource.isPlaying)
            {
                stopSoundSource.Play();
            }
            else if (!isCarStopped)
            {
                stopSoundSource.Stop();
            }

            // 見えるオブジェクトをネットワーク上に生成または削除します
            if (isCarStopped)
            {
                spawnedObject = PhotonNetwork.Instantiate(visibleObject.name, transform.position, Quaternion.identity);
            }
            else if (spawnedObject != null)
            {
                PhotonNetwork.Destroy(spawnedObject);
            }
        }
    }
}
