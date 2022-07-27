using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class References 
{
    //kind of like global.vars
    //points to entire player object (could also reference the specific behavior script...initialize '= this' in script)
    public static PlayerBehavior thePlayer; 
    public static CanvasBehavior canvas;
    public static List<EnemySpawner> spawners = new List<EnemySpawner>();
    public static List<EnemyBehavior> allEnemies = new List<EnemyBehavior>();
    public static List<Useable> useables = new List<Useable>();
    public static List<Plinth> plinths = new List<Plinth>();
    public static LevelManager levelManager;
    public static AlarmManager alarmManager;
    public static LevelGenerator levelGenerator;


    public static Persistent essentials;

    public static Screenshake screenshake;

    public static List<NavPoint> navPoints = new List<NavPoint>(); //use new when initializing Lists

    public static float maxDistanceInALevel = 1000;

    public static LayerMask wallsLayer = LayerMask.GetMask("Walls"); //include as many as you want
    public static LayerMask enemiesLayer = LayerMask.GetMask("Enemies");
}
