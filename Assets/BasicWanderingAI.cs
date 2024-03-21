using UnityEngine;

public class BasicWanderingAI : MonoBehaviour
{
    public float moveSpeed = 3f; // Speed at which the AI moves
    public float rotationSpeed = 100f; // Speed at which the AI rotates
    public float raycastDistance = 2f; // Distance to cast rays for obstacle detection

    void Update()
    {
        // Move the AI forward
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        // Check for obstacles
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance))
        {
            // Calculate the distance from the obstacle's collider
            float distanceToCollider = hit.distance - 0.5f * transform.localScale.z; // Assuming the AI has a box collider

            // Move away from the obstacle
            Vector3 moveDirection = Quaternion.AngleAxis(45f, transform.up) * transform.forward; // Rotate 45 degrees to the right
            Vector3 targetPosition = transform.position + moveDirection * distanceToCollider;

            // Rotate towards the target position
            Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
