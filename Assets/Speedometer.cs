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

    void Update()
    {
        // Calculate the speed in m/s
        if (!photonView.IsMine)
        {
            speedmetar.gameObject.SetActive(false);
            return;
        }
        float speedMS = targetRigidbody.velocity.magnitude;

        // Convert the speed to km/h and round to nearest integer
        speedKPH = Mathf.RoundToInt(speedMS * 3.6f);

        // Print the speed to the console
        speedmetar.text = "Speed: " + speedKPH + " km/h";
    }
}
