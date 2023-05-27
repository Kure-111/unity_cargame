using UnityEngine;
using Photon.Pun;

public class SpectatorCameraController : MonoBehaviourPun
{
    public float movementSpeed = 10.0f;  // カメラの移動速度
    public float zoomSpeed = 1000f;  // ズームの速度
    public Camera cam;
    private Quaternion normalRotation;
    private Quaternion qPressedRotation;

    void Start()
    {
        // カメラを上から見下ろすように回転させる
        transform.rotation = Quaternion.Euler(90, 0, 0);
        qPressedRotation = Quaternion.Euler(35, 0, 0);

    }

    void Update()
    {
        // 現在のクライアントがマスタークライアントかどうかを確認
        if (photonView.IsMine)
        {
            cam.enabled = true;
            if (PhotonNetwork.IsMasterClient)
            {
                // WASDキーでカメラの移動
                if (Input.GetKey(KeyCode.W))
                    transform.position += transform.up * movementSpeed * Time.deltaTime;

                if (Input.GetKey(KeyCode.S))
                    transform.position -= transform.up * movementSpeed * Time.deltaTime;

                if (Input.GetKey(KeyCode.A))
                    transform.position -= transform.right * movementSpeed * Time.deltaTime;

                if (Input.GetKey(KeyCode.D))
                    transform.position += transform.right * movementSpeed * Time.deltaTime;

                // マウススクロールでズームイン/アウト
                float scroll = Input.GetAxis("Mouse ScrollWheel");
                transform.position += transform.forward * scroll * zoomSpeed * Time.deltaTime;
                if (Input.GetKey(KeyCode.Q))
                {
                    transform.rotation = qPressedRotation;
                }
                else
                {
                    transform.rotation = normalRotation;
                }
            }
        }
        else
        {
            cam.enabled = false;
        }
    }
}

