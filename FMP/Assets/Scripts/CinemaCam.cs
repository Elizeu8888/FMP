using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemaCam : MonoBehaviour
{
    Cinemachine.CinemachineFreeLook freeCamera;
    public Transform target;
    public Transform player;

    private void Awake()
    {
        freeCamera = GetComponent<Cinemachine.CinemachineFreeLook>();
    }
    private void FixedUpdate()
    {
        freeCamera.m_LookAt = player.transform;
        freeCamera.m_Follow = target.transform;
    }
}
