using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Mummy mummyPrefab;

    [SerializeField] float nextSpawnTime = 1;
    [SerializeField] float spawnDelay = 12f;

    [SerializeField] Transform[] spawnPoints; 

    int spawnCount;

    void Update()
    {
        if(ReadyToSpawn())
            StartCoroutine(Spawn());
    }

    bool ReadyToSpawn() => Time.time >= nextSpawnTime;

    IEnumerator Spawn()
    {
        float delay = spawnDelay - spawnCount;
        delay = Mathf.Max(1, delay);
        nextSpawnTime = Time.time + delay;

        spawnCount++;

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        var mummy = Instantiate(mummyPrefab, spawnPoint.position, transform.rotation);

        yield return new WaitForSeconds(1);
    }

}
