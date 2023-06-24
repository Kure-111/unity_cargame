using UnityEngine;
using Photon.Pun;

public class SpectatorCameraController : MonoBehaviourPun
{
    public float movementSpeed = 10.0f;  // カメラの移動速度
    public float zoomSpeed = 1000f;  // ズームの速度
    public Camera cam;

    public float mouseSensitivity = 100.0f;  // マウスの感度
    private float xRotation = 0.0f;
    void Start()
    {
        // カメラを上から見下ろすように回転させる
        transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    void Update()
    {

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
                // マウスで視点変更
                float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, -90f, 90f); // これによりカメラが上下に180度以上回転するのを防ぎます

                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                cam.transform.localRotation = Quaternion.Euler(xRotation, mouseX, 0f);

            }
        }
        else
        {
            cam.enabled = false;
        }

    }
}
