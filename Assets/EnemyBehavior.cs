using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public float enemySpeed;
    protected Rigidbody ourRigidBody;
    public NavMeshAgent navAgent;

    /* this is a parent class for ALL enemies -- all enemies use this */
    //Protected: can be used by children & us, but no one else
    //private: child cannot inherit -- only this owner
    //virtual -- CAN be overridden by children. If they don't override, then parent code is used.
    //void: function returns nothing

    // Start is called before the first frame update
    protected virtual void Start()
    {
        ourRigidBody = GetComponent<Rigidbody>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        ChasePlayer();
    }

    //protected: children can inherit

    protected void ChasePlayer()
    {
        if (References.thePlayer != null)
        {
            navAgent.destination = References.thePlayer.transform.position;
            navAgent.speed = enemySpeed;
            /*
            Vector3 playerPosition = References.thePlayer.transform.position;
            Vector3 vectorToPlayer = playerPosition - transform.position; //dif. b/t two positions: destination - origin

            //follow the player
            ourRigidBody.velocity = vectorToPlayer.normalized * enemySpeed;//ourRigidBody.velocity = some kind of vector going towards player
            Vector3 playerPositionAtOurHeight = new Vector3(playerPosition.x, transform.position.y, playerPosition.z); //enemy was tilting before -- this locks the y perspective
            transform.LookAt(playerPositionAtOurHeight);
            */
        }
    }

    protected void OnCollisionEnter(Collision thisCollision)
    {
        GameObject theirGameObject = thisCollision.gameObject;
        //does the collision have the PlayerBehavior script?
        if (theirGameObject.GetComponent<PlayerBehavior>() != null)
        {
            HealthSystem theirHealthSystem = theirGameObject.GetComponent<HealthSystem>();
            if (theirHealthSystem != null)
            {
                theirHealthSystem.TakeDamage(1);
            }
        }
    }
}
