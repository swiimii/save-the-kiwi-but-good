using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : Affectable
{
    public bool isRepaired;
    public override bool Effect(string equipmentUsed)
    {
        if (equipmentUsed == "Wrench")
        {
            Repair();
            return true;
        }
        return false;
    }
    public void Repair()
    {
        isRepaired = true;
        GetComponent<Animator>().SetBool("isRepaired", true);
    }
}
