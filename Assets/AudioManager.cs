using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        StartCoroutine(PlayBGMAfterDelay(3f)); // 3秒後に音を再生する
    }

    IEnumerator PlayBGMAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.Play();
    }
}
