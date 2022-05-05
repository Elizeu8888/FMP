using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class EventSwitch : MonoBehaviour
{
    public UnityEvent use,altUse, altUse2, altUse3, altUse4, altUse5, altUse6, altUse7;
    public UnityEvent damage,attackball,attack2,attack3;
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
    public void AltUse7()
    {
        altUse7.Invoke();
    }
}
