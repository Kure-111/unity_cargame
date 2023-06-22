using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public float speed = 5.0f;
    public float maxZ = 20.0f;
    public float minZ = -20.0f;

    private int direction = 1;

    void Update()
    {
        float newZ = transform.position.z + direction * speed * Time.deltaTime;
        if (newZ > maxZ)
        {
            newZ = maxZ;
            direction = -1;
        }
        else if (newZ < minZ)
        {
            newZ = minZ;
            direction = 1;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
    }
}
