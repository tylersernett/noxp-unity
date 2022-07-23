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
        if (AlarmHasSounded() == false && alarmSound.isPlaying )
        {
            alarmSound.Stop();
        }
    }

    public void SetUpLevel(int desiredMaxAlertLevel)
    {
        maxAlertLevel = desiredMaxAlertLevel;
        //for each alert level, create a pip and store them in a list we can access later
        for (int i = 0; i < maxAlertLevel; i++)
        {
            GameObject newPip = Instantiate(alertPipPrefab, transform);
            alertPips.Add(newPip.GetComponent<Image>());
        }
        alertPips.Reverse(); //so we build from the bottom up
    }

    public void RaiseAlertLevel()
    {
        alertLevel++;
        UpdatePips();
    }

    public void SoundTheAlarm()
    {
        alertLevel = maxAlertLevel;
        UpdatePips();
    }

    public bool AlarmHasSounded()
    {
        return alertLevel >= maxAlertLevel;
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
