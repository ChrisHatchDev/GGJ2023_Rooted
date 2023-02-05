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

    public int CurrentWave = 0;
    public static int NumberOfEnemiesKilled;

    public TMPro.TMP_Text WaveText;
    public TMPro.TMP_Text NumberOfEnemiesText;

    void Start()
    {
        EnemySpawner.Instance = this;
        NumberOfEnemiesKilled = 0;

        InvokeRepeating("SpawnAtRandomPoint", 2.0f, (float)spawn_delay_s);
    }

    private void Update()
    {
        NumberOfEnemiesText.text = $"Enemies Killed: {NumberOfEnemiesKilled}";
    }

    void SpawnAtRandomPoint()
    {
        if (waitingForWaveToDie)
        {
            return;
        }

        CurrentWave += 1;
        WaveText.text = $"Wave: {CurrentWave}";

        currentSpawnPoint = ListOfSpawnPoints[Random.Range(0, ListOfSpawnPoints.Count)];
        currentSpawnPoint.SpawnWave(EnemyPrefab, BaseBuilding);

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
