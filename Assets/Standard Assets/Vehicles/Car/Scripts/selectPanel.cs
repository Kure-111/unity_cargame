using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.UI;
namespace UnityStandardAssets.Vehicles.Car
{
    public class selectPanel : MonoBehaviourPunCallbacks
    {
        // Start is called before the first frame update
        public GameObject selection;
        public CarController car; // CarUserControlがアタッチされたオブジェクトを指定
        public Button ultimateButton; // 最強のタイプを適用するボタンを指定

        // 各パラメータのデフォルト値を設定します。
        private float defaultTorque = 2000f;
        private float defaultTractionControl = 0.5f;
        private float defaultTopSpeed = 200f;
        private float defaultDownForce = 100f;


        // ボタンを押すためのコマンドを設定します。
        private bool commandStarted = false;
        private bool sequenceA = false;
        private bool sequenceB = false;
        void Start()
        {
            selection.SetActive(false);

            if (photonView.IsMine)
            {
                // 初期化処理を行います。
                car.m_FullTorqueOverAllWheels = defaultTorque;
                car.m_TractionControl = defaultTractionControl;
                car.m_Topspeed = defaultTopSpeed;
                car.m_Downforce = defaultDownForce;
                car.m_CentreOfMassOffset = new Vector3(0, 0, 0);
                car.m_MaximumSteerAngle = 10;
                ultimateButton.gameObject.SetActive(false);
                selection.SetActive(true);
            }
            StartCoroutine(SetInactiveAfterSeconds(3));
        }

        // Update is called once per frame
        void Update()
        {
            if (photonView.IsMine && photonView.Owner == PhotonNetwork.MasterClient)
            {
                ultimateButton.gameObject.SetActive(true);
            }

        }
        IEnumerator SetInactiveAfterSeconds(int seconds)
        {
            yield return new WaitForSeconds(seconds);
            this.gameObject.SetActive(false);
            ultimateButton.gameObject.SetActive(false);
        }
        public void Ultimate()
        {
            if (photonView.IsMine)
            {
                // 最強のタイプのパラメータに変更します。
                car.m_FullTorqueOverAllWheels = 4000;
                car.m_TractionControl = 0.1f;
                car.m_Topspeed = 300;
                car.m_Downforce = 300;
                ultimateButton.gameObject.SetActive(false); // 最強のタイプを適用した後、ボタンを再び隠します。
            }
        }
        public void Acceleration()
        {
            if (photonView.IsMine) //現在操作しているプレイヤーが自分自身であるかを確認します。
            {
                //加速が良いタイプのパラメータに変更します。
                car.m_FullTorqueOverAllWheels = 5000;
                car.m_TractionControl = 0.1f;
                car.m_Topspeed = 70;
                car.m_MaximumSteerAngle = 50;
            }
        }

        public void Speed()
        {
            Debug.Log("faf");
            if (photonView.IsMine)
            {
                //トップスピードが高いタイプのパラメータに変更します。
                Debug.Log("Okkk");
                car.m_FullTorqueOverAllWheels = 2000;
                car.m_TractionControl = 0.1f;
                car.m_Topspeed = 150;
                car.m_Downforce = 200;
                car.m_MaximumSteerAngle = 50;
            }
        }
        public void Normal()
        {
            if (photonView.IsMine)
            {
                //ノーマルタイプのパラメータに変更します。
                car.m_FullTorqueOverAllWheels = 2500;
                car.m_TractionControl = 0.1f;
                car.m_Topspeed = 80;
                car.m_Downforce = 150;
                car.m_MaximumSteerAngle = 50;
            }
        }
        public void Durable()
        {
            if (photonView.IsMine)
            {
                //体当たりに強いタイプのパラメータに変更します。
                car.m_FullTorqueOverAllWheels = 2500;
                car.m_TractionControl = 0.1f;
                car.m_Topspeed = 80;
                car.m_Downforce = 300;
                car.m_CentreOfMassOffset = new Vector3(0, -0.5f, 0);
                car.m_MaximumSteerAngle = 50;
            }
        }
    }
}