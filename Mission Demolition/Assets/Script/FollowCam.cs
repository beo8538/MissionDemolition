/****
 * Created by Betzaida Ortiz Rivas
 * Script Created 2/14/22
 * 
 * Last Edited by: NA
 * Updated on 2/16/22
 * 
 * Dscription: 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    /***VARIABLES***/
    static public GameObject POI; // the static point of interest

    [Header("Set Dynamically")]
    public float CamZ; //Desired Z pose of the camera
    [Header("Set In Inspector")]
    public float Easing = 0.05f; //5% of a move over time
    public Vector2 minXY = Vector2.zero; 

    private void Awake()
    {
        CamZ = this.transform.position.z; // sets the Z value
    } // end Awake()


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // FixedUpdate
    void FixedUpdate()
    {
        //if (POI == null) return; // do nothing is no POI
        //Vector3 destination = POI.transform.position;

        Vector3 destination;
        if(POI == null)
        {
            destination = Vector3.zero; //destination is zero
        } //end if(POI == null)
        else
        {
            destination = POI.transform.position;
            if(POI.tag == "Projectile")
            {
                if(POI.GetComponent<Rigidbody>().IsSleeping())
                {
                    POI = null; //null the POI if rigidbody is asleep
                    return; //happens in the next update
                } //end if(POI.GetComponent<Rigidbody>().IsSleeping())
            } //end if(POI.tag == "Projectile")
        } //end else

        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);

        //Interpolate from current position to destination
        destination = Vector3.Lerp(transform.position, destination, Easing);
        destination.z = CamZ;
        transform.position = destination;

        //orthographic view
        Camera.main.orthographicSize = destination.y + 10; // keep track of ball
    } //end FixedUpdate()
}
