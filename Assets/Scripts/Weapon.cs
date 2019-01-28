using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMeleeWeapon", menuName = "Weapon/Melee")]
public class Weapon : ScriptableObject
{

    [Header("Stats")]
    public float baseDamage = 10;
    public float baseSpeed = 1;

    [Header("Pattern")]
    public float[] combatPattern;
    public AnimationClip[] animations;
    public float[] speedperAttack;
    [Header("AttackArea")]
    public Vector2 radious;
    public Vector3 offset;

    public WeaponType type = WeaponType.Sword;


}

public enum WeaponType
{
    Sword, Dagger, Hammer, Bow
}