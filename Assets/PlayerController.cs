using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Speed of movement
    public float moveSpeed = 5f;

    void Update()
    {
        // Get input from WASD or arrow keys
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement vector
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput);

        // Normalize the direction to ensure consistent speed in all directions
        if (moveDirection.magnitude > 1f)
        {
            moveDirection = moveDirection.normalized;
        }

        // Move the object
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }
}
