using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour {
    public AudioClip hitSound; // Sound to play when hit
    public GameObject hitEffect; // Particle effect to spawn when hit

    private AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void ReactToHit()
{
    // Check if the game object has the WanderingAI component attached
    BasicWanderingAI behavior = GetComponent<BasicWanderingAI>();

    // If the WanderingAI component is found, set its alive state to false
    if (behavior != null)
    {
        behavior.SetAlive(false);
    }
    
    // Start the coroutine to handle the death animation or cleanup
    StartCoroutine(Die());
}

    private IEnumerator Die() {
        this.transform.Rotate(-75, 0, 0);
        
        this.transform.Translate(0, 0, -1f);
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }

    private void PlayHitSound() {
        if (hitSound != null && audioSource != null) {
            audioSource.PlayOneShot(hitSound);
        }
    }

    private void SpawnHitEffect() {
        if (hitEffect != null) {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
        }
    }
}
