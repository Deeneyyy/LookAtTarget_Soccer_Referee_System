using UnityEngine;

public class Randomize : MonoBehaviour
{
    // Movement speed
    public float moveSpeed = 2f;

    // Time interval for direction change
    public float directionChangeInterval = 2f;

    // Movement bounds (area limits)
    public Vector2 movementBoundsX = new Vector2(-10f, 10f); // Min and Max X
    public Vector2 movementBoundsZ = new Vector2(-10f, 10f); // Min and Max Z

    // Internal variables
    private Vector3 randomDirection;
    private float directionChangeTimer;

    void Start()
    {
        // Initialize with a random direction
        ChangeDirection();
    }

    void Update()
    {
        // Calculate the new position
        Vector3 newPosition = transform.position + randomDirection * moveSpeed * Time.deltaTime;

        // Clamp the position to stay within bounds
        newPosition.x = Mathf.Clamp(newPosition.x, movementBoundsX.x, movementBoundsX.y);
        newPosition.z = Mathf.Clamp(newPosition.z, movementBoundsZ.x, movementBoundsZ.y);

        // Apply the clamped position
        transform.position = newPosition;

        // Update the timer and change direction if needed
        directionChangeTimer -= Time.deltaTime;
        if (directionChangeTimer <= 0f)
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        // Generate a new random direction on the XZ plane
        randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;

        // Reset the timer
        directionChangeTimer = directionChangeInterval;
    }
}
