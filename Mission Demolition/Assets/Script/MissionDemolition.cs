/****
 * Created by Betzaida Ortiz Rivas
 * Script Created 2/17/22
 * 
 * Last Edited by: NA
 * Updated on 2/19/22
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
    public GameMode mode = GameMode.idle;
    public string showing = "Show Slingshot"; //FollowCam mode

    // Start is called before the first frame update
    void Start()
    {
        S = this; //define the Singleton

        level = 0;
        levelMax = castles.Length; //the amount of levels depends on amount of castles
        StartLevel();
    }

    void StartLevel()
    {
        //Get rid of the old castle if one exists
        if(castle != null)
        {
            Destroy(castle);
        }

        //Destroy old projectiles if they still exist
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
        foreach(GameObject pTemp in gos)
        {
            Destroy(pTemp);
        }

        //Instantiate the new castle
        castle = Instantiate<GameObject>(castles[level]);
        castle.transform.position = castlePos;
        shotsTaken = 0;

        //reset camera
        SwitchView("Show Both");
        ProjectileLine.S.Clear();

        //Reset the goal
        Goal.goalMet = false;

        UpdateGUI();

        mode = GameMode.playing;
    }

    void UpdateGUI()
    {
        uitLevel.text = "Level: " + (level + 1) + " of " + levelMax;
        uitShot.text = "Shots Taken: " + shotsTaken;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGUI();

        //check for level end
        if((mode == GameMode.playing) && Goal.goalMet)
        {
            //Change mode to stop checking for level end
            mode = GameMode.levelEnd;
            //zoom out
            SwitchView("Show Both");
            //Start the next level in 2 seconds
            Invoke("NextLevel", 2f);
        }
    }

    void NextLevel()
    {
        level++;
        if(level == levelMax)
        {
            level = 0;
        }
        StartLevel();
    }

    public void SwitchView(string eView = "")
    {
        if (eView == "")
        {
            eView = uitButton.text;
        }
        showing = eView;
        switch (showing)
        {
            case "Show Slingshot":
                FollowCam.POI = null;
                uitButton.text = "Show Castle";
                break;

            case "Show Castle":
                FollowCam.POI = S.castle;
                uitButton.text = "Show Both";
                break;

            case "Show Both":
                FollowCam.POI = GameObject.Find("ViewBoth");
                uitButton.text = "Show Slingshot";
                break;
        }
    }

    //Static method that allows code anywhere to increment shotsTaken
    public static void ShotFired()
    {
        S.shotsTaken++;
    }
}
