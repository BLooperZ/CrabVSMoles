using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestrictY : MonoBehaviour
{
    bool isCaught = true;

    public float maxSpeed = 7f;
    public float speed = 5f;
    public float minSpeed = 5f;

    public GameObject sphere;
    
    public Transform ballSpawn;
    // Start is called before the first frame update
    void Start()
    {
        // GetComponent<Rigidbody>().velocity = Vector3.right * -5f;
        GetComponent<MeshRenderer>().enabled = false;
        CatchBall();
    }

    public void CatchBall()
    {
        Debug.Log("Caught the ball!");
        isCaught = true;
        GetComponent<MeshRenderer>().enabled = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "SuperBouncy")
        {
            Debug.Log("SouperBouncy");
            GetComponent<AudioSource>().Play();
            GetComponent<Rigidbody>().velocity += 3f * (Vector3.zero - transform.position).normalized;
            if (!isCaught)
            {
                speed += 2f;
            }
        } else if (collision.gameObject.tag == "Mole")
        {
            speed += 0.5f;
        }
        speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
        if (!isCaught) {
            sphere.SetActive(true);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        speed = Mathf.Clamp(speed - 5f * Time.deltaTime, minSpeed, maxSpeed);
        if (isCaught)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            if (!Input.GetMouseButtonUp(0) && !Input.GetMouseButtonUp(1))
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                transform.position = new Vector3(ballSpawn.position.x, transform.position.y, ballSpawn.position.z);
                return;
            }
            isCaught = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero - transform.position;
        }
        Vector3 velocity = GetComponent<Rigidbody>().velocity.normalized * minSpeed;
        if (velocity.x < 0) {
            velocity.x = Mathf.Min(velocity.x, -2f);
        } else {
            velocity.x = Mathf.Max(velocity.x, 2f);
        }
        GetComponent<Rigidbody>().velocity = velocity.normalized * speed;
        sphere.transform.localScale = new Vector3(speed / minSpeed, speed / minSpeed, speed / minSpeed);
        if (speed <= minSpeed) {
            sphere.SetActive(false);
        }
        // Rigidbody rigidbody = GetComponent<Rigidbody>();
        // // Convert velocity from world space to local space
        // Vector3 localVelocity = transform.InverseTransformDirection(rigidbody.velocity);

        // // Set y component to 0
        // localVelocity.y = 0;

        // // Convert velocity from local space back to world space
        // rigidbody.velocity = transform.TransformDirection(localVelocity);
    }

    // void OnCollisionEnter(Collision collision)
    // {
    //     if (collision.gameObject.tag != "Playground")
    //     {
    //         Rigidbody rigidbody = GetComponent<Rigidbody>();
    //         // Convert velocity from world space to local space
    //         Vector3 localVelocity = transform.InverseTransformDirection(rigidbody.velocity);

    //         // keep the magnitude of the velocity
    //         float magnitude = rigidbody.velocity.magnitude;

    //         // Set y component to 0
    //         localVelocity.y = 0;
    //         localVelocity = localVelocity.normalized * magnitude;

    //         // Convert velocity from local space back to world space
    //         rigidbody.velocity = transform.TransformDirection(localVelocity);

    //         // if (collision.gameObject.tag == "SuperBouncy")
    //         // {
    //         //     rigidbody.AddForce(rigidbody.velocity * 2f, ForceMode.Impulse);
    //         // }
    //     }
    // }
}
