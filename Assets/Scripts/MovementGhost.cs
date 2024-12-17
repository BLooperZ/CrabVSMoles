using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovementGhost : MonoBehaviour
{

    public GameObject gizmo;
    public float angle = 0f;

    public ControlScheme controlScheme = ControlScheme.MouseLinear;

    public Animator hatAnimator;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

        Vector2 cur1 = 20f * new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        Vector2 cur2 = 20f * new Vector2(Mathf.Cos((angle + 120) * Mathf.Deg2Rad), Mathf.Sin((angle + 120) * Mathf.Deg2Rad));
        Vector2 cur3 = 20f * new Vector2(Mathf.Cos((angle - 120) * Mathf.Deg2Rad), Mathf.Sin((angle - 120) * Mathf.Deg2Rad));
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (controlScheme == ControlScheme.MouseLinear)
        {
            float factor = 2f;
            float timedFactor = factor * 100f * Time.unscaledDeltaTime;
            transform.Rotate(Vector3.down, (mouseX + mouseY) * timedFactor);
        }
        else if (controlScheme == ControlScheme.MouseCircular)
        {
            float factor = 1f;
            float timedFactor = factor * 100f * Time.unscaledDeltaTime;
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
            transform.Rotate(Vector3.down * horizontalInput * 100f * Time.unscaledDeltaTime);
        }

    }
}
