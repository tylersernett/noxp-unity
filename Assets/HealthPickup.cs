using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public void ReplenishHealth()
    {
        if (References.thePlayer != null)
        {
            References.thePlayer.GetComponentInParent<HealthSystem>().ReplenishHealth();
            Destroy(gameObject);
        }
    }
}
