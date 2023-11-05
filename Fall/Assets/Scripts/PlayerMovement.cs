using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;

    [SerializeField] private float maxSpeed = 7f;
    private float duration = .5f;


    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) && rb.velocity.x > 0f)  // pressing A and D while moving right
        {
            duration = Mathf.Max(duration - .001f, .5f);
            //Debug.Log("1");
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) && rb.velocity.x < 0f)  // pressing A and D while moving left
        {
            duration = Mathf.Min(duration + .001f, .5f);
            //Debug.Log("2");
        }
        else if (Input.GetKey(KeyCode.A) && rb.velocity.x > 0f)  // pressing A while moving right
        {
            duration = Mathf.Max(duration - .003f, 0f);
            //Debug.Log("3");
        }
        else if (Input.GetKey(KeyCode.A) && rb.velocity.x <= 0f)  // pressing A while moving left
        {
            duration = Mathf.Max(duration - .005f, 0f);
            //Debug.Log("4");
        }
        else if (Input.GetKey(KeyCode.D) && rb.velocity.x <= 0f)  // pressing D while moving left
        {
            duration = Mathf.Min(duration + .003f, 1f);
            //Debug.Log("5");
        }
        else if (Input.GetKey(KeyCode.D) && rb.velocity.x > 0f)  // pressing D while moving right
        {
            duration = Mathf.Min(duration + .005f, 1f);
            //Debug.Log("6");
        }
        else if (rb.velocity.x > 0f) // no input, moving right
        {
            duration = Mathf.Max(duration - .001f, .5f);
            //Debug.Log("7");
        }
        else if (rb.velocity.x < 0f) // no input, moving left
        {
            duration = Mathf.Min(duration + .001f, .5f);
            //Debug.Log("8");
        }
        duration = Mathf.Clamp01(duration);
        rb.velocity = new Vector2(Mathf.Lerp(-maxSpeed, maxSpeed, duration), rb.velocity.y);
        Debug.Log(rb.velocity.x);
    }
}
