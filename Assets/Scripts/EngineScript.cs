using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EngineScript : MonoBehaviour
{
    [SerializeField] private AnimationCurve engineCurve;
    [SerializeField] private AudioClip engineSoundClip;
    private AudioSource engineAudioSource;
    public float maxSpeed = 50.0f;
    public float currentSpeed = 0.0f;
    private float targetVolume = 0.2f;
    private float volumeChangeSpeed = 6.5f; // Adjust this value to control how fast the volume changes
    private float pitchChangeSpeed = 6.5f; // Adjust this value to control how fast the pitch changes

    // Start is called before the first frame update
    void Start()
    {
        engineAudioSource = gameObject.AddComponent<AudioSource>();
        engineAudioSource.clip = engineSoundClip;
        engineAudioSource.loop = true;
        engineAudioSource.volume = 0.0f; // Initialize volume to 0
        engineAudioSource.spatialBlend = 1.0f; // Enable 3D spatial sound
        engineAudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed = GetComponent<Rigidbody>().velocity.magnitude;
        float normalizedSpeed = currentSpeed / maxSpeed;
        targetVolume = Mathf.Max(Mathf.Clamp01(normalizedSpeed * 0.45f), 0.2f);
        float targetPitch = Mathf.Lerp(0.4f, 1.25f, normalizedSpeed);

        // Smoothly interpolate volume and pitch
        engineAudioSource.volume = Mathf.Lerp(engineAudioSource.volume, targetVolume, volumeChangeSpeed * Time.deltaTime);
        engineAudioSource.pitch = Mathf.Lerp(engineAudioSource.pitch, targetPitch, pitchChangeSpeed * Time.deltaTime);
    }

    public float GetEngineTorque(float currentVelocity)
    {
        float normalizedVelocity = currentVelocity / maxSpeed;
        float torque = engineCurve.Evaluate(normalizedVelocity);
        return torque;
    }
}
