/****
 * Created by Betzaida Ortiz Rivas
 * Script Created 2/16/22
 * 
 * Last Edited by: NA
 * Updated on 2/16/22
 * 
 * Dscription: Create trailing line behind projectile
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
    }

}
