using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public LayerMask enemylayer;
    public LayerMask blocklayer;
    public Transform cam;
    GrappleV2 grapScript;
    EnemyAI enemyScript;
    public CameraLock cameraScript;
    public RaycastHit rayHit;
    float distance;
    public bool isLocked;
    public GameObject cinemachine;



    void Start()
    {
        grapScript = GetComponent<GrappleV2>();
    }

    public void Deadagainlol()
    {

    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown("t") && isLocked == true)
        {
            isLocked = false;
            cinemachine.SetActive(true);
        }

        if (Input.GetKeyDown("t") && Physics.SphereCast(cam.position, 3, cam.forward, out rayHit, 29, blocklayer))
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
        else if (Physics.SphereCast(cam.position, 3, cam.forward, out rayHit, 29, blocklayer))
        {
            if (rayHit.transform.gameObject.tag == "enemy")
            {
                distance = Vector3.Distance(transform.position, rayHit.transform.gameObject.transform.position);
                if (rayHit.transform.gameObject.GetComponent<EnemyAI>() != null)
                {
                    if (rayHit.transform.gameObject.GetComponent<EnemyAI>().currentHealth <= 0)
                        cinemachine.SetActive(true);
                }
            }
            else
            {
                //Debug.Log("no enemy target?");
                cinemachine.SetActive(true);
            }
        }


 
            

        if(distance >= 200f)
        {
            cameraScript.target = gameObject.transform;
        }
    }
}