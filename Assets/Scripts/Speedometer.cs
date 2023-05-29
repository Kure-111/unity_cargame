using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
public class Speedometer : MonoBehaviourPun
{
    public Rigidbody targetRigidbody; // The Rigidbody you want to measure the speed of
    private int speedKPH;
    public TextMeshProUGUI speedmetar;
    void Start()
    {
        speedmetar.text = "";
    }

    void Update()
    {
        float speedMS = targetRigidbody.velocity.magnitude;
        if (photonView.IsMine)
        {

            speedmetar.gameObject.SetActive(true);
            speedKPH = Mathf.RoundToInt(speedMS * 3.6f);
            speedmetar.text = "Speed: " + speedKPH + " km/h";

        }
        else
        {
            speedmetar.gameObject.SetActive(false);
        }

        //speedKPH = Mathf.RoundToInt(speedMS * 3.6f);
        //speedmetar.text = "Speed: " + speedKPH + " km/h";


    }
}
