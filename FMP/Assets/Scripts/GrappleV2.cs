using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class GrappleV2 : MonoBehaviour
{
    LineRenderer lr;
    Vector3 grapplePoint;
    public LayerMask whatsIsGrappleable;
    public Transform grappleTip, camera, player;
    public float maxDistance;
    SpringJoint joint;
    public Rig playerRig;
    //public Animator anim;
    public GameObject grapplepoint;
    public Rigidbody rb;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        
        if(Input.GetMouseButtonDown(1))
        {
            StartGrapple();

        }
        else if (Input.GetMouseButtonUp(1))
        {
            StopGrapple();
            //playerRig.weight = 0;

        }
    }

    void LateUpdate()
    {
        DrawRope();
    }

    void StartGrapple()
    {
        RaycastHit hit;
        if(Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatsIsGrappleable))
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distancefrompoint = Vector3.Distance(player.position, grapplePoint);

            joint.maxDistance = distancefrompoint * 0.5f;
            joint.minDistance = distancefrompoint * 0.2f;

            joint.spring = 5f;
            joint.damper = 3f;
            joint.massScale = 15f;
            rb.angularDrag = 0f;

            lr.positionCount = 2;

            //playerRig.weight = 1;
            grapplepoint.SetActive(true);
            grapplepoint.transform.position = grapplePoint;
        }
        else
        {

        }
        
    }

    void DrawRope()
    {
        if (!joint) return;
        lr.SetPosition(0, grappleTip.position);
        lr.SetPosition(1, grapplePoint);
    }



    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
        grapplepoint.SetActive(false);
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
