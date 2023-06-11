using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class BoostItem : MonoBehaviour{
private bool isSpeedBoosted = false;
public CarController car;
void Start()
{
    StartCoroutine(ActivateSkill());
}

//ブースト開始
IEnumerator ActivateSkill()
{
    while (true)
    {
        yield return new WaitForSeconds(30f);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!isSpeedBoosted)
            {
                StartCoroutine(BoostSpeed());
            }
        }
    }
}

IEnumerator BoostSpeed()
{
    float originalSpeed = car.m_Topspeed;
    float targetSpeed = originalSpeed * 1.5f;
    float elapsedTime = 0f;

    isSpeedBoosted = true;
    car.m_Topspeed = targetSpeed;
    car.m_Topspeed = car.GetComponent<CarController>().m_FullTorqueOverAllWheels / car.GetComponent<CarController>().m_RevRangeBoundary; // 現在の速度をtopspeedまで瞬時に加速

    while (elapsedTime < 6f)
    {
        elapsedTime += Time.deltaTime;
        //car.m_Topspeed = Mathf.Lerp(originalSpeed, targetSpeed, elapsedTime / 6f);
        car.m_Topspeed = car.GetComponent<CarController>().m_FullTorqueOverAllWheels / car.GetComponent<CarController>().m_RevRangeBoundary; // 現在の速度をtopspeedまで瞬時に加速
        yield return null;
    }

    car.m_Topspeed = originalSpeed;
    isSpeedBoosted = false;
}
}
