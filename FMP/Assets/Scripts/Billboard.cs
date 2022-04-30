using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private GameObject cam;


    void Start()
    {
        cam = GameObject.Find("Main Camera");
    }


    void LateUpdate()
    {
        transform.LookAt(cam.transform.position);
    }
}
