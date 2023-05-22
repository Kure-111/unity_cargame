using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class MasterClientButton : MonoBehaviour
{
    public Button yourButton;

    void Start()
    {


        // ボタンのクリックイベントにリスナーを追加
        yourButton.onClick.AddListener(ReturnToTitle);
    }

    void ReturnToTitle()
    {


        // ルームから退室
        PhotonNetwork.LeaveRoom();
        Debug.Log(GlobalGameData.finishOrder);

        // タイトルシーンに遷移
        SceneManager.LoadScene("Title");
        GlobalGameData.finishOrder.Clear();

    }
}
