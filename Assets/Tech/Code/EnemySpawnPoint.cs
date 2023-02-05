using UnityEngine;
using System.Collections.Generic;

public class EnemySpawnPoint : MonoBehaviour
{
    public int wave_size = 10;
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

    public void SpawnWave(GameObject EnemyPrefab, Base BaseBuilding)
    {
        activeWave.Clear();

        for(int i=0; i<wave_size; i++)
        {
            Enemy tmp = Instantiate(EnemyPrefab, transform.position, Quaternion.identity).GetComponent<Enemy>();
            tmp.Target = BaseBuilding;

            activeWave.Add(tmp);
        }
    }
}
