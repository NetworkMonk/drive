using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CollisionSoundEffect : MonoBehaviour
{
    [SerializeField] private AudioClip collisionSoundClip;

    void OnCollisionEnter(Collision collision)
    {
        // Calculate the impact force
        float impactForce = collision.relativeVelocity.magnitude;

        // Map the impact force to a volume level (0.0 to 1.0)
        float volume = Mathf.Clamp01(impactForce / 20.0f); // Adjust the divisor to control sensitivity

        // Instantiate a new GameObject at the point of impact
        GameObject audioObject = new GameObject("CollisionAudio");
        audioObject.transform.position = collision.contacts[0].point;

        // Add an AudioSource component to the new GameObject
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioSource.clip = collisionSoundClip;
        audioSource.volume = volume;
        audioSource.spatialBlend = 1.0f; // Set to fully 3D
        audioSource.Play();

        // Destroy the new GameObject after the audio has finished playing
        Destroy(audioObject, collisionSoundClip.length);
    }
}