using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class GrappleV2 : MonoBehaviour
{
    public LineRenderer lr1,lr2;
    Vector3 grapplePoint;
    Vector3 grappleAimPoint;
    public LayerMask whatsIsGrappleable;
    public Transform camera,camLeft,camRight, player;
    public float maxDistance;
    SpringJoint joint;
    public Rig playerRig;
    //public Animator anim;
    public GameObject grapplepoint, grappleTip, grappleTip2;
    public Rigidbody rb;

    public float swing = 0.8f, pull = 0.05f;

    public float aimSize = 10f, aimSideSize = 2f;

    bool isgrappling = false, isgrappling2 = false;

    public Animator anim;
    bool grappleMode = false;


    void Start()
    {

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            grappleMode = !grappleMode;
        }

        if(isgrappling == true || isgrappling2 == true)
        {
            anim.SetBool("Grappling", true);
        }
        else
        {
            anim.SetBool("Grappling", false);
        }


        if(grappleMode == true)
        {

            grappleTip2.SetActive(true);
            grappleTip.SetActive(true);


            if (Input.GetMouseButtonDown(1) && isgrappling2 == false)
            {
                StartGrapple(pull);
                isgrappling = true;
            }
            else if (Input.GetMouseButtonUp(1))
            {
                StopGrapple();
                //playerRig.weight = 0;
                isgrappling = false;

            }
            if (Input.GetMouseButtonDown(0) && isgrappling == false)
            {
                StartGrapple(swing);
                isgrappling2 = true;
                
            }
            else if (Input.GetMouseButtonUp(0))
            {
                StopGrapple();
                isgrappling2 = false;
            }

            if (Physics.SphereCast(camera.position, aimSize, camera.forward, out RaycastHit aimhit, maxDistance, whatsIsGrappleable))
            {
                grappleAimPoint = aimhit.point;
                grapplepoint.SetActive(true);
                grapplepoint.transform.position = grappleAimPoint; ;
            }
            else
            {
                if (Physics.SphereCast(camRight.position, aimSideSize, camRight.forward, out RaycastHit aimside, maxDistance, whatsIsGrappleable))
                {
                    grappleAimPoint = aimside.point;
                    grapplepoint.SetActive(true);
                    grapplepoint.transform.position = grappleAimPoint;
                }
                else
                {

                    if (Physics.SphereCast(camLeft.position, aimSideSize, camLeft.forward, out RaycastHit aimsideL, maxDistance, whatsIsGrappleable))
                    {
                        grappleAimPoint = aimsideL.point;
                        grapplepoint.SetActive(true);
                        grapplepoint.transform.position = grappleAimPoint;
                    }
                    else
                    {
                        grapplepoint.SetActive(false);
                    }
                }


            }
        }
        else
        {
            grappleTip2.SetActive(false);
            grappleTip.SetActive(false);
            StopGrapple();
        }


    }

    void LateUpdate()
    {
        DrawRope();
    }

    void StartGrapple(float maxdist)
    {
        RaycastHit hit;
        if(Physics.SphereCast(camera.position,aimSize, camera.forward, out hit, maxDistance, whatsIsGrappleable))
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distancefrompoint = Vector3.Distance(player.position, grapplePoint);

            joint.maxDistance = distancefrompoint * maxdist;
            joint.minDistance = distancefrompoint * 0.05f;

            joint.spring = 5f;
            joint.damper = 2f;
            joint.massScale = 15f;
            rb.angularDrag = 0f;

            lr1.positionCount = 2;
            lr2.positionCount = 2;

            //playerRig.weight = 1;
        }
        else
        {
            if (Physics.SphereCast(camRight.position, aimSideSize, camRight.forward, out RaycastHit hitside, maxDistance, whatsIsGrappleable))
            {
                grapplePoint = hitside.point;
                joint = player.gameObject.AddComponent<SpringJoint>();
                joint.autoConfigureConnectedAnchor = false;
                joint.connectedAnchor = grapplePoint;

                float distancefrompoint = Vector3.Distance(player.position, grapplePoint);
                joint.maxDistance = distancefrompoint * maxdist;
                joint.minDistance = distancefrompoint * 0.05f;

                joint.spring = 5f;
                joint.damper = 2f;
                joint.massScale = 15f;
                rb.angularDrag = 0f;

                lr1.positionCount = 2;
                lr2.positionCount = 2;
            }
            else
            {
                if (Physics.SphereCast(camLeft.position, aimSideSize, camLeft.forward, out RaycastHit hitsideL, maxDistance, whatsIsGrappleable))
                {
                    grapplePoint = hitsideL.point;
                    joint = player.gameObject.AddComponent<SpringJoint>();
                    joint.autoConfigureConnectedAnchor = false;
                    joint.connectedAnchor = grapplePoint;

                    float distancefrompoint = Vector3.Distance(player.position, grapplePoint);
                    joint.maxDistance = distancefrompoint * maxdist;
                    joint.minDistance = distancefrompoint * 0.05f;

                    joint.spring = 5f;
                    joint.damper = 2f;
                    joint.massScale = 15f;
                    rb.angularDrag = 0f;

                    lr1.positionCount = 2;
                    lr2.positionCount = 2;
                }
            }
        }
        
    }
    void DoubleGrappleStart()
    {
        if(Physics.SphereCast(camera.transform.position,aimSize,camera.transform.forward, out RaycastHit raycastHit,maxDistance,whatsIsGrappleable))
        {

        }
    }

    void DrawRope()
    {
        if (!joint) return;
        lr1.SetPosition(0, grappleTip.transform.position);
        lr1.SetPosition(1, grapplePoint);
        lr2.SetPosition(0, grappleTip2.transform.position);
        lr2.SetPosition(1, grapplePoint);
    }



    void StopGrapple()
    {
        lr1.positionCount = 0;
        lr2.positionCount = 0;
        Destroy(joint);
    }

    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }


}
