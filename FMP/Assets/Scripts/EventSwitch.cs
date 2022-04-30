using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class EventSwitch : MonoBehaviour
{
    public UnityEvent use;
    public UnityEvent altUse;
    public UnityEvent altUse2;
    public UnityEvent altUse3;
    public UnityEvent altUse4;
    public UnityEvent altUse5;
    public UnityEvent altUse6;
    public UnityEvent damage;
    //public SoundManager soundscript;

    public void Damage()
    {
        damage.Invoke();
    }
    public void Use()
    {
        use.Invoke();
    }

    public void AltUse()
    {
        altUse.Invoke();
    }

    public void AltUse2()
    {
        altUse2.Invoke();
    }

    public void AltUse3()
    {
        altUse3.Invoke();
    }

    public void AltUse4()
    {
        altUse4.Invoke();
    }
    public void AltUse5()
    {
        altUse5.Invoke();
    }
    public void AltUse6()
    {
        altUse6.Invoke();
    }
}
