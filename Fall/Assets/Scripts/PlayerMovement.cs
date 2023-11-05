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

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float maxSpeed = 7f;
    private float holdTime = 0f;
    private float initialSpeed;
    private float lastDir = 0f;
    private float duration = 1f;
    private float reverseFactor = 1f;

    // Start is called before the first frame update
    private void Start()
    {
        initialSpeed = moveSpeed;
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        reverseFactor = 1f;
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            if (rb.velocity.x > 0f)
            {
                lastDir = 1f;
            }
            else if (rb.velocity.x < 0f)
            {
                lastDir = -1f;
            }
            else
            {
                lastDir = 0;
            }
            holdTime = 0f;
        }
        else if (Input.GetKey(KeyCode.A) && rb.velocity.x > 0f)
        {
            reverseFactor = 0.5f;
            
        }
        else if (Input.GetKey(KeyCode.D) && rb.velocity.x < 0f)
        {
            reverseFactor = 0.5f;
        }
        else if (Input.GetKey(KeyCode.A) && holdTime <= 0f)
        {
            holdTime -= Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.D) && holdTime >= 0f)
        {
            holdTime += Time.deltaTime;
        }
        else
        {
            if(holdTime < 0f)
            {
                lastDir = -1f;
            }
            else if(holdTime > 0f)
            {
                lastDir = 1f;
            }
            /*else
            {
                lastDir = 0f;
            }*/
            holdTime = 0f;
        }

        if(holdTime != 0f)
        {
            moveSpeed = Mathf.Abs(holdTime);
        }
        
        if (moveSpeed > maxSpeed)
        {
            moveSpeed = maxSpeed;
        }

        dirX = Input.GetAxisRaw("Horizontal");
        if(holdTime == 0f && moveSpeed > 1.00001f)
        {
            dirX = lastDir;
            //Debug.Log("movespeed before " + moveSpeed);
            duration = 1f / (reverseFactor * moveSpeed * 100f);
            moveSpeed = Mathf.Lerp(moveSpeed, 1f, duration);
            //Debug.Log("movespeed after " + moveSpeed);
        }
        //Debug.Log("dirx: " + dirX);
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        //Debug.Log(holdTime);
        //Debug.Log(moveSpeed);
    }
}
