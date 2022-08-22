using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")] 
    [SerializeField] private AudioClip shootingClip;
    [SerializeField] [Range(0f,1f)] private float shootingVolume = 1f;

    [Header("Explosion")] 
    [SerializeField] private AudioClip explosionClip;
    [SerializeField] [Range(0f,1f)] private float explosionVolume = 1f;

    
    [Header("Damage")] 
    [SerializeField] private AudioClip damageClip;
    [SerializeField] [Range(0f,1f)] private float damageVolume = 1f;
    
    
    public void PlayShootingClip()
    {   
        playClip(shootingClip , shootingVolume);
    }

    public void PlayExplosionClip()
    {
        playClip(explosionClip , explosionVolume);
    }

    public void PlayDamageClip()
    {
        playClip(damageClip , damageVolume);
    }

    void playClip(AudioClip clip , float volume)
    {
        if (clip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip , cameraPos , volume);
        }
    }
}
