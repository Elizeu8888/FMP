using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{

    public Collider attackrange;
    public float damage;

    void Start()
    {
        
    }

    public void LaunchDamage(Collider col, float damage)
    {

        Collider[] cols = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, LayerMask.GetMask("Player"));
        foreach (Collider c in cols)
        {           

            if (c.transform.parent == transform)
            {
                continue;
            }

            Debug.Log(c.tag);

            switch (c.tag)
            {
                case "Player":
                    
                    break;
                default:
                    Debug.Log("nopedidntwork");
                    break;

            }

            c.SendMessageUpwards("TakeDamage", damage);

        }

    }


    void OnTriggerEnter(Collider cal)
    {
        LaunchDamage(attackrange, damage);
    }


}
