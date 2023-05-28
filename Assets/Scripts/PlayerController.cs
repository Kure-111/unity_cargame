using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using System.Collections;

public class PlayerController : MonoBehaviourPunCallbacks
{
    SpawnManager spawnManager;
    public string targetTag = "goal";
    public Vector3 minLocation;
    public Vector3 maxLocation;
    public TextMeshProUGUI messageText;
    public float messageDelay = 3.0f;
    private void Awake()
    {
        spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();
        messageText.gameObject.SetActive(false);
    }
    void Update()
    {
        if (photonView.IsMine)
        {

            if (Input.GetKeyDown(KeyCode.R))
            {
                spawnManager.Die();
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            Vector3 teleportLocation = new Vector3(
                Random.Range(minLocation.x, maxLocation.x),
                Random.Range(minLocation.y, maxLocation.y),
                Random.Range(minLocation.z, maxLocation.z)
            );

            other.transform.position = teleportLocation;
            if (photonView.IsMine)
            {
                messageText.gameObject.SetActive(true);
            }

            StartCoroutine(HideTextAfterDelay(messageDelay));
        }
    }
    IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        messageText.gameObject.SetActive(false);
    }
}
