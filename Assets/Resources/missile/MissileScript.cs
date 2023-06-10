using System.Collections;
using UnityEngine;

public class MissileScript : MonoBehaviour
{
    public Transform target; // ミサイルのターゲット
    public float speed = 5f; // ミサイルの速度
    public GameObject explosionEffect; // 爆発エフェクトのプレハブ
    private Rigidbody rb; // ミサイル自身のRigidbody

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbodyを取得
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f); // Y軸の回転を適用
    }

    void Update()
    {
        // ターゲットが存在すれば、その方向へ移動
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * speed;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // プレイヤーと衝突した場合
        if (collision.gameObject.CompareTag("Player"))
        {
            // 爆発エフェクトを生成
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

            // Freezeスクリプトを使ってプレイヤーの動きを止める
            FreezeScript freezeScript = collision.gameObject.GetComponent<FreezeScript>();
            if (freezeScript != null)
            {
                freezeScript.Freeze();
            }

            // ミサイルを破壊
            Destroy(gameObject);
        }
    }
}
