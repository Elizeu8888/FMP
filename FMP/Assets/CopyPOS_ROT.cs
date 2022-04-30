using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPOS_ROT : MonoBehaviour
{

    public Transform handpos;
    // Update is called once per frame
   void FixedUpdate()
    {
        transform.position = handpos.position;
        transform.rotation = handpos.rotation;
    }
}
