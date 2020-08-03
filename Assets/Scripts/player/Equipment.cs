using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : MonoBehaviour
{
    public GameObject obj;
    public string equipmentName;

    public void EquipObject(GameObject equippable)
    {
        obj.gameObject.SetActive(true);
        equipmentName = equippable.name;
        obj.GetComponent<Image>().sprite = equippable.GetComponent<Equippable>().Equip();
    }

    public void AffectObject( GameObject target, bool equipmentIsReusable = false )
    {
        var myTarget = target.GetComponent<Affectable>();
        myTarget.Effect(equipmentName);
        if(!equipmentIsReusable)
        {
            obj.gameObject.SetActive(false);
        }
    }
}
