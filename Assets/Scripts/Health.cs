using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 50;
    [SerializeField] private ParticleSystem hitEffect;


    [SerializeField] private bool applyCameraShake;
    private CameraShake _cameraShake;
    
    private AudioPlayer _audioPlayer;
    
    void Awake()
    {
        _cameraShake = Camera.main.GetComponent<CameraShake>();
        _audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
            //Take damage
            TakeDamage(damageDealer.GetDamage());
            //Tell damage dealer that it hit something
            PlayHitEffect();
            ShakeCamera();
            damageDealer.Hit();
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            _audioPlayer.PlayExplosionClip();
            Destroy(gameObject);
        }
    }

    void PlayHitEffect()
    {
        if (hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    void ShakeCamera()
    {
        if (_cameraShake != null && applyCameraShake)
        {
            _cameraShake.Play();
        }
    }
}
