using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public GameObject slash,slashpoint,ball,ballpoint;
    public LayerMask enemylayer;
    public LayerMask blocklayer;
    public float cooldown1;
    public Transform cam;
    GrappleV2 grapScript;
    EnemyAI enemyScript;
    public CameraLock cameraScript;
    public RaycastHit rayHit;
    float distance;
    public bool isLocked;
    public GameObject cinemachine;
    PlayerWeaponManager weapScript;
    bool alreadyAttacked;

    void Start()
    {
        grapScript = GetComponent<GrappleV2>();
        weapScript = GetComponent<PlayerWeaponManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && weapScript.weapondrawn == true && !weapScript.weaponMenu.activeSelf)
        {
            weapScript.Attack();
        }
        if(Input.GetKeyDown("1") && weapScript.weapondrawn == true && !weapScript.weaponMenu.activeSelf)
        {
            Attack1();
        }
        if (Input.GetKeyDown("2") && weapScript.weapondrawn == true && !weapScript.weaponMenu.activeSelf)
        {
            Attack2();
        }
    }

    public void Attack1()
    {
        if (!alreadyAttacked && !weapScript.anim.GetCurrentAnimatorStateInfo(4).IsName("dead") && !weapScript.anim.GetCurrentAnimatorStateInfo(3).IsName("hurt"))
        {
            //Make sure enemy doesn't move
            //if (ishurt == false)   
            weapScript.anim.Play("attack2");
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), cooldown1);
        }
    }
    public void Attack2()
    {
        if (!alreadyAttacked && !weapScript.anim.GetCurrentAnimatorStateInfo(4).IsName("dead") && !weapScript.anim.GetCurrentAnimatorStateInfo(3).IsName("hurt"))
        {
            //Make sure enemy doesn't move
            //if (ishurt == false)   
            weapScript.anim.Play("attack3");
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), cooldown1);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    public void ShootSlash()
    {
        ///Attack code here
        Rigidbody rb = Instantiate(slash, slashpoint.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        Destroy(rb.gameObject, 2f);
        rb.AddForce(transform.forward * 154f, ForceMode.Impulse);
        //rb.AddForce(transform.up * 8f, ForceMode.Impulse);
        ///End of attack code
    }
    public void ballshoot()
    {
        ///Attack code here
        Rigidbody rb = Instantiate(ball, ballpoint.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        Destroy(rb.gameObject, 0.5f);
        //rb.AddForce(transform.forward * 154f, ForceMode.Impulse);
        //rb.AddForce(transform.up * 8f, ForceMode.Impulse);
        ///End of attack code
    }
    void FixedUpdate()
    {
        if (Input.GetKeyDown("t") && isLocked == true)
        {
            isLocked = false;
            cinemachine.SetActive(true);
        }

        if (Input.GetKeyDown("t") && Physics.SphereCast(cam.position, 2, cam.forward, out rayHit, 29, blocklayer) && isLocked == false)
        {
            print("hitsomthing IDK");
            if (rayHit.transform.gameObject.tag == "enemy")
            {
                print("hitessss");
                cameraScript.target = rayHit.transform;
                cinemachine.SetActive(false);
                isLocked = true;
            }

        }
        else if (Physics.SphereCast(cam.position, 2, cam.forward, out rayHit, 29, blocklayer))
        {
            if (rayHit.transform.gameObject.tag == "enemy")
            {
                distance = Vector3.Distance(rayHit.transform.gameObject.transform.position, transform.position);
                if (rayHit.transform.gameObject.GetComponent<EnemyAI>() != null)
                {
                    if (rayHit.transform.gameObject.GetComponent<EnemyAI>().currentHealth <= 0)
                        cinemachine.SetActive(true);
                }
            }

        }


 
            

        if(distance >= 200f)
        {
            cinemachine.SetActive(true);
        }
    }
}
