using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyHit : MonoBehaviour
{
    public GameObject controller;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            controller.GetComponent<CrabPunch>().Paralyze(2f);
        }
    }
}
