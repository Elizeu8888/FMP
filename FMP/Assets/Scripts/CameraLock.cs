using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLock : MonoBehaviour
{
    public Transform target;
    public Transform player;
    public float cameraSlack;
    public float cameraDistance;

    private Vector3 pivotPoint;

    void Start()
    {
        pivotPoint = transform.position;
    }

    void Update()
    {
        if(target != null)
        {
            Vector3 current = pivotPoint;
            Vector3 target1 = player.transform.position + Vector3.up;
            pivotPoint = Vector3.MoveTowards(current, target1, Vector3.Distance(current, target1) * cameraSlack);

            transform.position = pivotPoint;
            transform.LookAt((target.position + player.position) / 2);
            transform.position -= transform.forward * cameraDistance;
        }

    }
}
