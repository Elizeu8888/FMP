using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Death");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator Death()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
