using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bowlFish : MonoBehaviour
{
    public SpeechBubble speech;
    public AudioClip enterDream, exitDream, eatClip;
    AudioSource fishSource;
    public int nextLevel;

    Rigidbody2D fishBody;
    Vector3 leftRotation = new Vector3(0, 0, 0), rightRotation = new Vector3(0, 180, 0);
    Vector2 inputVector = Vector3.zero;

    Animator fishAnim;
    Animation Swim, Idle;

    bool hasBounced = false, hasEaten, canMove = true;
    float bounceCooldown = 0;
    int foodCounter = 0;

    void Start()
    {
        fishBody = GetComponent<Rigidbody2D>();
        fishAnim = GetComponent<Animator>();
        fishSource = GetComponent<AudioSource>();

        if(nextLevel == 2 || nextLevel == 3)
        {
            fishSource.clip = exitDream;
            fishSource.Play();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //if (!hasBounced)
        //{
        if (other.gameObject.tag == "Left Collision")
        {
            if (inputVector.x < 0)
                inputVector.x = -inputVector.x;
        }
        else if (other.gameObject.tag == "Right Collision")
        {
            if (inputVector.x > 0)
                inputVector.x = -inputVector.x;

        }
        else if (other.gameObject.tag == "Top Collision")
        {
            inputVector.y = -inputVector.y;
            hasBounced = true;
            bounceCooldown = 0;
        }
        //}

    }

    void Ate()
    {
        foodCounter++;
        fishSource.clip = eatClip;
        fishSource.Play();
    }

    IEnumerator fallAsleep()
    {
        yield return new WaitForSeconds(3f);
        canMove = false;
        speech.startSetText(5, "Gilbert falls\nasleep. ", 0.1f);
        fishSource.clip = enterDream;
        fishSource.Play();
    }

    IEnumerator loadLevel()
    {
        yield return new WaitForSeconds(5f);
        Application.LoadLevel(nextLevel);
    }
    void Update()
    {

        if (foodCounter >= 3 & !hasEaten)
        {
            hasEaten = true;
            StartCoroutine(fallAsleep());
        }

        if (canMove)
        {
            if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)))
            {
                if (inputVector.y > 0)
                {
                    if (inputVector.y - Time.deltaTime > 0)
                        inputVector.y -= Time.deltaTime;
                    else
                        inputVector.y = 0;
                }
                else if (inputVector.y < 0)
                {
                    if (inputVector.y + Time.deltaTime < 0)
                        inputVector.y += Time.deltaTime;
                    else
                        inputVector.y = 0;
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                {
                    if (inputVector.y + Time.deltaTime < 1.5f)
                        inputVector.y += Time.deltaTime;
                    else
                        inputVector.y = 1.5f;
                }
                else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                {
                    if (inputVector.y - Time.deltaTime > -1.5f)
                        inputVector.y -= Time.deltaTime;
                    else
                        inputVector.y = -1.5f;
                }
                else
                {
                    if (inputVector.y > 0)
                    {
                        if (inputVector.y - Time.deltaTime > 0)
                            inputVector.y -= Time.deltaTime;
                        else
                            inputVector.y = 0;
                    }
                    else if (inputVector.y < 0)
                    {
                        if (inputVector.y + Time.deltaTime < 0)
                            inputVector.y += Time.deltaTime;
                        else
                            inputVector.y = 0;
                    }
                }
            }

            if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)))
            {
                if (inputVector.x > 0)
                {
                    if (inputVector.x - Time.deltaTime > 0)
                        inputVector.x -= Time.deltaTime;
                    else
                        inputVector.x = 0;
                }
                else if (inputVector.x < 0)
                {
                    if (inputVector.x + Time.deltaTime < 0)
                        inputVector.x += Time.deltaTime;
                    else
                        inputVector.x = 0;
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                {
                    if (inputVector.x - Time.deltaTime > -1.5f)
                        inputVector.x -= Time.deltaTime;
                    else
                        inputVector.x = -1.5f;
                }
                else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                {
                    if (inputVector.x + Time.deltaTime < 1.5f)
                        inputVector.x += Time.deltaTime;
                    else
                        inputVector.x = 1.5f;
                }
                else
                {
                    if (inputVector.x > 0)
                    {
                        if (inputVector.x - Time.deltaTime > 0)
                            inputVector.x -= Time.deltaTime;
                        else
                            inputVector.x = 0;
                    }
                    else if (inputVector.x < 0)
                    {
                        if (inputVector.x + Time.deltaTime < 0)
                            inputVector.x += Time.deltaTime;
                        else
                            inputVector.x = 0;
                    }
                }
            }

            if (inputVector.x > 0)
            {
                transform.eulerAngles = rightRotation;
            }
            else if (inputVector.x < 0)
            {
                transform.eulerAngles = leftRotation;
            }

            //if (inputVector == Vector3.zero)
            //{
            //    inputVector = fishBody.velocity * 0.9f;
            //}

            if (inputVector.x != 0)
            {
                fishAnim.SetFloat("speed", Mathf.Abs(inputVector.x));
            }
            else
            {
                fishAnim.SetFloat("speed", 0);
            }
            fishBody.velocity = inputVector;

            //if (hasBounced)
            //    bounceCooldown += Time.deltaTime;

            //if (bounceCooldown >= 0.5f)
            //    hasBounced = false;

            if (inputVector == Vector2.zero)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + 0.002f * Mathf.Sin(1 * Time.time));
            }
        }
        else
        {
            fishBody.velocity = Vector2.zero;
            fishAnim.SetFloat("speed", 0);
            fishAnim.SetBool("fallAsleep", true);
            StartCoroutine(loadLevel());
        }
    }
}
