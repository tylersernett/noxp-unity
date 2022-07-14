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
    public static LayerMask wallsLayer = LayerMask.GetMask("Walls"); //include as many as you want
}
