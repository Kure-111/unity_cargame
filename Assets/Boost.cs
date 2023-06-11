using UnityEngine;
using Photon.Pun;
using UnityStandardAssets.Vehicles.Car;
using System.Collections;

public class Boost : MonoBehaviourPunCallbacks
{
    private CarController carController;
    private bool usedBoost = false;
    private float originalTopSpeed;

    void Start()
    {
        carController = GetComponent<CarController>();
        originalTopSpeed = carController.m_Topspeed;
    }

    void Update()
    {
        if (photonView.IsMine && !usedBoost && Input.GetKeyDown(KeyCode.Return))
        {
            usedBoost = true;
            carController.m_Topspeed *= 1.5f;
            foreach (var wheel in carController.m_WheelColliders)
            {
                wheel.motorTorque = carController.m_Topspeed * 10f; // This might need to be adjusted
            }
            StartCoroutine(DisableBoostAfterSeconds(5f));
        }
    }

    private IEnumerator DisableBoostAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        carController.m_Topspeed = originalTopSpeed;
        foreach (var wheel in carController.m_WheelColliders)
        {
            wheel.motorTorque = 0f;
        }
    }
}
