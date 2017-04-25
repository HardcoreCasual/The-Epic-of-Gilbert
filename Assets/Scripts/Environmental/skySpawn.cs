using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skySpawn : MonoBehaviour
{

    public GameObject Cloud, pillarUp, pillarDown;

    void Start()
    {
        StartCoroutine(pauseStart());
    }

    IEnumerator pauseStart()
    {
        while (!(Input.GetKeyDown(KeyCode.Space)))
        {
            yield return null;
        }
        StartCoroutine(spawnEnemy());
    }

    IEnumerator spawnEnemy()
    {

        int pickSpawn = Random.Range(0, 2);
        float spawnY = 0;

        if (pickSpawn == 0)
        {
            //Pillar

            bool up = Random.value > 0.5f;

            if (up)
            {
                spawnY = Random.Range(-5f, -2f);
                Instantiate(pillarUp, new Vector3(10, spawnY, 0), Quaternion.identity);
            }
            else
            {
                spawnY = Random.Range(1.5f, 5);
                Instantiate(pillarDown, new Vector3(10, spawnY, 0), Quaternion.identity);
            }

        }
        else if (pickSpawn == 1)
        {
            //Cloud

            int numClouds = Random.Range(1, 4);

            if(numClouds == 1)
            {
                spawnY = Random.Range(-3.15f, 3.15f);
                Instantiate(Cloud, new Vector3(10, spawnY, 0), Quaternion.identity);
            }
            else if (numClouds == 2)
            {
                spawnY = Random.Range(-3.15f, -1.25f);
                Instantiate(Cloud, new Vector3(10, spawnY, 0), Quaternion.identity);

                spawnY = Random.Range(1.25f, 3.15f);
                Instantiate(Cloud, new Vector3(10, spawnY, 0), Quaternion.identity);
            }
            else
            {
                spawnY = Random.Range(-3.15f, -2.15f);
                Instantiate(Cloud, new Vector3(10, spawnY, 0), Quaternion.identity);

                spawnY = Random.Range(-1, 1);
                Instantiate(Cloud, new Vector3(10, spawnY, 0), Quaternion.identity);

                spawnY = Random.Range(2.15f, 3.15f);
                Instantiate(Cloud, new Vector3(10, spawnY, 0), Quaternion.identity);
            }
        }

        yield return new WaitForSeconds(1.25f);
        StartCoroutine(spawnEnemy());
    }
}
