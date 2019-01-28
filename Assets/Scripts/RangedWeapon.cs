using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewRangedWeapon", menuName = "Weapon/Ranged")]
public class RangedWeapon : Weapon
{
    [Header("Ranged Attributes")]
    public GameObject arrowPrefab;
    public Vector3 arrowSpawnOffset;
    public float arrowForce;



}
