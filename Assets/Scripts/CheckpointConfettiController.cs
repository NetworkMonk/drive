using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointConfettiController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

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
    }
}
