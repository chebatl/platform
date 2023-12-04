using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{

    private AudioSource _audioSource;
    public AudioClip coinSound;
    public AudioClip attackSound;
    public AudioClip jumpSound;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySFX(AudioClip sfx){
        _audioSource.PlayOneShot(sfx);
    }
}
