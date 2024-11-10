using UnityEngine;

public class Creature : MonoBehaviour
{
    public float DetectionDistance;
    public int FieldOfView;
    public int DeltaAngle;

    public float Speed;
    public float RotationSpeed;
    public float SphereCastRadius = 1;

    void Update()
    {
        MoveBoid();
    }

    private void FixedUpdate()
    {
        var position = transform.position;

        var forward = transform.TransformDirection(Vector3.forward);
        if (Physics.SphereCast(position, SphereCastRadius, forward, out var hit, DetectionDistance))
        {
            // Debug.DrawRay(position, forward * DetectionDistance, Color.red);
            var bestDirection = BestDirection();
            RotateBoid(bestDirection);
        }
        else
        {
            // Debug.DrawRay(position, forward * DetectionDistance, Color.white);
        }
    }

    private Vector3 BestDirection()
{
    var position = transform.position;
    var bestDirection = Vector3.zero;
    var maxDistance = 0f;

    // Iterate through angles within the field of view
    for (var i = 1; i <= FieldOfView / DeltaAngle; i++)
    {
        var forwardAngle = DeltaAngle * i * Mathf.Deg2Rad;
        var z = Mathf.Cos(forwardAngle);

        for (int j = 0; j <= 360 / DeltaAngle; j++)
        {
            var horizontalAngle = (DeltaAngle * j + 90) % 360 * Mathf.Deg2Rad;
            var x = Mathf.Cos(horizontalAngle) * Mathf.Sin(forwardAngle);
            var y = Mathf.Sin(horizontalAngle) * Mathf.Sin(forwardAngle);

            var direction = new Vector3(x, y, z).normalized;
            var worldDirection = transform.rotation * direction;
            
            if (Physics.SphereCast(position, SphereCastRadius, worldDirection, out var hit, DetectionDistance))
            {
                // Debug.DrawRay(position, worldDirection * hit.distance, Color.yellow);
                
                if (hit.distance > maxDistance)
                {
                    bestDirection = worldDirection;
                    maxDistance = hit.distance;
                }
            }
            else
            {
                // Debug.DrawRay(position, worldDirection * DetectionDistance, Color.green);

                return worldDirection;
            }
        }
    }
    
    // Debug.DrawRay(position, bestDirection * DetectionDistance, Color.green);
    return bestDirection;
}

private void RotateBoid(Vector3 direction)
{
    if (direction != Vector3.zero)
    {
        var targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
    }
}

    private void MoveBoid()
    {
        var offset = transform.forward * (Speed * Time.deltaTime);
        transform.position += offset;
    }
}