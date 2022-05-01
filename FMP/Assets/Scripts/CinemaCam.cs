using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemaCam : MonoBehaviour
{
    Cinemachine.CinemachineFreeLook freeCamera;
    [SerializeField] Transform target;

    private void Awake()
    {
        freeCamera = GetComponent<Cinemachine.CinemachineFreeLook>();
    }
    private void Start()
    {
        freeCamera.m_LookAt = target.transform;
        freeCamera.m_Follow = target.transform;
    }
}
