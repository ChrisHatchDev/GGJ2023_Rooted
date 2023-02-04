using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public double spawn_delay_s = 5.0;
    public List<EnemySpawnPoint> ListOfSpawnPoints;
    public GameObject EnemyPrefab;
    public Base BaseBuilding;
    void Start()
    {
        InvokeRepeating("SpawnAtRandomPoint", 2.0f, (float)spawn_delay_s);
    }

    void SpawnAtRandomPoint(){
        ListOfSpawnPoints[Random.Range(0, ListOfSpawnPoints.Count)].SpawnWave(EnemyPrefab, BaseBuilding);
    }
}
