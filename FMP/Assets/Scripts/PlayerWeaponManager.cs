using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{

    public Collider weaponCOL;
    public Weapon weapScript;
    public GameObject[] weapons;
    public Weapon[] weaponList;
    GameObject weapon;
    public Transform rightHand;

    public bool weapondrawn = false;
    public Animator anim;

    public int weaponNumber;
    public GameObject weaponMenu;

    bool comboPossible;
    int comboStep;
    PlayerWalk playerScript;


    void Start()
    {
        playerScript = GetComponent<PlayerWalk>();
        weaponList = new Weapon[50];

        weaponList[0] = new Weapon();
        weaponList[0].InitWeapon("lava", 15, 0.7f);

        weaponList[1] = new Weapon();
        weaponList[1].InitWeapon("water", 10, 1.2f);

        weaponList[2] = new Weapon();
        weaponList[2].InitWeapon("toxin", 12, 1f);

        weaponList[3] = new Weapon();
        weaponList[3].InitWeapon("gum", 15, 0.7f);


    }

    void Update()
    {
        anim.SetFloat("speedanim", weaponList[weaponNumber].GetSpeed());
        if (Input.GetKeyDown("f"))
        {
            if (weapondrawn == false)
            {
                weaponMenu.SetActive(true);
            }
            else if (weaponMenu.activeSelf)
            {
                weaponMenu.SetActive(false);
            }
            weapondrawn = !weapondrawn;
            
        }


        if (weaponMenu.activeSelf)           
            Cursor.lockState = CursorLockMode.Confined;
        else
            Cursor.lockState = CursorLockMode.Locked;

        if (weapondrawn == false)
        {
            anim.SetBool("weaponOUT", false);
        }



    }


    public void Attack()
    {
        if(comboStep == 0)
        {
            anim.Play("attack1");
            comboStep = 1;
            return;
        }
        if(comboStep != 0)
        {
            if(comboPossible)
            {
                comboPossible = false;
                comboStep += 1;

            }
        }
    }
    public void ComboPossible()
    {
        comboPossible = true;
    }
    public void Combo()
    {
        if(comboStep == 2)
            anim.Play("attack2");
        if (comboStep == 3)
            anim.Play("attack3");

    }
    public void ComboReset()
    {
        comboPossible = false;
        comboStep = 0;
    }
    public void LaunchDamage(Collider col, float damage)
    {

        Collider[] cols = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, LayerMask.GetMask("HitBoxes"));
        foreach (Collider c in cols)
        {
            print("damageLaunched");

            if (c.transform.parent == transform)
            {
                continue;
            }

            Debug.Log(c.tag);

            switch (c.tag)
            {
                case "enemy":
                    anim.SetTrigger("hit");
                    break;
                default:
                    Debug.Log("nopedidntwork");
                    break;

            }            

            c.SendMessageUpwards("TakeDamage", damage);

        }

    }

    public void WeaponActivate(GameObject wep)
    {
        weapon = Instantiate(wep, new Vector3(0,0,0), Quaternion.identity);
        weapon.transform.parent = rightHand;
        weapon.transform.localScale = new Vector3(1, 1, 1);
        weapon.transform.localPosition = new Vector3(0, 0, 0);
        weapon.transform.localRotation = Quaternion.identity;
        weaponCOL = weapon.GetComponent<BoxCollider>();
    }

    public void WeaponLauncher()
    {
        WeaponActivate(weapons[weaponNumber]);
    }
    public void DealDamage()
    {
        LaunchDamage(weaponCOL, weaponList[weaponNumber].GetDamage());
    }
        
    public void RunicRedraw()
    {
        Destroy(weapon);      
    }
        
    public void LayerWeight()
    {
        anim.SetLayerWeight(1, 0);
    }

    public void Weapon1()
    {
        anim.SetLayerWeight(1, 1);
        anim.SetBool("weaponOUT", true);
        weaponNumber = 0;
        weaponMenu.SetActive(false);
    }
    public void Weapon2()
    {
        anim.SetLayerWeight(1, 1);
        anim.SetBool("weaponOUT", true);
        weaponNumber = 1;
        weaponMenu.SetActive(false);
    }
    public void Weapon3()
    {
        anim.SetLayerWeight(1, 1);
        anim.SetBool("weaponOUT", true);
        weaponNumber = 2;
        weaponMenu.SetActive(false);
    }
    public void Weapon4()
    {
        anim.SetLayerWeight(1, 1);
        anim.SetBool("weaponOUT", true);
        weaponNumber = 3;
        weaponMenu.SetActive(false);
    }
}

