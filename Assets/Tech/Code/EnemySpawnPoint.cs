using UnityEngine;
using System.Collections.Generic;

public class EnemySpawnPoint : MonoBehaviour
{
    public List<Enemy> activeWave = new List<Enemy>();

    public bool WaveHasDied ()
    {
        bool waveIsDead = true;

        foreach (var item in activeWave)
        {
            if (item != null)
                waveIsDead = false;
        }

        return waveIsDead;
    }

    public void SpawnWave(GameObject EnemyPrefab, Base BaseBuilding, Vector2 waveSize)
    {
        if (WaveHasDied() == true)
        {
            activeWave.Clear();
        }

        for(int i=0; i < Random.Range(waveSize.x, waveSize.y); i++)
        {
            Enemy newEnemy = Instantiate(EnemyPrefab, transform.position, Quaternion.identity).GetComponent<Enemy>();
            newEnemy.Target = BaseBuilding;

            activeWave.Add(newEnemy);
        }
    }
}
