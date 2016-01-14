using UnityEngine;
using System.Collections;

public class cameraV3 : MonoBehaviour {

    // Camera position variables \\
    Rigidbody rb;
    Vector3 rbPosition;
    public float cameraSpeed = 5;
    public Transform player;
    public Transform targetPos;

    // Camera rotation variables \\
    public Vector3 camLook;

    // Camera proximity variables \\
    float idealDistanceFromTarget;
    float distanceFromTarget;
    float idealDistanceFromPlayer;
    float distanceFromPlayer;
    [Range(1, 3)]
    public float bandingDistance;


	void Start () 
    {
        /// -- Getting the components -- \\\
        // Grab the rigidbody of the camera so it can be manipulated \\
        rb = GetComponent<Rigidbody>();

        /// -- Ignore collision between player and camera -- \\\
        Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), player.parent.GetComponent<Collider>());

        /// -- Finding starting distances -- \\\
        // Find the desired distance so they can be used for IF statements later
        idealDistanceFromTarget = Vector3.Distance(transform.position, targetPos.position);
        idealDistanceFromPlayer = Vector3.Distance(transform.position, player.position);

       

        /// -- Debugging -- \\\
        // Logs the ideal distance between the camera and the player \\
        Debug.Log("TD = " + idealDistanceFromTarget);
	}
	
	void FixedUpdate () 
    {
        cameraMove();
        cameraLook();
        cameraBanding();

        /// -- Debugging -- \\\
        // Draws a line between camera's current position and its target position   \\
        Debug.DrawLine(targetPos.position, transform.position, Color.magenta);
        // Logs the ideal distance between the camera and the player                \\
        //Debug.Log("CD is " + distanceFromTarget);
        // 
        Debug.DrawRay(transform.position, transform.forward * 1.5f, Color.green);
	}

    void cameraMove()
    {
        /// -- Moves Camera to default Position -- \\\
        // rbPosition finds the distance between its current position and its target position           \\
        // Multiplying it by the camera speed variable, which is then passed to the rigidbody velocity  \\
        // If the camera is in the correct position, then dont move                                     \\
        rbPosition = (targetPos.position - transform.position) * cameraSpeed;
        if (player.position == transform.position)
        {
            rb.velocity = Vector3.zero;
        }
        else
        {
            rb.velocity = rbPosition;
        }
    }
   
    void cameraLook()
    {
        /// -- Look at the correct position -- \\\
        // transform.LookAt is looking at the position camLook              \\
        // camLook is an empty gameObject that can be moved around in Unity \\
        camLook = player.transform.position;
        transform.LookAt(camLook);
    }

    void cameraBanding()
    {
        /// -- Resetting camera if it gets stuck -- \\\
        // Find the current distance from camera to the player and the target position \\
        distanceFromTarget = Vector3.Distance(transform.position, targetPos.position);
        distanceFromPlayer = Vector3.Distance(transform.position, player.position);

        // If the distance checks show that the camera is too far away from the target  \\
        // then ignore collisions until it returns to its ideal position.               \\
        // Player check ensures that the camera wont snap through a wall if the player  \\
        // backs up against it.                                                         \\
        if (distanceFromTarget > idealDistanceFromTarget * bandingDistance && distanceFromPlayer > idealDistanceFromPlayer)
        {
            rb.detectCollisions = false;
        }
        if (rb.detectCollisions == false)
        {
            if (distanceFromTarget <= idealDistanceFromTarget)
            {
                rb.detectCollisions = true;
            }
        }
        
        // Fires a raycast to check for a total obstruction between player and the      \\
        // camera. If it returns true, move forward using vectors. This is because      \\
        // rigidbody's will not pass through obstacles                                  \\

        // NOTE: When artists make the levels floor and walls, make the ray look at tags\\
        // rather than the player name, since it acts odd around objects in the         \\
        // world that should not interact with the camera (e.g. oil refill)             \\
        Ray obRay = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(obRay, out hit, 1.5f))
        {
            if (hit.transform.name == "loadedPlayer")
            {

            }
            else
            {
                Debug.Log("Something in the way");
                transform.position = transform.position + transform.forward;
            }
        }

    }
}
