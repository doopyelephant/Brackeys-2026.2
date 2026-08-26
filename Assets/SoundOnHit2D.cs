using System;
using System.Collections;
using UnityEngine;

public class SoundOnHit2D : MonoBehaviour
{
    private AudioSource audioSource;

    private bool CanHit = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public IEnumerator OnCollisionEnter2D(Collision2D other)
    {
        if (CanHit)
        {
            CanHit = false;
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
            CanHit = true;
        }
    }
}
