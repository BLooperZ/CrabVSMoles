using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControlScheme
{
    MouseLinear,
    MouseCircular,
    Keyboard
}

public class Movement : MonoBehaviour
{


    public GameObject gizmo;
    public float angle = 0f;

    public ControlScheme controlScheme = ControlScheme.MouseLinear;

    public Animator hatAnimator;

    public GameObject ghost;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        ghost.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.K))
        {
            controlScheme = ControlScheme.Keyboard;
            ghost.GetComponent<MovementGhost>().controlScheme = controlScheme;
        } else if (Input.GetKeyDown(KeyCode.L))
        {
            controlScheme = ControlScheme.MouseLinear;
            ghost.GetComponent<MovementGhost>().controlScheme = controlScheme;
        } else if (Input.GetKeyDown(KeyCode.O))
        {
            controlScheme = ControlScheme.MouseCircular;
            ghost.GetComponent<MovementGhost>().controlScheme = controlScheme;
        } else if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
            if (Time.timeScale == 0)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                ghost.GetComponent<MovementGhost>().angle = angle;
                ghost.SetActive(true);
            } else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                ghost.SetActive(false);
            }
        }

        Vector2 cur1 = 20f * new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        Vector2 cur2 = 20f * new Vector2(Mathf.Cos((angle + 120) * Mathf.Deg2Rad), Mathf.Sin((angle + 120) * Mathf.Deg2Rad));
        Vector2 cur3 = 20f * new Vector2(Mathf.Cos((angle - 120) * Mathf.Deg2Rad), Mathf.Sin((angle - 120) * Mathf.Deg2Rad));
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (controlScheme == ControlScheme.MouseLinear)
        {
            float factor = 2f;
            float timedFactor = factor * 100f * Time.deltaTime;
            transform.Rotate(Vector3.down, (mouseX + mouseY) * timedFactor);
        }
        else if (controlScheme == ControlScheme.MouseCircular)
        {
            float factor = 1f;
            float timedFactor = factor * 100f * Time.deltaTime;
            Vector2 vec = new Vector2(mouseX, mouseY) * timedFactor;

            Vector2 total1 = cur1 + vec;
            // Vector2 total2 = cur2 + vec;
            // Vector2 total3 = cur3 + vec;

            // Vector2 total = (total1 + total2 + total3) / 3f;

            angle = Mathf.Atan2(total1.y, total1.x) * Mathf.Rad2Deg;

            if (Input.GetMouseButtonUp(0))
            {
                angle -= 120f;
                hatAnimator.SetTrigger("ChangeHat");
            }
            if (Input.GetMouseButtonUp(1))
            {
                angle += 120f;
                hatAnimator.SetTrigger("ChangeHat");
            }

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.down);
            gizmo.transform.position = new Vector3(-2f + 10f * -Mathf.Sin(angle * Mathf.Deg2Rad), 0.5f, 10f * Mathf.Cos(angle * Mathf.Deg2Rad));
        }
        else if (controlScheme == ControlScheme.Keyboard)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.down * horizontalInput * 100f * Time.deltaTime);
        }

    }
}
