using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public float fractionToReplenish;
    public void ReplenishAmmo()
    {
        if (References.thePlayer!= null)
        {
            RefillWeapon(References.thePlayer.mainWeapon);
            RefillWeapon(References.thePlayer.secondaryWeapon);
            Destroy(gameObject);

        }
    }

    void RefillWeapon(WeaponBehavior weapon)
    {
        if (weapon != null)
        {
            weapon.currentAmmo += Mathf.RoundToInt(weapon.ammo * fractionToReplenish);
            weapon.currentAmmo = Mathf.Min(weapon.currentAmmo, weapon.ammo); //ensure refill doesn't exceed max
        }
    }
}
