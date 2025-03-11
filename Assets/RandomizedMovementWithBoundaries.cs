using UnityEngine;

public class RandomizedMovementWithBoundaries : MonoBehaviour
{
    // Movement speed
    public float moveSpeed = 2f;

    // Time interval for direction change
    public float directionChangeInterval = 2f;

    // Movement bounds (area limits)
    public Vector2 movementBoundsX = new Vector2(-10f, 10f); // Min and Max X
    public Vector2 movementBoundsY = new Vector2(-5f, 5f);   // Min and Max Y

    // Internal variables
    private Vector2 randomDirection;
    private float directionChangeTimer;

    void Start()
    {
        // Initialize with a random direction
        ChangeDirection();
    }

    void Update()
    {
        // Calculate the new position
        Vector3 newPosition = transform.position + (Vector3)(randomDirection * moveSpeed * Time.deltaTime);

        // Clamp the position to stay within bounds
        newPosition.x = Mathf.Clamp(newPosition.x, movementBoundsX.x, movementBoundsX.y);
        newPosition.y = Mathf.Clamp(newPosition.y, movementBoundsY.x, movementBoundsY.y);

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
        // Generate a new random direction
        randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

        // Reset the timer
        directionChangeTimer = directionChangeInterval;
    }
}
