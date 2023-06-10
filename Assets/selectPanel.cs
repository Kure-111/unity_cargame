using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
public class selectPanel : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public GameObject selection;
    void Start()
    {
        selection.SetActive(false);
        if (photonView.IsMine)
        {
            selection.SetActive(true);
        }
        StartCoroutine(SetInactiveAfterSeconds(3));
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator SetInactiveAfterSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        this.gameObject.SetActive(false);
    }
}
