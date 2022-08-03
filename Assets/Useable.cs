using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Useable : MonoBehaviour
{
    public UnityEvent whenUsed;
    public bool canBeReused; //bools default to false
    public string displayName;

    public bool alarmed;

    public void Use()
    {
        whenUsed.Invoke();
        if (alarmed)
        {
            References.alarmManager.RaiseAlertLevel();
            alarmed = false;
        }
        if (canBeReused == false)
        {
            enabled = false;
        }
    }

    //add/remove from References list
    void OnEnable()
    {
        References.useables.Add(this); //add this point to the List
    }
    private void OnDisable()
    {
        References.useables.Remove(this);
    }
}
