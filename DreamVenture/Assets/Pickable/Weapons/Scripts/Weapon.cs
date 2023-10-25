using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Weapon : MonoBehaviour
{
    [SerializeField] float _baseDamage;
    [SerializeField] float _lowestSpeed;
    [SerializeField] AudioSource _audioSource;
    Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject @object = collision.gameObject;
        if (@object.tag == "Enemy")
        {
            Health enemyHealth = collision.transform.root.GetComponent<Health>();
            float speed = _rb.velocity.magnitude;
            if (speed > _lowestSpeed)
            {
                float damage = _baseDamage * speed * _rb.mass;
                enemyHealth.TakeDamage((int)damage);
            }
        }
        else if (@object.tag == "Player")
        {
            PlayerHealth playerHealth = collision.transform.root.GetComponent<PlayerHealth>();
            float speed = _rb.velocity.magnitude;
            if (speed > _lowestSpeed)
            {
                float damage = _baseDamage * speed * _rb.mass;
                playerHealth.TakeDamage((int)damage);
            }
        }
        else if (@object.tag == "Weapon")
        {
            float speed = _rb.velocity.magnitude;
            if (speed > _lowestSpeed)
            {
                _audioSource.Play();
            }
        }
    }
}
