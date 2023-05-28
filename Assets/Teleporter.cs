using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public string targetTag = "goal";
    public Vector3 minLocation;
    public Vector3 maxLocation;

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
        }
    }
}
