using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ItemManager : MonoBehaviourPunCallbacks
{
    public GameObject playerCar;
    private Boost boost;
    private WallSpawn wallSpawn;

    void Start()
    {
        boost = playerCar.GetComponent<Boost>();
        wallSpawn = playerCar.GetComponent<WallSpawn>();
        boost.enabled = true;
        wallSpawn.enabled = false;
    }

    public void OnBoostButtonClicked()
    {
        if (photonView.IsMine)
        {
            Debug.Log("dasdsa");
            boost.enabled = true;
            wallSpawn.enabled = false;
        }
    }

    public void OnWallSpawnButtonClicked()
    {
        if (photonView.IsMine)
        {
            wallSpawn.enabled = true;
            boost.enabled = false;
        }
    }
}

