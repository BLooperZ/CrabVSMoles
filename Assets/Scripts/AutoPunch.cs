using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPunch : MonoBehaviour
{
    public GameObject puncher;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Update is called once per frame
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            Debug.Log("Punching the ball!");
            puncher.GetComponent<Puncher>().Punch();
        }
    }
}
