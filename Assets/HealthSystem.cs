using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HealthSystem : MonoBehaviour
{
    [FormerlySerializedAs("health")] //tell unity this was the old name to avoid losing data
    public float maxHealth;
    float currentHealth;
    
    public GameObject healthBarPrefab;
    HealthBar myHealthBar;

    public GameObject deathEffectPrefab;
    public GameObject lootDrop;

    public float bounty;
    public float chanceOfBounty;
    public float secondsForBountyToDecay;
    float decayRate;

    public bool showScoreMenu;

    // Start is called before the first frame update
    void Start()
    {
        //create health panel ON the canvas
        currentHealth = maxHealth;
        GameObject healthBarObject = Instantiate(healthBarPrefab, References.canvas.gameUIParent); //create healthbarobject
        
        myHealthBar = healthBarObject.GetComponent<HealthBar>();//fetch healthbar component from the object

        if (Random.value > chanceOfBounty)
        {
            bounty = 0;
        } 
        if (secondsForBountyToDecay != 0) //prevent division by 0
        {
            decayRate = bounty / secondsForBountyToDecay;
        }

        
    }

    public void KillMe()
    {
        TakeDamage(currentHealth);
    }

    public void ReplenishHealth()
    {
        currentHealth = maxHealth;
    }

    //void: function does not return anything
    public void TakeDamage(float damageAmount)
    {
        if (currentHealth > 0) //check we're actually alive first: prevent multiple explosions
        { 
            currentHealth -= damageAmount;
            if (currentHealth <= 0)
            {
                //This is where we die
                if (deathEffectPrefab != null)
                {
                    Instantiate(deathEffectPrefab, transform.position, transform.rotation);
                }
                if (lootDrop!= null)
                {
                    Instantiate(lootDrop, transform.position, transform.rotation);
                }
                References.scoreManager.IncreaseScore(BountyAsInt());
                if (showScoreMenu)
                {
                    References.canvas.ShowScoreMenu();
                }
                Destroy(gameObject);
            }
        }
    }

    int BountyAsInt()
    {
        return Mathf.FloorToInt(bounty);
    }

    // Update is called once per frame
    void Update()
    {
        //add canvas space: (shifts distance depending how close we are to camera)
        //myHealthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position) + Vector3.up * 30;
        //go to camera, enter player position in 3d, get a pseudo-2d response (that's still Vector3)
        myHealthBar.ShowHealthFraction(currentHealth / maxHealth );
        myHealthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2);

        if (bounty > 0)
        {
            bounty -= decayRate * Time.deltaTime; 
            myHealthBar.bountyText.enabled = true;
        } else
        {
            bounty = 0;
            myHealthBar.bountyText.enabled = false;
        }

        myHealthBar.bountyText.text = BountyAsInt().ToString();
    }

    //don't create things in the OnDestroy event -- it's only for cleaning up after yourself -- unity destroys things on gameEnd
    private void OnDestroy()
    {
        if (myHealthBar != null)
        { 
        Destroy(myHealthBar.gameObject);
        }
    }
}
