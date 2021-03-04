using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Destructable
{
    [SerializeField] Spawnpoints[] spawnPoints;
    void SpawnAtMoveSpawnPoint()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length - 1);
        transform.position = spawnPoints[spawnIndex].transform.position;
        transform.rotation = spawnPoints[spawnIndex].transform.rotation;
    }

    public override void Die()
    {
        base.Die();
        SpawnAtMoveSpawnPoint();

    }

    
}
