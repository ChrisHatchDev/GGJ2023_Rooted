using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public int wave_size = 10;
    public void SpawnWave(GameObject EnemyPrefab, Base BaseBuilding){
        for(int i=0; i<wave_size; i++){
            Enemy tmp = Instantiate(EnemyPrefab, transform.position, Quaternion.identity).GetComponent<Enemy>();
            tmp.Target = BaseBuilding;
        }
    }
}
