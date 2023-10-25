using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumption : MonoBehaviour
{
    [SerializeField] int _heal = 100;
    [SerializeField] AudioClip _audioClip;
    [SerializeField] AudioSource _audioSource;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerHealth playerHealth = collision.transform.root.GetComponent<PlayerHealth>();
            playerHealth.Heal(_heal);
            _audioSource.PlayOneShot(_audioClip);
            Destroy(gameObject);
        }
    }
}
