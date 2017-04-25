using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class oceanSpawn : MonoBehaviour
{

    public List<GameObject> SpawnList;
    float timeBetween = 2;
    float lastY = 0;
    float lastSpawn = 11;
    void Start()
    {

        StartCoroutine(pauseStart());
    }

    IEnumerator pauseStart()
    {
        while (!(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)))
        {
            yield return null;
        }
        StartCoroutine(SpawnPrefab());
    }

    IEnumerator SpawnPrefab()
    {
        int r = Random.Range(0, SpawnList.Count);

        while (r == lastSpawn)
        {
            r = Random.Range(0, SpawnList.Count);
        }

        lastSpawn = r;

        float yMin = -1, yMax = 1;

        switch (r)
        {
            case 0:
                break;
            case 1:
                yMax = 3;
                timeBetween = 2;
                break;
            case 2:
                yMin = -0.5f;
                yMax = 0.5f;
                timeBetween = 1.6f;
                break;
            case 3:
                yMax = 2;
                timeBetween = 2f;
                break;
            case 4:
                yMin = -0.5f;
                yMax = 2;
                timeBetween = 2;
                break;
            case 5:
                yMin = -1.5f;
                yMax = 2.5f;
                timeBetween = 1;
                break;
            case 6:
                yMax = 2.5f;
                timeBetween = 1;
                break;
            case 7:
                yMax = 2;
                timeBetween = 2;
                break;
            case 8:
                yMin = -1.3f;
                yMax = 1.3f;
                timeBetween = 2;
                break;
            case 9:
                yMin = -2;
                yMax = 2;
                timeBetween = 1;
                break;
            case 10:
                yMin = -2;
                yMax = 2;
                timeBetween = 1;
                break;
        }
        if (yMin < lastY - 2)
            yMin = lastY - 2;
        if (yMax > lastY + 2)
            yMax = lastY + 2;

        float y = Random.Range(yMin, yMax);
        lastY = y;
        Instantiate(SpawnList[r], new Vector3(12, y, 0), Quaternion.identity);

        yield return new WaitForSeconds(timeBetween);
        StartCoroutine(SpawnPrefab());
    }
}