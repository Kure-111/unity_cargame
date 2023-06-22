using UnityEngine;
using Photon.Pun;
using UnityStandardAssets.Vehicles.Car;
using System.Collections;

public class Boost : MonoBehaviourPunCallbacks
{
    private CarController carController;
    private bool usedBoost = false;
    private float originalTopSpeed;
    private float originalMotorTorque;

    void Start()
    {
        carController = GetComponent<CarController>();
        originalMotorTorque = carController.m_WheelColliders[0].motorTorque;
    }

    void Update()
    {
        if (photonView.IsMine && !usedBoost && Input.GetKeyDown(KeyCode.Return))
        {
            usedBoost = true;
            originalTopSpeed = carController.m_Topspeed;
            carController.m_Topspeed *= 2f;
            foreach (var wheel in carController.m_WheelColliders)
            {
                wheel.motorTorque = originalMotorTorque * 2f; // This might need to be adjusted
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
            wheel.motorTorque = originalMotorTorque;
        }
        usedBoost = false;
    }
}
