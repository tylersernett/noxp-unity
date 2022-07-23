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
    public WeaponBehavior myWeapon;
    public float reactionTime;
    float secondsSeeingPlayer;

    /* this is specifically GUARD behavior -- stuff for ALL enemies should be in EnemyBehavior*/

    //Protected: can be used by children & us, but no one else
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        alerted = false;
        GoToRandomNavPoint();
        secondsSeeingPlayer = 0;
    }

    void GoToRandomNavPoint()
    {
        int randomNavPointIndex = Random.Range(0, References.navPoints.Count); //inclusive, exclusive for int. (inclusive, inclusive for float)
        navAgent.destination = References.navPoints[randomNavPointIndex].transform.position;
    }

    protected bool CanSeePlayer()
    {
        if (References.thePlayer == null)
        {
            return false;
        }
        Vector3 vectorToPlayer = PlayerPosition() - transform.position;
        if (Physics.Raycast(
            transform.position,
            vectorToPlayer,
            out RaycastHit hitInfo,
            vectorToPlayer.magnitude, //only go as far as player
            References.wallsLayer
        ))
        {
            //ray hit wall before player -- can't see player
            return false;
        }
        else
        {
            //ray hit player before any walls - can see player
            return true;
        }


    }

    protected Vector3 PlayerPosition()
    {
        return References.thePlayer.transform.position;
    }

    public void KnockoutAttempt()
    {
        if (References.alarmManager.AlarmHasSounded() == false)
        {
            GetComponent<HealthSystem>().KillMe();
            References.alarmManager.RaiseAlertLevel();
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (References.alarmManager.AlarmHasSounded())
        {
            alerted = true;
        }

        if (References.thePlayer != null)
        {
            Vector3 vectorToPlayer = PlayerPosition() - transform.position; //dif. b/t two positions: destination - origin, in DIRECITONAL vector form
            myLight.color = Color.white;

            if (alerted)
            {
                //follow the player
                ChasePlayer();
                myLight.color = Color.red;
                if (CanSeePlayer())
                {
                    secondsSeeingPlayer += Time.deltaTime;
                    transform.LookAt(PlayerPosition()); //look at player before we're ready to fire
                    if (secondsSeeingPlayer >= reactionTime)
                    {
                        myWeapon.Fire(PlayerPosition());
                    }
                }
                else
                {
                    secondsSeeingPlayer = 0;
                }
            }
            else
            {

                if (navAgent.remainingDistance < 0.5f)
                {
                    GoToRandomNavPoint();
                }

                //check if we can see player
                if (Vector3.Distance(transform.position, PlayerPosition()) <= visionRange) //first check distance, then angle
                {
                    if (Vector3.Angle(transform.forward, vectorToPlayer) <= visionConeAngle)
                    {
                        //if there's no walls between guard and player...
                        if (Physics.Raycast(transform.position, vectorToPlayer, vectorToPlayer.magnitude, References.wallsLayer) == false)
                        {
                            alerted = true;
                            References.alarmManager.SoundTheAlarm();
                        }

                    }
                }
            }
        }
    }


}
