using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class food : MonoBehaviour {

    public float offset;

    Rigidbody2D foodBody;

    bool doSine;

	void Start ()
    {
        foodBody = GetComponent<Rigidbody2D>();
	}
	
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Water")
        {
            print("Collided with water");
            foodBody.gravityScale = 0.1f;
            foodBody.velocity = Vector2.zero;
            StartCoroutine(stopFood());
        }
        if (other.gameObject.tag == "Player")
        {
            print("ayylmao");
            other.SendMessage("Ate");
            Destroy(this.gameObject);
        }
    }

    IEnumerator stopFood()
    {
        yield return new WaitForSeconds(2);
        foodBody.gravityScale = 0;
        foodBody.velocity = Vector2.zero;
        doSine = true;
    }

	void Update ()
    {
		if(doSine)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + 0.002f * Mathf.Sin(1 * Time.time) * offset);
        }
	}
}
