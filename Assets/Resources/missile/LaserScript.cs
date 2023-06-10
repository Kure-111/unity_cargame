using UnityEngine;

public class LaserScript : MonoBehaviour
{
    public Transform missilePrefab; // 発射するミサイルのプレハブ
    private Transform currentTarget; // 現在のロックオンターゲット
    private LineRenderer lineRenderer; // レーザーを描画するためのLineRenderer
    public float range = 10f; // レーザーの射程

    void Start()
    {
        // LineRendererコンポーネントを取得します
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2; // 頂点数を2に設定します
    }

    // Updateはフレームごとに呼び出されます
    void Update()
    {
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
            // 現在のターゲットに向けてミサイルを発射します
            if (currentTarget != null)
            {
                Transform missile = Instantiate(missilePrefab, transform.position, Quaternion.identity);
                missile.GetComponent<MissileScript>().target = currentTarget;
            }
        }
    }
}
