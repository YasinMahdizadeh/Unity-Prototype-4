using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    private float spawnRange = 9.0f;

    public int enemiesCount;
    private int enemiesWave = 1;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy(enemiesWave);
        Instantiate(powerupPrefab, GetSpawnPosition(), powerupPrefab.transform.rotation);
        InvokeRepeating("PowerupDrown", 3, 7);
    }

    // Update is called once per frame
    void Update()
    {
        enemiesCount = FindObjectsOfType<Enemy>().Length;
        if ( enemiesCount == 0 )
        {
            enemiesWave++;
            SpawnEnemy(enemiesWave);
        }
    }

    Vector3 GetSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }

    void SpawnEnemy( int enemiesToSpawn )
    {
        for ( int i=0; i< enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GetSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    void PowerupDrown()
    { 
        Instantiate(powerupPrefab, GetSpawnPosition(), powerupPrefab.transform.rotation);
    }
}
