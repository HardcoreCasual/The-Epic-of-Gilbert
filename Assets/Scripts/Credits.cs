using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour {

    void Start()
    {
        StartCoroutine(restartGame());
    }

    IEnumerator restartGame()
    {
        yield return new WaitForSeconds(25);
        SceneManager.LoadScene(0);
    }

	void FixedUpdate () {
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.02f, 0);
	}
}
