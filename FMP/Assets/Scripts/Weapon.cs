using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    //public GameObject obj;
    public string name;
    public float damage;
    public float atkSpeed;
    public void InitWeapon(string name, float damage, float atkSpeed)
    {
        this.name = name;
        this.damage = damage;
        this.atkSpeed = atkSpeed;
    }
    public float GetDamage()
    {
        return damage;
    }
    public float GetSpeed()
    {
        return atkSpeed;
    }
}
