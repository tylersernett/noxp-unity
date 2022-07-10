using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float bulletSpeed;
    public float secondsUntilDestroyed;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody ourRigidBody = GetComponent<Rigidbody>();
        ourRigidBody.velocity = transform.forward * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        secondsUntilDestroyed -= Time.deltaTime; //reduce by # seconds each update

        if (secondsUntilDestroyed < 1)
        {
            transform.localScale *= secondsUntilDestroyed; //Vector3.one -> (1, 1, 1)
        }

        if (secondsUntilDestroyed < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision thisCollision)
    {
        GameObject theirGameObject = thisCollision.gameObject;
        //does the collision have the EnemyBehavior script?
        if (theirGameObject.GetComponent<EnemyBehavior>() != null)
        {
            HealthSystem theirHealthSystem = theirGameObject.GetComponent<HealthSystem>();
            if (theirHealthSystem != null)
            { 
                theirHealthSystem.TakeDamage(damage);
            }
            Destroy(gameObject); //destroy bullet itself
        }
    }
}
