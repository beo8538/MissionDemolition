/****
 * Created by Betzaida Ortiz Rivas
 * Script Created 2/16/22
 * 
 * Last Edited by: NA
 * Updated on 2/16/22
 * 
 * Dscription: Put rigidbody to sleep
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))] //adds component in () to the Level Design

public class RigidBodySleep : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) rb.Sleep();
    } //end start

    // Update is called once per frame
    void Update()
    {
        
    }
}
