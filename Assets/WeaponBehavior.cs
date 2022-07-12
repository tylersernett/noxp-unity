using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehavior : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float accuracy;
    public float secondsBetweenShots;
    public float numberOfProjectiles;
    float secondsSinceLastShot;

    // Start is called before the first frame update
    void Start()
    {
        secondsSinceLastShot = secondsBetweenShots; //INITIALIZE here 
    }

    // Update is called once per frame
    void Update()
    {
        secondsSinceLastShot += Time.deltaTime;
    }

    public void Fire(Vector3 targetPosition)
    {
        //FIRING
        
        //if clicked, create bullet at our current position
        if (secondsSinceLastShot >= secondsBetweenShots)
        {
            //ready to fire
            for (int i = 0; i < numberOfProjectiles; i++)
            {
                GameObject newBullet = Instantiate(bulletPrefab, transform.position + transform.forward, transform.rotation); //transform.forward = 1 unit in forward direction (z?)

                //offset target position by a random amount based on inaccuracy
                float inaccuracy = Vector3.Distance(transform.position, targetPosition) / accuracy; //distance from player to cursor / constant
                targetPosition.x += Random.Range(-inaccuracy, inaccuracy);
                targetPosition.z += Random.Range(-inaccuracy, inaccuracy);
                newBullet.transform.LookAt(targetPosition);
                secondsSinceLastShot = 0;
            }
        }
    }
}
