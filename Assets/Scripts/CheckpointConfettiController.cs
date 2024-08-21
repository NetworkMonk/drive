using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointConfettiController : MonoBehaviour
{
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RestartAllParticleSystems()
    {
        ParticleSystem[] particleSystems = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem ps in particleSystems)
        {
            ps.Clear();
            ps.Play();
        }

        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
