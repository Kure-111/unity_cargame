using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R : MonoBehaviour
{
    public Camera mainCamera;
    private bool isVisible;

    void Start()
    {
        // 開始時にカメラを非表示にする
        mainCamera.enabled = false;
        isVisible = false;
    }

    void Update()
    {
        // Rキーが押されたとき
        if (Input.GetKeyDown(KeyCode.R) && !isVisible)
        {
            StartCoroutine(ToggleCameraVisibility());
        }
    }

    IEnumerator ToggleCameraVisibility()
    {
        // カメラを表示する
        mainCamera.enabled = true;
        isVisible = true;

        // 4秒間待つ
        yield return new WaitForSeconds(3.5f);

        // カメラを再び非表示にする
        mainCamera.enabled = false;
        isVisible = false;
    }
}

