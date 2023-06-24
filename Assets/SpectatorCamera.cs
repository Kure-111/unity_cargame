using UnityEngine;
using Photon.Pun;

public class SpectatorCamera : MonoBehaviour
{
    public float speed = 10.0f; // Free Lookモードでの移動速度
    public float sensitivity = 2.0f; // マウスの感度
    public float zoomSpeed = 10.0f; // ズームの速度
    public float riseSpeed = 5.0f; // 上昇速度

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();

        // このカメラがマスタークライアント以外には見えないようにします。
        if (PhotonNetwork.IsMasterClient)
        {
            cam.enabled = true;
        }
        else
        {
            cam.enabled = false;
        }
    }

    void Update()
    {
        // マスタークライアントのみがこのカメラを操作できます。
        if (PhotonNetwork.IsMasterClient)
        {
            // マウスの移動でカメラの回転
            float newRotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
            float newRotationY = transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * sensitivity;
            transform.localEulerAngles = new Vector3(newRotationY, newRotationX, 0f);

            // WASDで移動
            float xAxisValue = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            float zAxisValue = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            transform.Translate(new Vector3(xAxisValue, 0.0f, zAxisValue));

            // スペースキーで上昇
            if (Input.GetKey(KeyCode.Space))
            {
                transform.Translate(new Vector3(0, riseSpeed * Time.deltaTime, 0), Space.World);
            }

            // マウスホイールで拡大・縮小
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            transform.Translate(0, 0, scroll * zoomSpeed, Space.Self);
        }
    }
}
