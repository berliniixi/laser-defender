using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    private EnemySpawner _enemySpawner;
    private WavesConfigSO _wavesConfigSo;
    private List<Transform> waypoints;
    private int waypointsIndex = 0;

    void Awake()
    {
        _enemySpawner = FindObjectOfType<EnemySpawner>();
    }
    
    void Start()
    {
        _wavesConfigSo = _enemySpawner.GetCurrentWave();
        waypoints = _wavesConfigSo.GetWaypoints();
        transform.position = waypoints[waypointsIndex].position;
    }

    void Update()
    {
        FollowPath();
    }

    void FollowPath()
    {
        if (waypointsIndex < waypoints.Count)
        {
            Vector3 targetPosition = waypoints[waypointsIndex].position;
            float delta = _wavesConfigSo.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            if (transform.position == targetPosition)
            {
                waypointsIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
