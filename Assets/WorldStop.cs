using UnityEngine;
using Photon.Pun;
using UnityStandardAssets.Vehicles.Car;

public class WorldStop : MonoBehaviourPunCallbacks
{
    public AudioSource stopSoundSource;
    public GameObject visibleObject;
    private CarController carController;
    private PhotonView photonView;
    private bool isCarStopped = false;
    private GameObject spawnedObject;
    private float originalTopSpeed;

    void Start()
    {
        carController = GetComponent<CarController>();
        photonView = GetComponent<PhotonView>();
        originalTopSpeed = carController.m_Topspeed;
    }

    void Update()
    {
        // Only the master client can trigger the stop
        if (Input.GetKeyDown(KeyCode.Q) && PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("ToggleCarStop", RpcTarget.All);
        }
    }

    [PunRPC]
    public void ToggleCarStop()
    {
        isCarStopped = !isCarStopped;

        // Apply the stop/start to all players, except for the master client
        if (carController != null && photonView.Owner != PhotonNetwork.MasterClient)
        {
            carController.m_Topspeed = isCarStopped ? 0 : originalTopSpeed;

            if (isCarStopped && !stopSoundSource.isPlaying)
            {
                stopSoundSource.Play();
            }
            else if (!isCarStopped)
            {
                stopSoundSource.Stop();
            }

            // Only instantiate/destroy the object for non-master players
            if (isCarStopped)
            {
                spawnedObject = PhotonNetwork.Instantiate(visibleObject.name, transform.position, Quaternion.identity);
            }
            else if (spawnedObject != null)
            {
                PhotonNetwork.Destroy(spawnedObject);
            }
        }
    }
}
