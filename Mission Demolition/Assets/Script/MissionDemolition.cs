/****
 * Created by Betzaida Ortiz Rivas
 * Script Created 2/17/22
 * 
 * Last Edited by: NA
 * Updated on 2/17/22
 * 
 * Description: Game State Manager of the game
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode
{
    idle,
    playing,
    levelEnd
}

public class MissionDemolition : MonoBehaviour
{
    static private MissionDemolition S; //a private Singleton

    [Header("Set In Inpsector")]
    public Text uitLevel; //the UIText_Level Text
    public Text uitShot; //the UIText_Shots Text
    public Text uitButton; //the Text on UIButton_View
    public Vector3 castlePos; //the place to put castles
    public GameObject[] castles; //An array of the castles

    [Header("Set Dynamically")]
    public int level; //the current level
    public int levelMax; //the number of levels
    public int shotsTaken; //shots the player takes
    public GameObject castle; //the current castle


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
