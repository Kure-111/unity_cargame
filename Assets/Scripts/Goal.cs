using UnityEngine;
using Photon.Pun;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Retrieve the PhotonView component from the player object
            PhotonView photonView = other.gameObject.GetComponent<PhotonView>();

            // If the player object has a PhotonView and it has an owner, get the owner's name
            string playerName = photonView != null && photonView.Owner != null ? photonView.Owner.NickName : "Unknown";

            // Assuming the GameControl component is attached to the same GameObject as this script
            GetComponent<GameControl>().PlayerFinished(playerName);

            Debug.Log("OK!");
        }
    }
}


