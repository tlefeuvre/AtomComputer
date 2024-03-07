using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadinSFX : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;

    private void OnEnable()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
