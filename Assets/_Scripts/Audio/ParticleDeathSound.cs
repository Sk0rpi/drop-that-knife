using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class ParticleDeathSound : MonoBehaviour
{
    private int _numberOfParticles = 0;

    ParticleSystem particles;

    [SerializeField] EventReference sound;

    private void Awake()
    {
        particles = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        int count = particles.particleCount;
        if (count < _numberOfParticles)
        { //particle has died
            FMODUnity.RuntimeManager.PlayOneShot(sound);

        }
        _numberOfParticles = count;
    }
}
