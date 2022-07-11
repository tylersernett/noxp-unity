using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float health;
    public GameObject healthBarPrefab;

    HealthBar myHealthBar;

    // Start is called before the first frame update
    void Start()
    {
        //create health panel ON the canvas
        GameObject healthBarObject = Instantiate(healthBarPrefab, References.canvas.transform); //create healthbarobject
        myHealthBar = healthBarObject.GetComponent<HealthBar>();//fetch healthbar component from the object
    }


    //void: function does not return anything
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        //add canvas space: (shifts distance depending how close we are to camera)
        //myHealthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position) + Vector3.up * 30;
        //go to camera, enter player position in 3d, get a pseudo-2d response (that's still Vector3)

        myHealthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2);

    }
}
