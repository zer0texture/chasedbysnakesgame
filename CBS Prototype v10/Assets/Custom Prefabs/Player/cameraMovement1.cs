using UnityEngine;
using System.Collections;

public class cameraMovement1 : MonoBehaviour {

    public static float yRot;
    float lookSensitivity = 3.5f;

    RaycastHit fwdHit;
    RaycastHit bckHit;
    public float fwdDistance;
    public float bckDistance;

    public GameObject player;

    Vector3 playerRay;
    Vector3 idealPos;
    Vector3 clearPos;

    Rigidbody rb;

    float xPos;
    float yPos;

    public float speed = 0;

    float vDistance;

    public bool forwardHit = false;

	// Use this for initialization
	void Start () 
    {
        playerRay.Set(0,0.5f,0);
        idealPos = transform.localPosition;
        xPos = transform.localPosition.x;
        yPos = transform.localPosition.y;
	}
	
	// Update is called once per frame
	void Update () 
    {

        //yRot += Input.GetAxis("Mouse Y") * lookSensitivity;

        vDistance = Vector3.Distance(transform.localPosition, player.transform.localPosition);

        fwdRay();
        bckRay();
        
    }

    void fwdRay()
    {
        if(Physics.Raycast(transform.position, transform.forward, out fwdHit, fwdDistance))
        {
            if (fwdHit.transform.name == "Player")
            {
                Debug.Log("HIT!");
                forwardHit = false;
            }

            else
            {
                forwardHit = true;
                transform.position = transform.position + (transform.forward * speed);
            }
        }

        else
        {
            Debug.Log("MISS");
            if(fwdHit.transform.name == "Player")
            {
                
            }
            
        }
        
        Debug.DrawRay(transform.position, transform.forward * fwdDistance, Color.red);
    }



    void bckRay()
    {
        if (!Physics.Raycast(transform.position, -transform.forward, out bckHit, bckDistance) && forwardHit == false)
        {
            if (transform.localPosition != idealPos)
            {
                transform.position = transform.position - (transform.forward * speed);
            }
        }

        Debug.DrawRay(transform.position, -transform.forward * bckDistance, Color.red);
    }






    //void OnCollisionEnter(Collision col)
    //{
    //    //transform.localPosition = Vector3.Lerp(transform.localPosition, hit.transform.position, 1);
    //    //transform.localPosition = Vector3.Lerp(transform.localPosition, clearPos, 1);
    //    //transform.localPosition = transform.localPosition + (transform.forward * speed);
        
        

    //}

    //void OnCollisionStay(Collision col)
    //{
    //    //Debug.DrawRay(transform.position, transform.forward * distance, Color.red);
    //}

    //void OnCollisionExit(Collision col)
    //{
    //    //transform.localPosition = Vector3.Lerp(transform.localPosition, idealPos, 1);  
    //    transform.localPosition = Vector3.Lerp(transform.localPosition, idealPos, 1.0f);
    //}
}
