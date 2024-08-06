using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
[Header("KnightsSpawn Var")]
public GameObject knightPrefab;
public Transform knightSpawnPosition;
public GameObject dangerZone;
private float repeatCycle = 1f;
 //private float spawnDelay = 10f;

private void OnTriggerEnter(Collider other)
{
    if (other.gameObject.tag == "Player")
    {
        InvokeRepeating("EnemySpawner", 1f, repeatCycle);
        Destroy(gameObject, 1f);
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }
    // {
    //         Invoke("EnemySpawner", spawnDelay);
    //         Destroy(gameObject, 1f);
    //         gameObject.GetComponent<BoxCollider>().enabled = false;
    //     }
}

// void EnemySpawner()
// {
//     Instantiate(knightPrefab, knightSpawnPosition.position, knightSpawnPosition.rotation);
   
// }
void EnemySpawner()
{
    Vector3 spawnPos = knightSpawnPosition.position;
    spawnPos.x += Random.Range(-1.0f, 1.0f); // Random offset in the X direction
    spawnPos.z += Random.Range(-1.0f, 1.0f); // Random offset in the Z direction

    Instantiate(knightPrefab, spawnPos, knightSpawnPosition.rotation);
}

// {
//         Vector3 spawnPos = knightSpawnPosition.position;
//         spawnPos.x += Random.Range(-1.0f, 1.0f); // Random offset in the X direction
//         spawnPos.z += Random.Range(-1.0f, 1.0f); // Random offset in the Z direction

//         Instantiate(knightPrefab, spawnPos, knightSpawnPosition.rotation);
//     }


}
