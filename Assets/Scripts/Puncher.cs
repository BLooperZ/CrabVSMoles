using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puncher : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Punch()
    {
        Debug.Log("Punch");
        animator.SetTrigger("Punch");
    }
}
