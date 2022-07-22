using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmerBehavior : EnemyBehavior
{

    public GameObject explosionPrefab;

    protected void OnCollisionEnter(Collision thisCollision)
    {
        GameObject theirGameObject = thisCollision.gameObject;//.GetComponent<PlayerBehavior>;
        //PlayerBehavior theirBehavior = theirGameObject.GetComponent<PlayerBehavior>();

        //does the collision have the PlayerBehavior script?
        if (theirGameObject == References.thePlayer.gameObject)
        {
            //create an explosion, then destroy self
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
