using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailgunBeam : BulletBehavior
{
    public LineRenderer myBeam;


    // Start is called before the first frame update
    void Start()
    {
        //step 1 - fire laser to see how far we go before hitting wall
        //Raycast(startingPoint, dir, var to store info about ray hits, distance to travel, layerMask)
        Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, References.maxDistanceInALevel, References.wallsLayer);

        float distanceToWall = hitInfo.distance;

        //step 2 - fire new laser, only going as far as wall, but check for enemies now
        float beamThickness = 0.3f;
        RaycastHit[] listOfHitInfo = Physics.SphereCastAll(transform.position, beamThickness, transform.forward, distanceToWall, References.enemiesLayer);
        foreach (RaycastHit enemyHitInfo in listOfHitInfo)
        {
            HealthSystem theirHealthSystem = enemyHitInfo.collider.GetComponentInParent<HealthSystem>();
            if (theirHealthSystem != null)
            {
                theirHealthSystem.TakeDamage(damage); //use InParent for guards
            }
        }


        //step 3 - show the beam
        myBeam.SetPosition(0, transform.position);
        myBeam.SetPosition(1, hitInfo.point);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
