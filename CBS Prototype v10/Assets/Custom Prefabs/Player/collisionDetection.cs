using UnityEngine;
using System.Collections;

public class collisionDetection : MonoBehaviour {

    public static bool fCollided;
    public static bool bCollided;
    public static bool lCollided;
    public static bool rCollided;

    public static bool pCollided;

    public float collisRange;
    RaycastHit forwardHit;
    RaycastHit backHit;
    RaycastHit leftHit;
    RaycastHit rightHit;


    // Use this for initialization
    void Start()
    {


    }

    void FixedUpdate()
    {
        fCollided = false;
        bCollided = false;
        lCollided = false;
        rCollided = false;

       

        
        // Forward ray check
        if (Physics.Raycast(transform.position, transform.forward, out forwardHit, collisRange))
        {
            fCollided = true;
        }

        // Back ray check
        if (Physics.Raycast(transform.position, -transform.forward, out backHit, collisRange))
        {
            bCollided = true;
        }

        // Left ray check
        if (Physics.Raycast(transform.position, -transform.right, out leftHit, collisRange))
        {
            lCollided = true;
        }

        // Right ray check
        if (Physics.Raycast(transform.position, transform.right, out rightHit, collisRange))
        {
            rCollided = true;
        }


        Debug.DrawRay(transform.position, transform.forward * collisRange, Color.black);
        Debug.DrawRay(transform.position, -transform.forward * collisRange, Color.black);
        Debug.DrawRay(transform.position, transform.right * collisRange, Color.black);
        Debug.DrawRay(transform.position, -transform.right * collisRange, Color.black);

        if(fCollided || bCollided || lCollided || rCollided)
        {
            pCollided = true;
        }

        else
        {
            pCollided = false;
        }

    }

    

   

    //void OnCollisionEnter(Collision col)
    //{
    //    if (col.gameObject.tag == "Floor")
    //    {

    //    }

    //    else
    //    {
    //        pCollided = true;
    //    }
    //}

    //void OnCollisionExit(Collision col)
    //{
    //    if (col.gameObject.tag == "Floor")
    //    {

    //    }

    //    else
    //    {
    //        pCollided = false;
    //    }
    //} 
}
