using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth, maxHealth;
    public Animator anim;
    public GameObject damageText, bloodSplat;
    public Transform respawnpoint, bloodpoint;
    public Healthbar healthscript;
    bool canTakeDamage;
    void Start()
    {
        healthscript.SetMaxHealth(maxHealth);
    }
    public void TakeDamage(int damage)
    {
        if (!anim.GetCurrentAnimatorStateInfo(3).IsName("dead") && canTakeDamage == true)
        {
            anim.Play("hurt");
            currentHealth -= damage;
            DamageIndicator indicator = Instantiate(damageText, bloodpoint.position, Quaternion.identity).GetComponent<DamageIndicator>();
            indicator.SetDamageText(damage);
            GameObject bloodyblood = Instantiate(bloodSplat, bloodpoint.position, Quaternion.identity);
            Destroy(bloodyblood, 2);
            canTakeDamage = false;
            //anim.Play("hurt");
        }

        if (currentHealth <= 0) anim.Play("dead");
    }

    public void Respawn()
    {
        transform.position = respawnpoint.position;
        currentHealth = maxHealth;
    }



    void FixedUpdate()
    {
        healthscript.SetHealth(currentHealth);
        if(anim.GetCurrentAnimatorStateInfo(4).IsName("dead"))
        {
            anim.SetLayerWeight(4, 1);
        }
        else
        {
            anim.SetLayerWeight(4, 0);
        }
        if (anim.GetCurrentAnimatorStateInfo(3).IsName("hurt"))
        {
            anim.SetLayerWeight(3, 1);
        }
        else
        {
            anim.SetLayerWeight(3, 0);
            canTakeDamage = true;
        }
    }
}
