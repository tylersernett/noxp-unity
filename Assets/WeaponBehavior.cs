using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponBehavior : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float accuracy;
    public float secondsBetweenShots;
    public float numberOfProjectiles;
    public float secondsSinceLastShot;

    public AudioSource audioSource;
    public float kickAmount;

    public int ammo;
    public int currentAmmo;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        secondsSinceLastShot = secondsBetweenShots; //INITIALIZEj here 
        audioSource = GetComponent<AudioSource>();
        currentAmmo = ammo;
    }

    // Update is called once per frame
    void Update()
    {
        secondsSinceLastShot += Time.deltaTime;
    }

    public void BePickedUpByPlayer()
    {
        

        //snap position to player
        transform.position = References.thePlayer.transform.position;
        transform.rotation = References.thePlayer.transform.rotation;
        //then parent weapon to player so it moves with them as well
        transform.SetParent(References.thePlayer.transform);
        //make it the currently active weapon
 
        References.thePlayer.PickUpWeapon(this);
    }

    public void Fire(Vector3 targetPosition)
    {
        //FIRING
        
        //if clicked, create bullet at our current position
        if (secondsSinceLastShot >= secondsBetweenShots && currentAmmo > 0)
        {
            //ready to fire
            References.alarmManager.SoundTheAlarm();
            audioSource.Play();
            References.cameraTools.joltVector = transform.forward * kickAmount;

            //multi-fire, like shotgun
            for (int i = 0; i < numberOfProjectiles; i++)
            {
                GameObject newBullet = Instantiate(bulletPrefab, transform.position + transform.forward, transform.rotation); //transform.forward = 1 unit in forward direction (z?)

                newBullet.GetComponent<BulletBehavior>().damage = damage;

                //offset target position by a random amount based on inaccuracy
                float inaccuracy = Vector3.Distance(transform.position, targetPosition) / accuracy; //distance from player to cursor / constant
                Vector3 inaccuratePosition = targetPosition;
                inaccuratePosition.x += Random.Range(-inaccuracy, inaccuracy);
                inaccuratePosition.z += Random.Range(-inaccuracy, inaccuracy);
                newBullet.transform.LookAt(inaccuratePosition);
                secondsSinceLastShot = 0;
            }
            currentAmmo--;
        }
    }

    public void Drop()
    {
        transform.parent = null; //no longer attached to player

        //move from 'don't destroy on load' into the regular scene (so it does get destroyed on new game)
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
        GetComponent<Useable>().enabled = true;
    }
}
