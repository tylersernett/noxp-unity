using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float enemySpeed;

    public float secondsBetweenSpawns;
    float secondsSinceLastSpawn = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        secondsSinceLastSpawn += Time.deltaTime;
        if (secondsSinceLastSpawn >= secondsBetweenSpawns)
        {
            secondsSinceLastSpawn = 0;
            //Instantiate(gameObject);
        }

        if (References.thePlayer != null) { 
            Rigidbody ourRigidBody = GetComponent<Rigidbody>();
            Vector3 vectorToPlayer = References.thePlayer.transform.position - transform.position; //dif. b/t two positions: destination - origin
            ourRigidBody.velocity = vectorToPlayer.normalized * enemySpeed;
            //ourRigidBody.velocity = some kind of vector going towards player
        }
    }

    private void OnCollisionEnter(Collision thisCollision)
    {
        GameObject theirGameObject = thisCollision.gameObject;
        //does the collision have the EnemyBehavior script?
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
