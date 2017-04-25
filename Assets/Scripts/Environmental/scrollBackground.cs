using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollBackground : MonoBehaviour {

    Renderer rend;

	void Start ()
    {
        rend = GetComponent<Renderer>();	
	}
	
	void Update ()
    {
        float offset = Time.time * 0.1f;
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
	}
}
