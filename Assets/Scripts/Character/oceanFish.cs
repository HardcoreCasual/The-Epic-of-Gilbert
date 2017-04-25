using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class oceanFish : MonoBehaviour
{
    public GameObject tutorial;
    public float moveSpeed, rotationSpeed, yMin, yMax;

    Vector3 positionVector;
    float zRotation;
    bool start = false;

    void Start()
    {
        positionVector = transform.position;
        tutorial.SetActive(true);
        StartCoroutine(pauseStart());
    }

    IEnumerator pauseStart()
    {
        while (!(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)))
        {
            yield return null;
        }
        start = true;
        tutorial.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Death")
        {
            SceneManager.LoadScene(4);
        }
    }

    void Update()
    {
        if (start)
        {
            if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)))
            {
                if (zRotation < 0 && zRotation + Time.deltaTime * rotationSpeed < 0)
                {
                    zRotation += Time.deltaTime * rotationSpeed;
                    positionVector.y -= Time.deltaTime * moveSpeed * (0.01f * zRotation);
                }
                else if (zRotation > 0 && zRotation - Time.deltaTime * rotationSpeed > 0)
                {
                    zRotation -= Time.deltaTime * rotationSpeed;
                    positionVector.y -= Time.deltaTime * moveSpeed * (0.01f * zRotation);
                }
                else
                    zRotation = 0;
            }
            else
            {
                if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                {
                    if (transform.position.y < yMax && (transform.position.y + (Time.deltaTime * moveSpeed) < yMax))
                    {

                        if (zRotation > -45 && zRotation - Time.deltaTime * rotationSpeed > -45)
                            zRotation -= Time.deltaTime * rotationSpeed;
                        else
                            zRotation = -45;

                        positionVector.y -= Time.deltaTime * moveSpeed * (0.01f * zRotation);
                    }
                    else
                    {
                        if (zRotation < 0 && zRotation + Time.deltaTime * rotationSpeed < 0)
                        {
                            zRotation += Time.deltaTime * rotationSpeed;
                            positionVector.y -= Time.deltaTime * moveSpeed * (0.01f * zRotation);
                        }
                        else if (zRotation > 0 && zRotation - Time.deltaTime * rotationSpeed > 0)
                        {
                            zRotation -= Time.deltaTime * rotationSpeed;
                            positionVector.y -= Time.deltaTime * moveSpeed * (0.01f * zRotation);
                        }
                        else
                            zRotation = 0;

                    }
                }
                else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                {
                    if (transform.position.y > yMin && (transform.position.y - (Time.deltaTime * moveSpeed) > yMin))
                    {
                        if (zRotation < 45 && zRotation + Time.deltaTime * rotationSpeed < 45)
                            zRotation += Time.deltaTime * rotationSpeed;
                        else
                            zRotation = 45;

                        positionVector.y -= Time.deltaTime * moveSpeed * (0.01f * zRotation);
                    }
                    else
                    {
                        if (zRotation < 0 && zRotation + Time.deltaTime * rotationSpeed < 0)
                        {
                            zRotation += Time.deltaTime * rotationSpeed;
                            positionVector.y -= Time.deltaTime * moveSpeed * (0.01f * zRotation);
                        }
                        else if (zRotation > 0 && zRotation - Time.deltaTime * rotationSpeed > 0)
                        {
                            zRotation -= Time.deltaTime * rotationSpeed;
                            positionVector.y -= Time.deltaTime * moveSpeed * (0.01f * zRotation);
                        }
                        else
                            zRotation = 0;
                    }
                }
                else
                {
                    if (zRotation < 0 && zRotation + Time.deltaTime * rotationSpeed < 0)
                    {
                        zRotation += Time.deltaTime * rotationSpeed;
                        positionVector.y -= Time.deltaTime * moveSpeed * (0.01f * zRotation);
                    }
                    else if (zRotation > 0 && zRotation - Time.deltaTime * rotationSpeed > 0)
                    {
                        zRotation -= Time.deltaTime * rotationSpeed;
                        positionVector.y -= Time.deltaTime * moveSpeed * (0.01f * zRotation);
                    }
                    else
                        zRotation = 0;
                }
            }

            transform.eulerAngles = new Vector3(0, 180, zRotation);

            transform.position = positionVector;
        }
    }
}