/****
 * Created by Betzaida Ortiz Rivas
 * Script Created 2/16/22
 * 
 * Last Edited by: NA
 * Updated on 2/16/22
 * 
 * Description: Create trailing line behind projectile
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLine : MonoBehaviour
{
    static public ProjectileLine S; //singleton

    [Header("Set In Inpspector")]
    public float minDist = 0.1f;
    private LineRenderer line;
    private GameObject _poi;
    private List<Vector3> points;

    private void Awake()
    {
        S = this; //sets the singleton

        line = GetComponent<LineRenderer>(); // reference to lineRenderer
        line.enabled = false; //disable lineRenderer
        points = new List<Vector3>(); //new list

    } //end awake

    public GameObject poi
    {
        get 
        { 
            return (_poi);
        }
        set
        {
            _poi = value;
            if (_poi != null)
            {
                line.enabled = false;
                points = new List<Vector3>();
                AddPoint();
            }
        }
    }

    //This can be used to clear the line directly
    public void Clear()
    {
        _poi = null;
        line.enabled = false;
        points = new List<Vector3>();
    }

    public void AddPoint()
    {
        //This is called to add point to the line
        Vector3 pt = _poi.transform.position;
        if(points.Count == 0 && (pt - lastPoint).magnitude < minDist)
        {
            //if the point isn't far enough from the last point, it returns
            return;
        }
        if(points.Count == 0) //If this is the launch point...
        {
            Vector3 launchPosDiff = pt - SlingShot.LAUNCH_POS; //To be defined
            //...it adds an extra bit of line to aid aiming later
            points.Add(pt + launchPosDiff);
            points.Add(pt);
            line.positionCount = 2;
            //Sets the first two points
            line.SetPosition(0, points[0]);
            line.SetPosition(1, points[1]);
            //Enables the LineRenderer
            line.enabled = true;
        }
        else
        {
            //Normal behavior of adding a point
            points.Add(pt);
            line.positionCount = points.Count;
            line.SetPosition(points.Count - 1, lastPoint);
            line.enabled = true;
        }
    }

    public Vector3 lastPoint
    {
        get
        {
            if(points == null)
            {
                //if there are no points, returns Vector3.zero
                return (Vector3.zero);
            }
            return (points[points.Count - 1]);
        }
    }
    private void FixedUpdate()
    {
        if(poi == null)
        {
            //If there is no poi, search for one
            if(FollowCam.POI != null)
            {
                if(FollowCam.POI.tag == "Projectile")
                {
                    poi = FollowCam.POI;
                } 
                else
                {
                    return; //Return if we didn't find a poi
                }
            }
            else
            {
                return; // Return if we didn't find a poi
            }
        }
        //If there is a poi, it's loc is added every FixedUpdate
        AddPoint();
        if(FollowCam.POI == null)
        {
            //Once FollowCam.POI is null, make the local poi null too
            poi = null;
        }
    }
}
