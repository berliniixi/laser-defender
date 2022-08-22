using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<WavesConfigSO> waveConfigs;
    [SerializeField] private float timeBetweenWaves = 0f;

    [SerializeField] private bool isLooping = true;
    
    private WavesConfigSO currentWave;
    
    void Start()
    {
        StartCoroutine(SpawnEnemyWave());
    }

    public WavesConfigSO GetCurrentWave()
    {
        return currentWave;
    }
    
    IEnumerator SpawnEnemyWave()
    {
        do
        {
            foreach (WavesConfigSO wave in waveConfigs)
            {
                currentWave = wave;

                for (int i = 0; i < currentWave.GetEnemyCount(); i++)
                {
                    Instantiate(currentWave.GetEnemyPrefab(i),
                        currentWave.GetStartingWaypoint().position,
                        Quaternion.Euler(0,0,180),
                        transform);

                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }

                yield return new WaitForSeconds(timeBetweenWaves);
            }
        } while (isLooping);
    }
}
