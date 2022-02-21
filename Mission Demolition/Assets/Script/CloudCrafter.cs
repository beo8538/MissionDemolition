/****
 * Created by Betzaida Ortiz Rivas
 * Script Created 2/16/22
 * 
 * Last Edited by: NA
 * Updated on 2/16/22
 * 
 * Description: create randomly generated cloud(s)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCrafter : MonoBehaviour
{
    [Header("Set In Inspector")]
    public int numClouds = 40; // number of clouds to make
    public GameObject cloudPrefab; //prefaob for clouds
    public Vector3 cloudPosMin = new Vector3(-50, -5, 10);
    public Vector3 cloudPosMax = new Vector3(150, 100, 10);
    public float cloudScaleMin = 1; //min scale of each cloud
    public float cloudScaleMax = 3; //max scale of each cloud
    public float cloudSpeedMult = 0.5f; // adjust the speed of clouds

    private GameObject[] cloudInstances;

    void Awake()
    {
        //Make an arrat large enough to hold all the Cloud_ instances
        cloudInstances = new GameObject[numClouds];
        //Find the CloudAnchor parent GameObject
        GameObject anchor = GameObject.Find("CloudAnchor");
        //Iterate through and make Clouds
        GameObject cloud;
        for(int i = 0; i < numClouds; i++)
        {
            //Make an instance of the cloudPrefab
            cloud = Instantiate<GameObject>(cloudPrefab);
            //Position of cloud
            Vector3 cPos = Vector3.zero;
            cPos.x = Random.Range(cloudPosMin.x, cloudPosMax.x);
            cPos.y = Random.Range(cloudPosMin.y, cloudPosMax.y);
            //Scale cloud
            float scaleU = Random.value;
            float scaleVal = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scaleU);
            //Smaller clouds should be further away
            cPos.z = 100 - 90 * scaleU;
            //Apply these transformers to the cloud
            cloud.transform.position = cPos;
            cloud.transform.localScale = Vector3.one * scaleVal;
            //Make cloud as the child of the anchor
            cloud.transform.SetParent(anchor.transform);
            //Add cloud to cloudInstances
            cloudInstances[i] = cloud;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Iterate over each cloud that was created
        foreach(GameObject cloud in cloudInstances)
        {
            //Get the cloud scale and position
            float scaleVal = cloud.transform.localScale.x;
            Vector3 cPos = cloud.transform.position;
            //Move larger clouds faster
            cPos.x -= scaleVal * Time.deltaTime * cloudSpeedMult;
            //If a cloud has moved to far to the left
            if(cPos.x <= cloudPosMin.x)
            {
                //Move it to the far right
                cPos.x = cloudPosMax.x;
            }
            //Apply the new position to cloud
            cloud.transform.position = cPos;
        }
    }
}
