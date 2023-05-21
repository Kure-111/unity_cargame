using UnityEngine;
using Photon.Pun;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI countdownText;
    public GameObject raceStarter;

    private bool countdownStarted = false;

    private void Start()
    {
        raceStarter.SetActive(false); // Ensure race doesn't start until countdown
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        if (!countdownStarted && PhotonNetwork.IsMasterClient && PhotonNetwork.PlayerList.Length >= 1)
        {
            StartCoroutine(StartCountdown());
            countdownStarted = true;
        }
    }

    private IEnumerator StartCountdown()
    {
        int count = 3;
        while (count > 0)
        {
            photonView.RPC("SyncCountdown", RpcTarget.All, count);
            yield return new WaitForSeconds(1);
            count--;
        }

        photonView.RPC("StartRace", RpcTarget.All);
        yield return new WaitForSeconds(1);

        raceStarter.SetActive(true); // Begin the race here, enable your player, cars, etc.
    }

    [PunRPC]
    private void SyncCountdown(int count)
    {
        countdownText.text = count.ToString();
    }

    [PunRPC]
    private void StartRace()
    {
        countdownText.text = "GO!";
        countdownText.gameObject.SetActive(false);
    }
}
