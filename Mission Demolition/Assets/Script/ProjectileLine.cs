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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
                AddPoints();
            }
        }
    }
}
