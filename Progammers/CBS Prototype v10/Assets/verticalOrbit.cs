using UnityEngine;
using System.Collections;

public class verticalOrbit : MonoBehaviour {

    // Variables affecting how much the camera moves per cycle
    public float camSpeed;
    public float lookSensitivity;

    // Adjustable boundaries for the camera based on the PLAYERS origin
    public float upperLimit;
    public float lowerLimit;

    // Naming convention used for internal calculation
    float uLimit;
    float lLimit;

    // Variable that takes the input and converts it to a format that can be used to manipulate the game objects position
    Vector3 camSpeedVector;

    // Target to the Player GameObject. Used to create boundaries
    public Transform targetPlayer;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    
    {
        // Adjusts the number based on how much the mouse moves
        camSpeed = Input.GetAxis("Mouse Y") * lookSensitivity;                   

        // These limits are based on the Y position of the player, 
        uLimit = targetPlayer.position.y + upperLimit;
        lLimit = targetPlayer.position.y + lowerLimit;

        // If the camera is within the boundaries it is free to move
        if (transform.position.y <= uLimit && transform.position.y >= lLimit)
        {
            transform.position -= camSpeedVector;
        }

        // If the camera has hit the upper limit it can only move down
        else if (transform.position.y > uLimit)
        {
            if (camSpeed > 0)
            {
                transform.position -= camSpeedVector;
            }
        }
            
        // If the camera has hit the lower limit it can only move up
        else if (transform.position.y < lLimit)
        {
            if (camSpeed <= 0)
            {
                transform.position -= camSpeedVector;
            }
        }

        // Creates the new movement vector
        camSpeedVector.Set(0, camSpeed, 0); 
	}
}
