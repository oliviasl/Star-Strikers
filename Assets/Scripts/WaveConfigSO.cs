using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private Transform pathPrefab;
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float timeBetweenEnemySpawns = 1.0f;
    [SerializeField] private float spawnTimeVariance = 0.5f;
    [SerializeField] private float minimumSpawnTime = 0.2f;

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public Transform GetStartingWaypoint()
    {
        return pathPrefab.GetChild(0);
    }

    public List<Transform> GetWaypoints()
    {
        List<Transform> waypoints = new List<Transform>();
        
        // we use notation "Transform in transform" because
        // game objects are technically children of transforms
        // not other game objects themselves
        // (hierarchies are built off transforms)
        foreach (Transform child in pathPrefab)
        {
            waypoints.Add(child);
        }
        return waypoints;
    }

    public int GetEnemyCount()
    {
        return enemyPrefabs.Count;
    }

    public GameObject GetEnemyPrefab(int index)
    {
        return enemyPrefabs[index];
    }

    public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(timeBetweenEnemySpawns - spawnTimeVariance,
            timeBetweenEnemySpawns + spawnTimeVariance);
        return Mathf.Max(spawnTime, minimumSpawnTime);
    }
}
