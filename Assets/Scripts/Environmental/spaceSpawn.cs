using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spaceSpawn : MonoBehaviour {

    public GameObject[] asteroids;
    public float spawnMinimum;

    void Start ()
    {
        StartCoroutine(pauseStart());
    }

    IEnumerator pauseStart()
    {
        while (!(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)))
        {
            yield return null;
        }
        StartCoroutine(spawnAsteroid());

    }

    IEnumerator spawnAsteroid()
    {
        int enemyPick = Random.Range(0, asteroids.Length);

        Vector2 circlePosition = Random.insideUnitCircle;
        Vector2 spawnPosition = circlePosition.normalized * spawnMinimum;

        GameObject a = asteroids[enemyPick];
        a.GetComponent<asteroid>().Speed = Random.Range(1f, 2f);
        if (!(spawnPosition == Vector2.zero))
            Instantiate(a, spawnPosition, Quaternion.identity);

        yield return new WaitForSeconds(1);
        StartCoroutine(spawnAsteroid());
    }
}
