using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

[RequireComponent(typeof(CarController))]
public class CarDriftController : MonoBehaviour
{
    private CarController carController;
    private WheelCollider[] wheelColliders;

    public float driftFactor = 0.5f;

    private void Awake()
    {
        carController = GetComponent<CarController>();
        wheelColliders = carController.GetComponentsInChildren<WheelCollider>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyDriftFactor();
        }
        else
        {
            RemoveDriftFactor();
        }
    }

    private void ApplyDriftFactor()
    {
        var localVelocity = transform.InverseTransformDirection(carController.m_Rigidbody.velocity);
        foreach (var wc in wheelColliders)
        {
            WheelFrictionCurve fFriction = wc.forwardFriction;
            fFriction.stiffness = 1 - (driftFactor * Mathf.Abs(localVelocity.x));
            wc.forwardFriction = fFriction;
        }
    }

    private void RemoveDriftFactor()
    {
        foreach (var wc in wheelColliders)
        {
            WheelFrictionCurve fFriction = wc.forwardFriction;
            fFriction.stiffness = 1;
            wc.forwardFriction = fFriction;
        }
    }
}
