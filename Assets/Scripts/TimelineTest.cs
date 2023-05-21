using UnityEngine;
using UnityEngine.Playables;    //これを忘れずに追加する

public class TimelineTest : MonoBehaviour
{
    [SerializeField] private PlayableDirector _director;

    private void Start()
    {
        _director.Play();
    }
}