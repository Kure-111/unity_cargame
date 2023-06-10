using System.Collections;
using UnityEngine;
using Photon.Pun;

public class FreezeScript : MonoBehaviourPunCallbacks
{
    public float freezeDuration = 1f; // 凍結の期間
    private Rigidbody carRigidbody; // 車のRigidbodyへの参照

    private void Start()
    {
        // Rigidbodyコンポーネントを取得
        carRigidbody = GetComponent<Rigidbody>();
    }

    [PunRPC]
    public void Freeze()
    {
        StartCoroutine(FreezeCoroutine());
    }

    private IEnumerator FreezeCoroutine()
    {
        // 元の制約を保存
        RigidbodyConstraints originalConstraints = carRigidbody.constraints;

        // 車の位置と回転を凍結
        carRigidbody.constraints = RigidbodyConstraints.FreezeAll;

        yield return new WaitForSeconds(freezeDuration);

        // 制約を元のものにリセット
        carRigidbody.constraints = originalConstraints;
    }
}
