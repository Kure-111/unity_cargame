using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Teleporter : MonoBehaviour
{
    public string targetTag = "goal";

    public TextMeshProUGUI messageText;
    public float messageDelay = 3.0f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            Vector3 teleportLocation = new Vector3(1000, 0, 0);
            Destroy(this.gameObject);
            other.transform.position = teleportLocation;
            messageText.gameObject.SetActive(true);
            StartCoroutine(HideTextAfterDelay(messageDelay));
        }
    }

    IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        messageText.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
}