using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeaWave : MonoBehaviour
{

    int ballIndex = 4;
    public GameObject[] balls;
    public GameObject player;
    public GameObject ball;
    public GameObject troop;
    public GameObject bigMole;
    public GameObject bigCrab;

    bool tryAgain = false;
    // // Start is called before the first frame update
    // void Start()
    // {
    //     balls = GameObject.FindGameObjectsWithTag("Ball");
    // }

    void Update()
    {
        GameObject[] moles = GameObject.FindGameObjectsWithTag("Mole");
        if (moles.Length == 0)
        {
            Debug.Log("No more moles");
            Destroy(ball);
            Destroy(troop);
            Destroy(player);
            bigCrab.SetActive(true);
            tryAgain = true;
        }
        if (Input.GetKeyUp(KeyCode.R) || (tryAgain && (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
        }
    }

    // Update is called once per frame
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            Debug.Log("Ball hit the sea wave");
            if (ballIndex >= 0)
            {
                // Vector3 ballPos = balls[ballIndex].transform.position;
                // other.transform.position = new Vector3(ballPos.x, other.transform.position.y, ballPos.z);
                // other.transform.position = new Vector3(ballSpawn.position.x, other.transform.position.y, ballSpawn.position.z);
                // // give the ball velocity towards 0,0,0
                // other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero - other.transform.position;
                Destroy(balls[ballIndex]);
                ballIndex--;
                other.gameObject.GetComponent<RestrictY>().CatchBall();
                GetComponent<AudioSource>().Play();
            } else {
                Debug.Log("No more balls to destroy");
                Destroy(player);
                Destroy(ball);
                bigMole.SetActive(true);
                tryAgain = true;
            }
        }
    }
}
