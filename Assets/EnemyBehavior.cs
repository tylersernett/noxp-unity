using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float enemySpeed;
    public float visionRange;
    public float visionConeAngle;
    public bool alerted;
    Rigidbody ourRigidBody;
    public Light myLight;
    public float turnSpeed;

    // Start is called before the first frame update
    void Start()
    {
        alerted = false;
        ourRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {


        if (References.thePlayer != null) {
            Vector3 playerPosition = References.thePlayer.transform.position;
            Vector3 vectorToPlayer = playerPosition - transform.position; //dif. b/t two positions: destination - origin
            myLight.color = Color.white;

            if (alerted)
            {
                //follow the player
                ourRigidBody.velocity = vectorToPlayer.normalized * enemySpeed;//ourRigidBody.velocity = some kind of vector going towards player
                Vector3 playerPositionAtOurHeight = new Vector3 (playerPosition.x, transform.position.y, playerPosition.z); //enemy was tilting before -- this locks the y perspective
                transform.LookAt(playerPositionAtOurHeight);
                myLight.color = Color.red;
            }
            else 
            {
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
                        
                        alerted = true;
                    }
                }
            }
        }
    }

    private void OnCollisionEnter(Collision thisCollision)
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
