using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponPanel : MonoBehaviour
{
    public TextMeshProUGUI weaponName;
    public TextMeshProUGUI ammoCount;

    public WeaponBehavior myWeapon;

    public void AssignWeapon(WeaponBehavior weapon)
    {
        myWeapon = weapon;
        weaponName.text = weapon.GetComponent<Useable>().displayName;
    }

    // Update is called once per frame
    void Update()
    {
        if (myWeapon != null)
        {
            ammoCount.text = myWeapon.ammo.ToString();
        }
    }
}
