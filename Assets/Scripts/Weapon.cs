using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{

    [Header("Stats")]
    public float baseDamage = 10;
    public float baseSpeed = 1;


    public WeaponType type;

    public virtual void Interact()
    {

    }
}

public enum WeaponType
{
    Sword, Dagger, Hammer
}