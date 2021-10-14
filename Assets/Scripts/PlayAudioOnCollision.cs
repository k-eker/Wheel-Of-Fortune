using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnCollision : MonoBehaviour
{
    [SerializeField] private AudioClip m_AudioToPlay;
    private AudioSource m_AudioSource;
    private void Start()
    {
        m_AudioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        m_AudioSource.clip = m_AudioToPlay;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        m_AudioSource.Play();
    }
}
