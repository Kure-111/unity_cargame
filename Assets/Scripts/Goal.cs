using UnityEngine;
using Photon.Pun;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // プレイヤーオブジェクトからPhotonViewコンポーネントを取得
            PhotonView photonView = other.gameObject.GetComponent<PhotonView>();

            // プレイヤーオブジェクトにPhotonViewが存在し、オーナーが設定されている場合はそのオーナーの名前を取得
            string playerName = photonView != null && photonView.Owner != null ? photonView.Owner.NickName : "Unknown";

            // GameControlコンポーネントはこのスクリプトと同じGameObjectにアタッチされていると仮定
            GetComponent<GameControl>().PlayerFinished(playerName, other.gameObject);

            Debug.Log("OK!");
        }
    }
}
