using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public Transform spawnPoint; 

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true) 
        {
            float interval = Random.Range(10f, 20f);
            yield return new WaitForSeconds(interval); 

            int enemiesToSpawn = Random.Range(2, 5);

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
                yield return new WaitForSeconds(1f); 
            }

        }
    }
}