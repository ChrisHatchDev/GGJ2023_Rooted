using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public int wave_size = 10;
    public double spawn_delay_s = 5.0;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }


    void SpawnWave(){
        for(int i=0; i<wave_size; i++){
            Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
