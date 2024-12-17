using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperBouncy : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            // Convert velocity from world space to local space
            Vector3 localVelocity = transform.InverseTransformDirection(collision.rigidbody.velocity);

            // Set y component to 0
            localVelocity.y = 0;

            // Convert velocity from local space back to world space
            Vector3 worldVelocity = transform.TransformDirection(localVelocity);

            collision.gameObject.GetComponent<Rigidbody>().AddForce(worldVelocity.normalized * 2f, ForceMode.Impulse);
        }
    }
}
