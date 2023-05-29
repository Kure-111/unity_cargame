using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        // Qキーが押されたら
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // 自分の位置を(0, 0, 0)にセット
            this.transform.position = Vector3.zero;
            // 自分の回転を0にセット
            this.transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }
}
