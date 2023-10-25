using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralPoint : MonoBehaviour
{
    [SerializeField] Transform _hipTransform;
    AudioSource _audioSource;
    [SerializeField] AudioClip[] _clips;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        transform.position = _hipTransform.position;
        if(transform.position.y < -1000)
        {
            Destroy(transform.root.gameObject);
        }
    }

    public void PlayDamagedSound()
    {
        _audioSource.PlayOneShot(_clips[0]);
    }

    public void PlayDeathSound()
    {
        _audioSource.PlayOneShot(_clips[1]);
    }
}

