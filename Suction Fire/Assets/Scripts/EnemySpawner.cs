using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public enum SpawnerType {SmallEnemySpawner, LargeEnemySpawner}
    public SpawnerType type;
    public GameObject smallEnemy, largeEnemy;

    public float smallSpawnDelay = 2f, largeSpawnDelay = 4f;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(type == SpawnerType.SmallEnemySpawner){
            if(Time.time >= startTime + smallSpawnDelay){
                SpawnSmallEnemy();
                startTime = Time.time;
            }
        }
        else if(type == SpawnerType.LargeEnemySpawner){
            if(Time.time >= startTime + largeSpawnDelay){
                SpawnLargeEnemy();
                startTime = Time.time;
            }
        }
    }

    void SpawnSmallEnemy(){
        Instantiate(smallEnemy, this.transform.position, Quaternion.identity);
    }

    void SpawnLargeEnemy(){
        Instantiate(largeEnemy, this.transform.position, Quaternion.identity);
    }
}
