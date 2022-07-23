using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmerBehavior : EnemyBehavior
{

    public GameObject explosionPrefab;

    protected void OnCollisionEnter(Collision thisCollision)
    {
        GameObject theirGameObject = thisCollision.gameObject;//.GetComponent<PlayerBehavior>;
        PlayerBehavior possiblePlayer = theirGameObject.GetComponent<PlayerBehavior>();

        //does the collision have the PlayerBehavior script?
        if (possiblePlayer == References.thePlayer)
        {
            //create an explosion, then destroy self
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
