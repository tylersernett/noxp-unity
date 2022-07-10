using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject player;
    public float enemySpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody ourRigidBody = GetComponent<Rigidbody>();
        Vector3 vectorToPlayer = player.transform.position - transform.position; //dif. b/t two positions: destination - origin
        ourRigidBody.velocity = vectorToPlayer.normalized * enemySpeed; 
        
        //ourRigidBody.velocity = some kind of vector going towards player

    }
}
