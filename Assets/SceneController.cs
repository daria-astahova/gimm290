using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab; // Serialized variable for linking to the prefab object

    private GameObject _enemy; // Private variable to keep track of the enemy instance in the scene

    void Update()
    {
        // Spawn a new enemy if there isn't already one in the scene
        if (_enemy == null)
        {
            // Instantiate a new enemy GameObject using the assigned enemyPrefab
            _enemy = Instantiate(enemyPrefab) as GameObject;

            // Set the initial position of the spawned enemy
            _enemy.transform.position = new Vector3(22, 1, 22);

            // Randomly rotate the enemy around the y-axis for variety
            float angle = Random.Range(0, 360);
            _enemy.transform.Rotate(0, angle, 0);

            // Set a random color for the enemy
            Renderer renderer = _enemy.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Random.ColorHSV();
            }
            else
            {
                Debug.LogError("Renderer component not found on the enemy prefab.");
            }
        }
    }
}
