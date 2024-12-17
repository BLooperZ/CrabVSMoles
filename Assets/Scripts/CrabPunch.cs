using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabPunch : MonoBehaviour
{
    public GameObject rightClaw;
    public GameObject leftClaw;
    float paralyzed = 0f;

    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        bool isPaused = Time.timeScale == 0;
        if (paralyzed > 0)
        {
            paralyzed -= Time.deltaTime;
            return;
        }
        animator.SetBool("isParalyzed", false);
        // float horizontalInput = Input.GetAxis("Horizontal");
        // move the player in circular motion
        // transform.Rotate(Vector3.down * horizontalInput * 30f * Time.deltaTime);
        // transform.Translate(Vector3.forward * horizontalInput * 4f * Time.deltaTime);
        if (!isPaused && Input.GetMouseButtonUp(1))
        {
            Debug.Log("Punching Left Claw" + leftClaw.name);
            leftClaw.GetComponent<Puncher>().Punch();
        }
        if (!isPaused && Input.GetMouseButtonUp(0))
        {
            Debug.Log("Punching Right Claw" + rightClaw.name);
            rightClaw.GetComponent<Puncher>().Punch();
        }
    }

    public void Paralyze(float duration)
    {
        Debug.Log("Paralyzed");
        paralyzed = duration;
        animator.SetBool("isParalyzed", true);
    }
}
