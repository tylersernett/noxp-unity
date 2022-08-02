using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlarmManager : MonoBehaviour
{
    public GameObject alertPipPrefab;
    public List<Image> alertPips = new List<Image>();

    public int alertLevel;
    public int maxAlertLevel;

    public Sprite emptyPip;
    public Sprite filledPip;

    public AudioSource alarmSound;

    public float alarmDelay;
    float secondsUntilAlarm;

    private void Awake()
    {
        References.alarmManager = this;
        alarmSound = GetComponent<AudioSource>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (AlarmHasSounded() && alarmSound.isPlaying == false)
        {
            alarmSound.Play();
        }
        if (AlarmHasSounded() == false && alarmSound.isPlaying)
        {
            alarmSound.Stop();
        }

        if (MaxAlertLevelReached() && secondsUntilAlarm > 0)
        {
            References.canvas.alarmCountdownText.enabled = true;
            secondsUntilAlarm -= Time.deltaTime;
            References.canvas.alarmCountdownText.text = secondsUntilAlarm.ToString("N1");
        }
        else
        {
            References.canvas.alarmCountdownText.enabled = false;
        }
    }

    public void SetUpLevel(int desiredMaxAlertLevel)
    {
        for (int i = 0; i < alertPips.Count; i++)
        {
            Destroy(alertPips[i].gameObject);
        }
        alertPips.Clear();
        maxAlertLevel = desiredMaxAlertLevel;
        //for each alert level, create a pip and store them in a list we can access later
        for (int i = 0; i < maxAlertLevel; i++)
        {
            GameObject newPip = Instantiate(alertPipPrefab, transform);
            alertPips.Add(newPip.GetComponent<Image>());
        }
        alertPips.Reverse(); //so we build from the bottom up

        secondsUntilAlarm = alarmDelay;
    }

    public void RaiseAlertLevel()
    {
        alertLevel++;
        UpdatePips();
    }

    //sound alarm with no grace period
    public void SoundTheAlarm()
    {
        alertLevel = maxAlertLevel;
        secondsUntilAlarm = 0;
        UpdatePips();
    }

    public void StopTheAlarm()
    {
        alertLevel = 0;
        UpdatePips();
    }

    public bool MaxAlertLevelReached()
    {
        if (maxAlertLevel == 0) { return false; }
        return alertLevel >= maxAlertLevel;
    }

    public bool AlarmHasSounded()
    {
        return MaxAlertLevelReached() && secondsUntilAlarm <= 0; //&& timer expired
    }


    public void UpdatePips()
    {
        for (int i = 0; i < alertPips.Count; i++)
        {
            if (i < alertLevel)
            {
                alertPips[i].sprite = filledPip;
            }
            else
            {
                alertPips[i].sprite = emptyPip;
            }
        }
    }

}
