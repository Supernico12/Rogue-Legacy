using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon" , menuName = "Weapon")]
public class Weapon : ScriptableObject {

    [Header("Stats")]
    public float baseDamage = 10;
    public float baseSpeed = 1;

    [Header("Pattern")]
    public float[] combatPattern;
    public AnimationClip[] animations;
    [Header("AttackArea")]
    public float radious;
    public Vector3 offset;
}
