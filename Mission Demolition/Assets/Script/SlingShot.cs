/****
 * Created by Betzaida Ortiz Rivas
 * Script Created 2/9/22
 * 
 * Updated on 2/9/22
 * 
 * Dscription: 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShot : MonoBehaviour
{
    /**VARIABLES**/
    [Header("Set In Inpsector")]
    public GameObject prefabProjectile;
    public float VelocityMultiplier = 8f;

    [Header("Set Dynamically")]
    public GameObject LaunchPoint;
    public Vector3 LaunchPos; //launch position of projectile
    public GameObject Projectile; //instance of projectile
    public bool AimingMode; //is the player aiming
    public Rigidbody ProjectileRB; //rigid body of projectile

    private void Awake()
    {
        Transform LaunchPointTrans = transform.Find("LaunchPoint"); //find the child object
        LaunchPoint = LaunchPointTrans.gameObject; //the game object of this child object
        LaunchPoint.SetActive(false); // disable game object
        LaunchPos = LaunchPointTrans.position;
    } //end Awake

    private void OnMouseEnter()
    {
        print("Slingshot: OnMouseEnter"); // whne mouse hovers
        LaunchPoint.SetActive(true); // enable game object
        
    } //end OnMouseEnter

    private void Update()
    {
        if (!AimingMode) return;

        //get current mouse position in 2D screen coordinates
        Vector3 MouseCode2D = Input.mousePosition;
        MouseCode2D.z = -Camera.main.transform.position.z;
        Vector3 MousePos3D = Camera.main.ScreenToWorldPoint(MouseCode2D);

        Vector3 MouseDelta = MousePos3D - LaunchPos; //pixel amount of change between MousePos3D and LaunchPos

        //limit MouseDelta to SlingShot collider radius
        float MaxMagnitude = this.GetComponent<SphereCollider>().radius;

        if(MouseDelta.magnitude > MaxMagnitude)
        {
            MouseDelta.Normalize(); //sets the vector to the same direction, but its link is 1.0
            MouseDelta *= MaxMagnitude;
        } //end of if statement

        //Move projectile to this new position
        Vector3 ProjPos = LaunchPos + MouseDelta;
        Projectile.transform.position = ProjPos;

        if (Input.GetMouseButtonUp(0))
        {
            AimingMode = false;
            ProjectileRB.isKinematic = false;
            ProjectileRB.velocity = -MouseDelta * VelocityMultiplier; // velocity if multiplied to the mouseDelta
            FollowCam.POI = Projectile; //set the POI for the camera
            Projectile = null; //forget the last instance (the instance still exists but we don't have a ref to it)
        }

    }//end Update

    private void OnMouseExit()
    {
        print("Slingshot: OnMouseExit");
        LaunchPoint.SetActive(false); // disable game object

    } //end OnMouseExit

    private void OnMouseDown()
    {
        AimingMode = true;
        Projectile = Instantiate(prefabProjectile) as GameObject;
        Projectile.transform.position = LaunchPos;
        ProjectileRB = Projectile.GetComponent<Rigidbody>();
        ProjectileRB.isKinematic = true;
    }
}
