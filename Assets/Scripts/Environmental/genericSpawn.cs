using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class genericSpawn : MonoBehaviour {

    public GameObject[] enemies;

    bool up;    //True if the last enemy spawned was high

    void Start()
    {
        int startBool = Random.Range(0, 2);
        if (startBool == 0)
            up = true;
        else
            up = false;

        StartCoroutine(spawnEnemy());
    }

    IEnumerator spawnEnemy()
    {
        if (up)
        {
            float spawnY = Random.Range(-3.75f, -1.75f);

            int enemyPick = Random.Range(0, enemies.Length);

            Instantiate(enemies[enemyPick], new Vector3(10, spawnY, 0), Quaternion.identity);

            up = false;
        }
        else
        {
            float spawnY = Random.Range(1.75f, 3.75f);

            int enemyPick = Random.Range(0, enemies.Length);

            Instantiate(enemies[enemyPick], new Vector3(10, spawnY, 0), Quaternion.identity);

            up = true;
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(spawnEnemy());
    }
}
