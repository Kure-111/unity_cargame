using UnityEngine;
using Photon.Pun;

public class LaserScript : MonoBehaviour
{
    public Transform missilePrefab; // 発射するミサイルのプレハブ
    private Transform currentTarget; // 現在のロックオンターゲット
    private LineRenderer lineRenderer; // レーザーを描画するためのLineRenderer
    public float range = 10f; // レーザーの射程

    private PhotonView photonView; // PhotonViewコンポーネント

    void Start()
    {
        // LineRendererコンポーネントを取得します
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2; // 頂点数を2に設定します

        // PhotonViewコンポーネントを取得します
        photonView = GetComponent<PhotonView>();
    }

    // Updateはフレームごとに呼び出されます
    void Update()
    {
        // 自分のプレイヤーでない場合は処理をスキップします
        if (!photonView.IsMine) return;

        // このオブジェクトから前方にレイを飛ばします
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            // ヒットした場所までのレーザーを描画します
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.point);

            // ヒットしたオブジェクトがプレイヤーの車かどうかを確認します
            if (hit.transform.CompareTag("Player"))
            {
                // この車にロックオンします
                currentTarget = hit.transform;
            }
        }
        else
        {
            // レイが何もヒットしなかった場合は、レーザーの終点を射程の最大距離にします
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position + transform.forward * range);
        }

        // Enterキーが押されたかどうかを確認します
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (currentTarget != null)
            {
                // ロックオンしたプレイヤーに対してミサイルを発射します
                Vector3 spawnPosition = transform.position;
                Quaternion rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + 90f, 0f); // Y軸の回転を適用

                Transform missile = PhotonNetwork.Instantiate(missilePrefab.name, spawnPosition, rotation).transform;
                missile.GetComponent<MissileScript>().target = currentTarget;
            }
        }
    }
}
