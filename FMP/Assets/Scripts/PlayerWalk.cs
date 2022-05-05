using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : MonoBehaviour
{
    public bool attacking,specialattacking;
    public Rigidbody rb;
    public Transform cam, camAIM;

    public float speed = 6f, runSpeed, jumpheight = 8f;

    float turnsmoothing = 0.1f;
    float turnsmoothvelocity = 0.5f;

    public float maxVelocity = 20f, jumpVelocity = 100f;

    public Transform groundcheck;
    public float grounddistance = 0.4f;
    public LayerMask groundmask;
    public bool isgrounded, canPlayAnim;

    public Animator anim,golAnim;

    Vector3 direction;
    public GrappleV2 grappleScript;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Physics.IgnoreLayerCollision(7, 3);
        Physics.IgnoreLayerCollision(7, 6);
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");// uses imput to find direction
        direction = new Vector3(horizontal, 0f, vertical).normalized;
        //-----------------------------------------------------------------
        if (anim.GetCurrentAnimatorStateInfo(2).IsTag("attack"))
        {
            specialattacking = true;
            anim.SetLayerWeight(2, 1);
            var lookDir = transform.position - camAIM.position;
            lookDir.y = 0;
            transform.rotation = Quaternion.LookRotation(lookDir);
            anim.SetBool("specialattacking", true);
        }
        else
        {
            anim.SetBool("specialattacking", false);
            specialattacking = false;
            anim.SetLayerWeight(2, 0);
        }
        if (anim.GetCurrentAnimatorStateInfo(1).IsTag("attack"))
        {
            attacking = true;
            anim.SetLayerWeight(1, 1);
            var lookDir = transform.position - camAIM.position;
            lookDir.y = 0;
            transform.rotation = Quaternion.LookRotation(lookDir);
            anim.SetBool("attacking", true);
        }
        else
        {
            anim.SetBool("attacking", false);
            attacking = false;
        }







        //-----------------------------------------------------------------
        if (isgrounded == false)
        {
            anim.SetBool("Grounded", false);
            golAnim.SetBool("Grounded", false);
        }
        else
        {
            anim.SetBool("Grounded", true);
            golAnim.SetBool("Grounded", true);
        }
    }

    void FixedUpdate()
    {
        isgrounded = Physics.CheckSphere(groundcheck.position, grounddistance, groundmask);
        Jump();
        if (isgrounded == false && grappleScript.isgrappling == false && canPlayAnim == true && !anim.GetCurrentAnimatorStateInfo(0).IsName("flip"))
        {
            anim.Play("fall start");
            canPlayAnim = false;
        }
        if (isgrounded == true || grappleScript.isgrappling == true)
            canPlayAnim = true;
        //-----------------------------------------------------------------
        if (Input.GetKey(KeyCode.LeftShift))
        {
            maxVelocity = runSpeed;
            Walk(runSpeed);
            anim.SetFloat("Speed", 1f);
        }
        else
        {
            maxVelocity = speed;
            Walk(speed);
            anim.SetFloat("Speed", 0f);

        }
        //-----------------------------------------------------------------
        if (rb.velocity.sqrMagnitude > 0.2)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }
        //-----------------------------------------------------------------

    }

    void Walk(float speedIn)
    {
        if (direction.magnitude >= 0.1f)
        {
            float targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;// finds direction of movement

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnsmoothvelocity, turnsmoothing);// makes it so the player faces its movement direction
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 movedir = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward;// here is the movement
            rb.AddForce(movedir.normalized * speedIn * Time.deltaTime, ForceMode.Impulse);
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
        //-----------------------------------------------------------------
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
        if (rb.velocity.y > 0)
        {
            if (rb.velocity.sqrMagnitude > jumpVelocity)
            {
                Vector3 endVelocity = rb.velocity;
                endVelocity.y *= 0.9f;
                rb.velocity = endVelocity;

            }
        }
    }
}
