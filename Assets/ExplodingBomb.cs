using UnityEngine;

public class ExplodingBomb : MonoBehaviour
{
    public GameObject explosionEffect; // 爆発エフェクトのプレハブをInspectorからアタッチします。
    public float explosionForce = 1000f; // 吹っ飛びの力を指定します。

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // 爆発エフェクトを作成します。
            Instantiate(explosionEffect, transform.position, transform.rotation);

            // プレイヤーの車を特定の方向に吹っ飛ばします。
            Rigidbody playerRigidbody = other.gameObject.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                playerRigidbody.AddExplosionForce(explosionForce, transform.position, 5f);
            }

            // 爆弾オブジェクトを削除します。
            Destroy(gameObject);
        }
    }
}
