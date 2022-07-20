using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehavior : MonoBehaviour
{
    public float secondsToExist;
    private float secondsWeveBeenAlive;
    public float damage;

    public GameObject soundObject;
    // Start is called before the first frame update
    void Start()
    {
        secondsWeveBeenAlive = 0;
        transform.localScale = Vector3.zero; //avoid split second MAX SCALE issue when explosion spawns
        Instantiate(soundObject, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        secondsWeveBeenAlive += Time.fixedDeltaTime;

        float lifeFraction = secondsWeveBeenAlive / secondsToExist;
        Vector3 maxScale = Vector3.one * 5;
        transform.localScale = Vector3.Lerp(Vector3.zero, maxScale, lifeFraction) ;

        if (secondsWeveBeenAlive >= secondsToExist)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        //look for health system in item we collide with. if found, do lots of damage.
        HealthSystem theirHealthSystem = collision.gameObject.GetComponent<HealthSystem>();
        if (theirHealthSystem != null)
        {
            theirHealthSystem.TakeDamage(damage);
        } 
    }
}
