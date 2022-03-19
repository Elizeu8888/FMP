using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : MonoBehaviour
{
    public Rigidbody rb;
    public Transform cam;
    public float speed = 6f;
    public float jumpheight = 8f;
    float turnsmoothing = 0.1f;
    float turnsmoothvelocity = 0.5f;
    public float maxVelocity = 20f;

    public Transform groundcheck;
    public float grounddistance = 0.4f;
    public LayerMask groundmask;
    public bool isgrounded;

    Vector3 direction;

    void Start()
    {
        
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");// uses imput to find direction
        direction = new Vector3(horizontal, 0f, vertical).normalized;

        Walk();
    }

    void FixedUpdate()
    {
        isgrounded = Physics.CheckSphere(groundcheck.position, grounddistance, groundmask);
        Jump();
    }

    void Walk()
    {
        if (direction.magnitude >= 0.1f)
        {


            float targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;// finds direction of movement





         
            
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnsmoothvelocity, turnsmoothing);// makes it so the player faces its movement direction
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            







            Vector3 movedir = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward;// here is the movement
            rb.AddForce(movedir.normalized * speed * Time.deltaTime, ForceMode.Impulse);
        }
        else
        {
            if (isgrounded == true)
            {
                Vector3 resultVelocity = rb.velocity;
                resultVelocity.z = 0;
                resultVelocity.x = 0;
                rb.velocity = resultVelocity;
            }


        }
        if (rb.velocity.sqrMagnitude > maxVelocity)// right alt and shift for||||
        {
            Vector3 endVelocity = rb.velocity;
            endVelocity.z *= 0.9f;
            endVelocity.x *= 0.9f;
            rb.velocity = endVelocity;

        }
    }
    void Jump()
    {
        if (Input.GetKey("space") && isgrounded)
        {
            rb.AddForce(transform.up.normalized * jumpheight, ForceMode.Impulse);// here u jump
            isgrounded = false;
        }
    }
}
