using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class References 
{
    //kind of like global.vars
    //points to entire player object (could also reference the specific behavior script...initialize '= this' in script)
    public static GameObject thePlayer; 
    public static GameObject canvas;
    public static EnemySpawner spawner;

    public static Screenshake screenshake;

    public static List<NavPoint> navPoints = new List<NavPoint>(); //use new when initializing Lists

    public static float maxDistanceInALevel = 1000;

    public static LayerMask wallsLayer = LayerMask.GetMask("Walls"); //include as many as you want
    public static LayerMask enemiesLayer = LayerMask.GetMask("Enemies");
}
