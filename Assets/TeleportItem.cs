using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class TeleportItem : MonoBehaviourPunCallbacks, IPunObservable
{
    public GameObject teleportMarkerPrefab; // テレポートマーカーのプレハブ

    private GameObject teleportMarker; // テレポートマーカーのインスタンス
    private bool isSettingTeleportPosition; // テレポート位置を設定中かどうかのフラグ
    private Vector3 teleportPosition; // テレポート先の位置

    private PhotonView photonView; // PhotonViewコンポーネントの参照

    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (!photonView.IsMine) return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!isSettingTeleportPosition)
            {
                // テレポート位置を設定中でない場合、テレポート位置の設定を開始します
                StartSettingTeleportPosition();
            }
            else
            {
                // テレポート位置を設定中の場合、テレポートを実行します
                PerformTeleport();
            }
        }
    }

    private void StartSettingTeleportPosition()
    {
        // テレポートマーカーを生成します
        teleportMarker = Instantiate(teleportMarkerPrefab, transform.position, Quaternion.identity);
        isSettingTeleportPosition = true;

        // テレポートマーカーの位置情報を他のプレイヤーに同期します
        photonView.RPC("SyncTeleportMarkerPosition", RpcTarget.OthersBuffered, teleportMarker.transform.position);
    }

    private void PerformTeleport()
    {
        // テレポート先の位置を設定します
        teleportPosition = teleportMarker.transform.position;

        // スピードをゼロにします
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;

        // 回転をゼロにします
        rb.angularVelocity = Vector3.zero;
        transform.eulerAngles = new Vector3(0f, -90f, 0f);
        // テレポート先の位置に移動します
        gameObject.transform.position = teleportPosition;

        // テレポートマーカーを破棄します
        Destroy(teleportMarker);

        // テレポート位置を設定中のフラグをリセットします
        isSettingTeleportPosition = false;
    }


    [PunRPC]
    private void SyncTeleportMarkerPosition(Vector3 position)
    {
        // テレポートマーカーの位置を同期します
        if (teleportMarker == null)
        {
            // テレポートマーカーが生成されていない場合は生成します
            teleportMarker = Instantiate(teleportMarkerPrefab, position, Quaternion.identity);
        }
        else
        {
            // テレポートマーカーが既に存在する場合は位置を更新します
            teleportMarker.transform.position = position;
        }
    }

    [PunRPC]
    private void SyncTeleportPosition(Vector3 position)
    {
        // テレポート位置を同期します
        teleportPosition = position;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // テレポートマーカーの位置情報をシリアライズ/デシリアライズします
        if (stream.IsWriting)
        {
            // データの送信時に位置情報を送信します
            stream.SendNext(teleportMarker.transform.position);
        }
        else
        {
            // データの受信時に位置情報を受信します
            Vector3 position = (Vector3)stream.ReceiveNext();
            if (teleportMarker == null)
            {
                // テレポートマーカーが生成されていない場合は生成します
                teleportMarker = Instantiate(teleportMarkerPrefab, position, Quaternion.identity);
            }
            else
            {
                // テレポートマーカーが既に存在する場合は位置を更新します
                teleportMarker.transform.position = position;
            }
        }
    }
}





