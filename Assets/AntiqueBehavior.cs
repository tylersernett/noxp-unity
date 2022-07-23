using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiqueBehavior : MonoBehaviour
{

    public void BeCollected()
    {
        References.thePlayer.IncreaseScore(1);
        References.alarmManager.RaiseAlertLevel();
        Destroy(gameObject);
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
