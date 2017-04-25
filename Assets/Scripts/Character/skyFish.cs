using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skyFish : MonoBehaviour
{

    public GameObject tutorial;
    AudioSource fishSource;
    Rigidbody2D fishBody;
    public Vector2 jumpForce;
    bool queueJump;
    Vector3 jumpRotation = new Vector3(0, 0, 45);
    bool start;

    void Start()
    {
        fishBody = GetComponent<Rigidbody2D>();
        fishSource = GetComponent<AudioSource>();
        StartCoroutine(pauseStart());
    }

    IEnumerator pauseStart()
    {
        while (!(Input.GetKeyDown(KeyCode.Space)))
        {
            yield return null;
        }
        start = true;
        yield return null;
        tutorial.SetActive(false);
        fishBody.gravityScale = 1.5f;
        queueJump = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Death")
        {
            Application.LoadLevel(5);
        }
    }

    void FixedUpdate()
    {
        if (queueJump)
        {
            fishSource.Play();
            fishBody.velocity = Vector2.zero;
            fishBody.AddForce(jumpForce, ForceMode2D.Impulse);
            queueJump = false;
            transform.eulerAngles = jumpRotation;
        }
    }

    void Update()
    {
        if (start)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                queueJump = true;
            }
            if (!queueJump && transform.eulerAngles.z > -44)
            {
                Vector3 tempRotation = transform.eulerAngles;
                tempRotation.z -= 1;
                transform.eulerAngles = tempRotation;
            }
        }
        else
        {
            fishBody.gravityScale = 0;
            fishBody.velocity = Vector2.zero;
        }

    }
}
