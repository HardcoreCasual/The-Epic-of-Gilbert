using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour {



    void Start ()
    {
        //StartCoroutine(killThis());
	}
	

    IEnumerator killThis()
    {
        yield return new WaitForSeconds(10);
        Destroy(this.gameObject);
    }
	void Update ()
    {
        transform.Translate(Vector3.left * Time.deltaTime * 5);
	}
}
