using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//to play sounds
public class SoundPlayer : MonoBehaviour
{
    AudioSource m_AudioSource;
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_AudioSource.Play();
    }
}
