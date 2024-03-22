using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 10.0f;
    public int damage = 1;

    void Update()
    {
        // Move the fireball forward
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is the player
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();
        if (player != null)
        {
            // If the collided object is the player, damage the player
            player.Hurt(damage);
        }

        // Destroy the fireball when it collides with anything
        Destroy(gameObject);
    }
}
