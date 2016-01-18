using UnityEngine;
using System.Collections;

public class cameraV2 : MonoBehaviour {

    RaycastHit rh;
    
    Vector3 rhToLocal;
    public float cameraSpeed = 5;
    Rigidbody rb;

    public Vector3 startingPos;
    Vector3 rbPosition;

    public Quaternion startingRot;
    Quaternion rbRotation;

    public Transform player;
    public Transform targetPos;

    float targetDistance;

	// Use this for initialization
	void Start () 
    {
        startingPos = transform.localPosition;
        startingRot = transform.localRotation;
        rb = GetComponent<Rigidbody>();
        rb.maxDepenetrationVelocity = 0;
        rb.maxAngularVelocity = 0;
        //rb.freezeRotation = true;
	}
	
    

	// Update is called once per frame
	void FixedUpdate () 
    {



       /// -- Moves Camera to default Position -- \\\
        rbPosition = (startingPos - transform.localPosition) * cameraSpeed * Time.deltaTime;
        if(transform.localPosition == startingPos)
        {
            rb.velocity = Vector3.zero;
        }
        else
        {
            rb.velocity = rbPosition;
        }






        //if (transform.localRotation == startingRos)
        //{
            
        //}
        //else
        //{
        //    rb.MoveRotation(startingRos);
        //}


        //rb.MoveRotation(Quaternion.Euler(0,90,0));
        /// -- Moves Camera to default Rotation -- \\\
        //rbRotation = (startingRos * Quaternion.Inverse(transform.localRotation));
        //if (transform.localRotation == startingRos)
        //{
            
        //}
        //else
        //{
        //    rb.rotation = rbRotation;
        //}

        //targetDistance = Vector3.Distance(transform.position, player.position);
        //Debug.Log(targetDistance);

        

        //rb.sleepThreshold = 1;
        //rb.inertiaTensor = Vector3.zero;
        //rb.inertiaTensorRotation = player.rotation;



        // Draws a line between camera's current position and its target position
        Debug.DrawLine(startingPos, transform.position, Color.magenta);
	}

   
}
