using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equippable : MonoBehaviour
{
    [SerializeField] private Sprite equipment;
    private void Start()
    {
        if(!equipment)
            equipment = GetComponent<SpriteRenderer>().sprite;
    }
    public Sprite Equip()
    {
        gameObject.SetActive(false);
        return equipment;
    }
}
