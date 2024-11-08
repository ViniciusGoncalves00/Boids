using UnityEngine;
using Random = UnityEngine.Random;

public class Creature : MonoBehaviour
{
    public float initialSpeed;
    public Vector3 velocity;
    public float speedLimit = 4f;
    public float boxSize = 10f;

    void Start()
    {
        // Initialize with a random velocity
        velocity = new Vector3(Random.Range(-initialSpeed, initialSpeed), Random.Range(-initialSpeed, initialSpeed), Random.Range(-initialSpeed, initialSpeed));
    }

    void Update()
    {
        MoveBoid();
    }

    void MoveBoid()
    {
        // Update position based on velocity
        transform.position += velocity * Time.deltaTime;

        // Check if the boid hits the boundaries of the box and reverse its direction if necessary
        if (Mathf.Abs(transform.position.x) > boxSize)
        {
            velocity.x *= -1;
            transform.position = new Vector3(Mathf.Sign(transform.position.x) * boxSize, transform.position.y, transform.position.z);
        }
        if (Mathf.Abs(transform.position.y) > boxSize)
        {
            velocity.y *= -1;
            transform.position = new Vector3(transform.position.x, Mathf.Sign(transform.position.y) * boxSize, transform.position.z);
        }
        if (Mathf.Abs(transform.position.z) > boxSize)
        {
            velocity.z *= -1;
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Sign(transform.position.z) * boxSize);
        }

        // Limit the speed
        if (velocity.magnitude > speedLimit)
        {
            velocity = velocity.normalized * speedLimit;
        }
    }
}