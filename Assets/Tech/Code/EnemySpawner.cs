using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    public double spawn_delay_s = 5.0;
    public List<EnemySpawnPoint> ListOfSpawnPoints;
    public GameObject EnemyPrefab;
    public Base BaseBuilding;

    private EnemySpawnPoint currentSpawnPoint;
    private bool waitingForWaveToDie = false;

    public static int CurrentWave = 0;
    public static int NumberOfEnemiesKilled;

    void Start()
    {
        EnemySpawner.Instance = this;
        NumberOfEnemiesKilled = 0;

        InvokeRepeating("SpawnAtRandomPoint", 2.0f, (float)spawn_delay_s);
    }

    void SpawnAtRandomPoint()
    {
        if (waitingForWaveToDie || MainUI.GameIsActive == false)
        {
            return;
        }

        CurrentWave += 1;

        // X, Lowbound, Y highbound, Z number of locations to spawn from
        Vector3 waveData = Vector3.zero;

        switch (CurrentWave)
        {
            case 1 or 2 or 3:
                waveData = new Vector3(8, 15, 1);
            break;

            case 4 or 5 or 6 or 7:
                waveData = new Vector3(8, 10, 2);
            break;

            case 8 or 9 or 10 or 11:
                waveData = new Vector3(10, 15, 2);
            break;

            case 12 or 13 or 14 or 15:
                waveData = new Vector3(8, 14, 3);
            break;

            case >= 15:
                waveData = new Vector3(10, 30, 4);
            break;
            
            default:
                // waveData = new Vector3(8, 15, 1);
            break;
        }

        // waveData = new Vector3(8, 15, 3);
        // Debug.Log($"Wave data z {waveData.z}");

        List<int> usedSpawnedPoints = new List<int>();

        for (int i = 0; i < waveData.z; i++)
        {
            int targetSpawnPoint = Random.Range(0, ListOfSpawnPoints.Count);

            if (usedSpawnedPoints.Contains(i))
            {
                targetSpawnPoint = Random.Range(0, ListOfSpawnPoints.Count);
            }

            usedSpawnedPoints.Add(targetSpawnPoint);

            currentSpawnPoint = ListOfSpawnPoints[targetSpawnPoint];
            currentSpawnPoint.SpawnWave(EnemyPrefab, BaseBuilding, waveData); 
        }

        StartCoroutine(WaitToSpawnNextWave());
    }

    IEnumerator WaitToSpawnNextWave()
    {
        waitingForWaveToDie = true;

        while(currentSpawnPoint.WaveHasDied() == false)
        {
            yield return new WaitForSeconds(0.5f);
        }

        waitingForWaveToDie = false;
    }
}
