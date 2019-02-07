using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableWeapon : Interactable
{

    public Weapon weapon;


    public override void Interact()
    {
        PlayerFighting playerFighting = player.GetComponent<PlayerFighting>();
        playerFighting.EquipWeapon(weapon, 1);


        Destroy(gameObject);

    }


}
