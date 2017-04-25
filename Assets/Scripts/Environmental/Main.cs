using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour {

	void Start () {
		
	}
	
    public void mainToOcean()
    {
        SceneManager.LoadScene(1);
    }

    public void mainToSky()
    {
        SceneManager.LoadScene(2);
    }

    public void mainToSpace()
    {
        SceneManager.LoadScene(3);
    }

	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}
}