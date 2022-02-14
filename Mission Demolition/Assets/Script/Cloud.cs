/****
 * Created by Betzaida Ortiz Rivas
 * Script Created 2/14/22
 * 
 * Last Edited by: NA
 * Updated on 2/14/22
 * 
 * Dscription: create randomly generated cloud
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    /***VARIABLES***/
    [Header("Set In Inspector")]
    public GameObject cloudSphere;
    public int numSphereMin = 6;
    public int numSphereMax = 10;
    public Vector2 sphereScaleRangeX = new Vector2(4, 8); // how big the x scale can be
    public Vector2 sphereScaleRangeY = new Vector2(3, 4); // how big the y scale can be
    public Vector2 sphereScaleRangeZ = new Vector2(2, 4); // how big the z scale can be
    public Vector3 sphereOffsetScale = new Vector3(5, 2, 1);
    public float scaleYMin = 2f; // scaled down in y direction depending how far it scaled in x dimension

    private List<GameObject> spheres;


    // Start is called before the first frame update
    void Start()
    {
        spheres = new List<GameObject>();
        int num = Random.Range(numSphereMin, numSphereMax);

        for(int i = 0; i < num; i++)
        {
            GameObject SP = Instantiate<GameObject>(cloudSphere);
            spheres.Add(SP);

            Transform SPTrans = SP.transform;
            SPTrans.SetParent(this.transform); //sets location relevant to the cloud object (parent)

            //Randomly assign a position
            Vector3 offset = Random.insideUnitSphere;
            offset.x *= sphereOffsetScale.x;
            offset.y *= sphereOffsetScale.y;
            offset.z *= sphereOffsetScale.z;

        } //end for loop 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
