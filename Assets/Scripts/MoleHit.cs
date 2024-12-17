using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleHit : MonoBehaviour
{

    Animator animator;
    float hitTime = 0f;
    public int hitPoints = 3;
    public GameObject snout;

    public AudioSource audioSource;

    public AudioClip doneSound;

    public Material[] skins;

    public Material hitSkin;

    public GameObject eyesNormal;
    public GameObject eyesHit;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        ColorChange();
    }

    // Update is called once per frame
    void Update()
    {
        if (hitTime > 0)
        {
            animator.SetBool("isHit", true);
            hitTime -= Time.deltaTime;
            eyesNormal.SetActive(false);
            eyesHit.SetActive(true);
            if (hitTime <= 4.7f)
            {
                ColorChange();
            }
            return;
        }
        animator.SetBool("isHit", false);
        eyesNormal.SetActive(true);
        eyesHit.SetActive(false);
    }

    void ColorChange()
    {
        if (hitPoints == 3)
        {
            GetComponent<Renderer>().material = skins[0];
            snout.GetComponent<Renderer>().material = skins[1];
        }
        else if (hitPoints == 2)
        {
            GetComponent<Renderer>().material = skins[1];
            snout.GetComponent<Renderer>().material = skins[2];
        }
        else if (hitPoints == 1)
        {
            GetComponent<Renderer>().material = skins[2];
            snout.GetComponent<Renderer>().material = skins[1];
        }
        else if (hitPoints == 0)
        {
            Destroy(gameObject);
        }
    }

    void PlaySound()
    {
        // play sound
        if (hitPoints == 0)
        {
            audioSource.PlayOneShot(doneSound);
        }
        else
        {
            audioSource.Play();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball" && hitTime <= 0)
        {
            Debug.Log("Hit by Ball");
            hitTime = 5f;
            hitPoints--;
            GetComponent<Renderer>().material = hitSkin;
        }
        PlaySound();
        // ColorChange();
    }
}
