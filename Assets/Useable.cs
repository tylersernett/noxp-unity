using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Useable : MonoBehaviour
{
    public UnityEvent whenUsed;
    public bool canBeReused; //bools default to false

    public void Use()
    {
        whenUsed.Invoke();
        if (canBeReused == false)
        {
            enabled = false;
        }
    }

    void OnEnable()
    {
        References.useables.Add(this); //add this point to the List
    }


    private void OnDisable()
    {
        References.useables.Remove(this);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
