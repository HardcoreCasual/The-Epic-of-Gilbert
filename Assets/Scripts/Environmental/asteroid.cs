using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroid : MonoBehaviour
{
    public float Speed;
    Vector3 rotationVector = new Vector3(0, 0, 1);
    void Start()
    {
        
    }

    void Die()
    {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Planet")
            Application.LoadLevel(6);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, Time.deltaTime * Speed);

        transform.Rotate(rotationVector * (0.5f * Speed));
    }
}
