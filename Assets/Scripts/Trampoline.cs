using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : Affectable
{
    public override void Effect()
    {
        Repair();
    }
    public void Repair()
    {
        print("Fixed!");
    }
}
