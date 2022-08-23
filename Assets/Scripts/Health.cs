using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private bool isPlayer;
    
    [SerializeField] private int health = 50;

    [SerializeField] private int score = 50;
    
    [SerializeField] private ParticleSystem hitEffect;
    


    [SerializeField] private bool applyCameraShake;
    private CameraShake _cameraShake;
    
    private AudioPlayer _audioPlayer;

    private ScoreKeeper _scoreKeeper; //Challenge 
    private LevelManager _levelManager;

    
    void Awake()
    {
        _cameraShake = Camera.main.GetComponent<CameraShake>();
        _audioPlayer = FindObjectOfType<AudioPlayer>();
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
        _levelManager = FindObjectOfType<LevelManager>();
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer != null) // When Player hit by opponent
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
            Die();
            _audioPlayer.PlayExplosionClip();
        }
    }


    void Die()
    {
        if (!isPlayer)
        {
            _scoreKeeper.ModifyScore(score);
        }
        else
        {
            _levelManager.GameOver();
        }
        Destroy(gameObject);
    }
    
    
    public int GetHealth()
    {
        return health;  // Challenge
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
