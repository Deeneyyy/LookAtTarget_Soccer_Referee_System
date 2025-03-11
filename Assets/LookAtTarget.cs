using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    // Array of objects to target
    public Transform[] targets;

    // Materials for indicating active/inactive targets
    public Material activeMaterial;
    public Material inactiveMaterial;

    // Current target index
    private int currentTargetIndex = 0;

    // Orbiting object
    public Transform orbitingObject;

    // Minimum and maximum radii for the orbit
    public float minRadius = 3f;
    public float maxRadius = 7f;

    // Speed settings
    public float orbitSpeed = 100f;
    public float radiusAdjustSpeed = 2f;

    // Obstacle avoidance settings
    public float obstacleDetectionDistance = 2f;
    public float obstacleAvoidanceStrength = 5f;
    public LayerMask obstacleLayer;

    // Additional followers
    public Transform followerX; // Object moving along the X-axis
    public Transform followerZ; // Object moving along the Z-axis
    public float followSpeed = 5f;

    // Current state
    private float currentRadius;
    private float currentAngle = 0f;

    void Start()
    {
        if (targets.Length == 0)
        {
            Debug.LogError("No targets assigned!");
            return;
        }

        if (orbitingObject == null)
        {
            Debug.LogError("Orbiting object is not assigned!");
            return;
        }

        // Initialize the current radius
        currentRadius = (minRadius + maxRadius) / 2f;

        // Start with the first target
        SetTarget(0);
    }

    void Update()
    {
        if (targets.Length == 0 || orbitingObject == null) return;

        // Orbit around the current target
        Transform currentTarget = targets[currentTargetIndex];
        OrbitAroundTargetWithAvoidance(currentTarget);

        // Move followers towards the current target
        MoveFollowerX(currentTarget);
        MoveFollowerZ(currentTarget);

        // Cycle targets using input
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            CycleTarget();
        }
    }

    void OrbitAroundTargetWithAvoidance(Transform target)
    {
        if (target == null) return;

        // Get user input for angle and radius adjustments
        float angleInput = Input.GetAxis("Horizontal");
        float radiusInput = Input.GetAxis("Vertical");

        // Adjust the angle dynamically
        currentAngle += angleInput * Time.deltaTime * orbitSpeed;
        if (currentAngle >= 360f) currentAngle -= 360f;
        if (currentAngle < 0f) currentAngle += 360f;

        // Adjust the radius dynamically
        currentRadius += radiusInput * Time.deltaTime * radiusAdjustSpeed;
        currentRadius = Mathf.Clamp(currentRadius, minRadius, maxRadius);

        // Convert the angle to radians
        float angleInRadians = currentAngle * Mathf.Deg2Rad;

        // Calculate the desired position based on the orbit radius and angle
        Vector3 desiredPosition = new Vector3(
            target.position.x + currentRadius * Mathf.Cos(angleInRadians),
            orbitingObject.position.y,
            target.position.z + currentRadius * Mathf.Sin(angleInRadians)
        );

        // Calculate the direction to the desired position
        Vector3 directionToTarget = (desiredPosition - orbitingObject.position).normalized;

        // Obstacle avoidance logic
        if (Physics.Raycast(orbitingObject.position, directionToTarget, out RaycastHit hit, obstacleDetectionDistance, obstacleLayer))
        {
            // Obstacle detected! Adjust the movement direction to avoid the obstacle
            Vector3 avoidanceDirection = Vector3.Cross(hit.normal, Vector3.up).normalized;
            desiredPosition += avoidanceDirection * obstacleAvoidanceStrength;
        }

        // Move towards the desired position
        orbitingObject.position = Vector3.MoveTowards(orbitingObject.position, desiredPosition, orbitSpeed * Time.deltaTime);

        // Always look at the target
        orbitingObject.LookAt(target);
    }

    void MoveFollowerX(Transform target)
    {
        if (followerX == null || target == null) return;

        // Maintain the same Z and Y positions, move only along the X-axis
        Vector3 targetPosition = new Vector3(target.position.x, followerX.position.y, followerX.position.z);

        // Smoothly move the follower
        followerX.position = Vector3.MoveTowards(followerX.position, targetPosition, followSpeed * Time.deltaTime);
    }

    void MoveFollowerZ(Transform target)
    {
        if (followerZ == null || target == null) return;

        // Maintain the same X and Y positions, move only along the Z-axis
        Vector3 targetPosition = new Vector3(followerZ.position.x, followerZ.position.y, target.position.z);

        // Smoothly move the follower
        followerZ.position = Vector3.MoveTowards(followerZ.position, targetPosition, followSpeed * Time.deltaTime);
    }

    void CycleTarget()
    {
        // Increment the target index and loop back to the start if needed
        currentTargetIndex = (currentTargetIndex + 1) % targets.Length;

        // Update the active indicator
        UpdateTargetIndicators();

        Debug.Log($"Switched to target: {targets[currentTargetIndex].name}");
    }

    void SetTarget(int index)
    {
        if (index < 0 || index >= targets.Length)
        {
            Debug.LogError("Invalid target index!");
            return;
        }

        currentTargetIndex = index;

        // Update the active indicator
        UpdateTargetIndicators();

        Debug.Log($"Target set to: {targets[currentTargetIndex].name}");
    }

    void UpdateTargetIndicators()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            Renderer renderer = targets[i].GetComponent<Renderer>();
            if (renderer != null)
            {
                // Apply active material to the current target
                if (i == currentTargetIndex)
                {
                    renderer.material = activeMaterial;
                }
                else
                {
                    renderer.material = inactiveMaterial;
                }
            }
        }
    }
}
