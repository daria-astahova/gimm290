using UnityEngine;

public class BasicWanderingAI : MonoBehaviour
{
    public float speed = 3.0f;
    public float obstacleRange = 5.0f;
    public float raycastDistance = 10.0f; // Define raycast distance
    public GameObject fireballPrefab; // Prefab for the fireball
    private bool _alive;

    void Start()
    {
        _alive = true;
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * raycastDistance, Color.red); // Draw ray with defined distance

        if (_alive)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
            // Perform obstacle avoidance
            PerformObstacleAvoidance();

            // Shoot fireball if player detected
            ShootFireball();
        }
    }

    public void SetAlive(bool alive)
    {
        _alive = alive;
    }

    void PerformObstacleAvoidance()
    {
        // Create a ray pointing forward from the AI's position
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // Perform a sphere cast to detect obstacles
        if (Physics.SphereCast(ray, 0.75f, out hit))
        {
            if (hit.distance < obstacleRange)
            {
                // If an obstacle is detected within the obstacle range, turn away from it
                float angle = Random.Range(-110, 110); // Generate a random angle
                transform.Rotate(0, angle, 0); // Rotate the AI by the generated angle
            }
        }
    }

    void ShootFireball()
    {
        // Create a ray pointing forward from the AI's position
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // Perform a sphere cast to detect the player
        if (Physics.SphereCast(ray, 0.75f, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;
            if (hitObject.GetComponent<PlayerCharacter>())
            {
                // Check if fireball is not already spawned
                if (GameObject.FindGameObjectWithTag("Fireball") == null)
                {
                    // Instantiate the fireball prefab
                    GameObject fireball = Instantiate(fireballPrefab, transform.position + transform.forward * 1.5f, transform.rotation);
                    // Set the fireball's direction and speed
                    fireball.GetComponent<Rigidbody>().velocity = transform.forward * 10.0f;
                }
            }
        }
    }
}
