using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float projectileLifetime = 5f;
    
    [SerializeField] private float baseFiringRate = .2f;

    [Header("AI")]
    [SerializeField] private bool useAI;

    [SerializeField] private float firingRateVariance = 0f;
    [SerializeField] private float minFiringRate = 0.1f;
    [HideInInspector] public bool isFiring;

    
    private Coroutine _fireCoroutine;
    
    void Start()
    {
        if (useAI)
        {
            isFiring = true;
        }
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && _fireCoroutine == null)
        { 
           _fireCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && _fireCoroutine != null)
        {
            StopCoroutine(_fireCoroutine);
            _fireCoroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject instance = Instantiate(
                projectilePrefab, 
                transform.position,
                Quaternion.identity
            );

            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = transform.up * projectileLifetime;
            }

            Destroy(instance , projectileLifetime);

            float timeToNextProjectile = Random.Range(baseFiringRate - firingRateVariance,
                baseFiringRate + firingRateVariance);

            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minFiringRate, float.MaxValue);
            yield return new WaitForSeconds(timeToNextProjectile);
        }
    }
}
