using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spaceFish : MonoBehaviour {

    public GameObject tutorial;
    AudioSource fishSource;
    Animator fishAnim;
    SpriteRenderer fishRenderer;

	void Start ()
    {
        //fishAnim = GetComponent<Animator>();
        fishRenderer = GetComponent<SpriteRenderer>();
        fishSource = GetComponent<AudioSource>();
        StartCoroutine(pauseStart());
	}

    IEnumerator pauseStart()
    {
        while (!(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)))
        {
            yield return null;
        }
        tutorial.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        other.SendMessage("Die");
        fishSource.Play();
    }

	void Update ()
    {
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)))
        {
            //fishAnim.SetFloat("speed", 0);
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                transform.RotateAround(Vector3.zero, Vector3.forward, Time.deltaTime * 200);
                //fishAnim.SetFloat("speed", 1);

                fishRenderer.flipX = false;
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                transform.RotateAround(Vector3.zero, Vector3.back, Time.deltaTime * 200);
                //fishAnim.SetFloat("speed", 1);

                fishRenderer.flipX = true;
            }
            else
            {
                //fishAnim.SetFloat("speed", 0);
            }
        }
	}
}
