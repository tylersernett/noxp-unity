using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Plinth : MonoBehaviour
{
    Useable myUseable;
    public TextMeshProUGUI myLabel;
    public Transform spotForItem;

    public GameObject cage;

    public float secondsToLock;

    private void OnEnable()
    {
        References.plinths.Add(this);
    }

    private void OnDisable()
    {
        References.plinths.Remove(this);
    }

    public void AssignItem(GameObject item)
    {
        myUseable = item.GetComponent<Useable>();
        myLabel.text = myUseable.displayName;
        myUseable.transform.position = spotForItem.transform.position;
        myUseable.transform.rotation = spotForItem.transform.rotation;
    }

    private void Update()
    {
        if (myUseable!= null && myUseable.enabled == false)
        {
            //don't allow plinth to disable items the player has already picked up
            myUseable = null;
        }
        if (secondsToLock > 0 && References.alarmManager.AlarmHasSounded())
        {
            secondsToLock -= Time.deltaTime;
            if (secondsToLock <= 0)
            {
                cage.SetActive(true);
                myLabel.text = "ALARM";
                //if the plinth item is still on the plinth, destroy it
                if (myUseable != null && myUseable.enabled)
                {
                    Destroy(myUseable.gameObject);
                }
            }
        }
    }
}
