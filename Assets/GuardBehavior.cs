using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// : parentBehavior to inhereit 
public class GuardBehavior : EnemyBehavior
{
    public float visionRange;
    public float visionConeAngle;
    public bool alerted;
    public Light myLight;
    public float turnSpeed;

    /* this is specifically GUARD behavior -- stuff for ALL enemies should be in EnemyBehavior*/

    //Protected: can be used by children & us, but no one else
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        alerted = false;
        GoToRandomNavPoint();
    }

    void GoToRandomNavPoint()
    {
        int randomNavPointIndex = Random.Range(0, References.navPoints.Count); //inclusive, exclusive for int. (inclusive, inclusive for float)
        navAgent.destination = References.navPoints[randomNavPointIndex].transform.position;
    }

    // Update is called once per frame
    protected override void Update()
    {


        if (References.thePlayer != null)
        {
            Vector3 playerPosition = References.thePlayer.transform.position;
            Vector3 vectorToPlayer = playerPosition - transform.position; //dif. b/t two positions: destination - origin, in DIRECITONAL vector form
            myLight.color = Color.white;

            if (alerted)
            {
                //follow the player
                ChasePlayer();
                myLight.color = Color.red;
            }
            else
            {
                
                if (navAgent.remainingDistance < 0.5f)
                {
                    GoToRandomNavPoint();
                }
                
                //Rotate
                Vector3 lateralOffset = transform.right * Time.deltaTime * turnSpeed;
                transform.LookAt(transform.position + transform.forward + lateralOffset);
                //walk in circle
                ourRigidBody.velocity = transform.forward * enemySpeed;

                //check if we can see player
                if (Vector3.Distance(transform.position, playerPosition) <= visionRange) //first check distance, then angle
                {
                    if (Vector3.Angle(transform.forward, vectorToPlayer) <= visionConeAngle)
                    {
                        //if there's no walls between guard and player...
                        if (Physics.Raycast(transform.position, vectorToPlayer, vectorToPlayer.magnitude, References.wallsLayer) == false)
                        {
                            alerted = true;
                            References.spawner.activated = true;
                        }

                    }
                }
            }
        }
    }


}
