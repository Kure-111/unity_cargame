using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using System.Collections;

public class PlayerController : MonoBehaviourPunCallbacks
{
    private Vector3 startPosition;
    private Quaternion startRotation;
    private Rigidbody carRigidbody;
    private PhotonView photonView;

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        photonView = GetComponent<PhotonView>();
        carRigidbody = GetComponent<Rigidbody>(); 
    }

    void Update()
    {
        if (photonView.IsMine && Input.GetKeyDown(KeyCode.R))
        {
            transform.position = startPosition;
            transform.rotation = startRotation;

            carRigidbody.velocity = Vector3.zero;
            carRigidbody.angularVelocity = Vector3.zero;
        }
    }
}
