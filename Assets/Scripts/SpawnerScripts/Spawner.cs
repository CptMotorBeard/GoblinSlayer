using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject EnemyToSpawn;

    public void SpawnEnemy(Transform parentTransform)
    {
        Instantiate(EnemyToSpawn, this.transform.position, Quaternion.identity, parentTransform);
    }
}
