using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class Spawner : MonoBehaviour
{
    public GameObject mobPrefab;
    public Vague[] difVagues;
    public float inBetweenSpawnTime = 0.5f;
    private bool canSpawn = true;
    private int curentWave = 0;

    public Transform[] spawnPos;


    void FixedUpdate()
    {
        if (canSpawn) StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        canSpawn = false;
        int randint = Random.Range(0, 4);
        Vector2 pos = spawnPos[randint].position ;
        randint = Random.Range(0, difVagues[curentWave].enemies.Length);
        var newEnemy = Instantiate(mobPrefab, pos, Quaternion.identity);
        //newEnemy.GetComponent<Enemy>().Initialise(difVagues[curentWave].enemies[randint]);
        yield return new WaitForSeconds(difVagues[curentWave].spawnInterval);
        canSpawn = true;


    }

}
