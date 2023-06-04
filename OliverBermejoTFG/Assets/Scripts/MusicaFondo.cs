using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicaFondo : MonoBehaviour
{
    public AudioSource audioSource; // Referencia al componente AudioSource
    public AudioClip musicaFondo; // Clip de audio de la música de fondo

    private void Start()
    {
        audioSource.clip = musicaFondo;

        audioSource.loop = true;
        audioSource.Play();
        ajustarVolumen(0.1f);
    }

    public void ajustarVolumen(float volume)
    {
        audioSource.volume = volume;
    }
}
