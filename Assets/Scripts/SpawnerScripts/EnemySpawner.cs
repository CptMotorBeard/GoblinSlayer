using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    List<Spawner> spawners;
    List<Spawner> usedSpawners;
    PlayerStatus player;

    // Parameters
    public Transform SpawnParent;
    public float SpawnDelay = 2.0f;
    public float EnemiesToSpawn = 1;

    private void Start()
    {
        usedSpawners = new List<Spawner>();
        spawners     = new List<Spawner>(GetComponentsInChildren<Spawner>());

        player = PlayerStatus.instance;
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (player != null)
        {
            EnemiesToSpawn += (SpawnDelay * 0.1f);
            if (EnemiesToSpawn > spawners.Count) { EnemiesToSpawn = spawners.Count; }

            for (int i = 0; i < (int)EnemiesToSpawn; i++)
            {
                int s = Random.Range(0, spawners.Count);
                spawners[s].SpawnEnemy(SpawnParent);
                usedSpawners.Add(spawners[s]);
                spawners.RemoveAt(s);
            }

            spawners.AddRange(usedSpawners);
            usedSpawners.Clear();

            yield return new WaitForSeconds(SpawnDelay);
        }
    }
}
