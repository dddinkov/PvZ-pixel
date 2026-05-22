using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    private float cooldown = 0.1f;
    private float lastPlayedTime;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        if (Time.time - lastPlayedTime > cooldown)
        {
            audioSource.PlayOneShot(audioSource.clip);
            lastPlayedTime = Time.time;
        }
    }
}
