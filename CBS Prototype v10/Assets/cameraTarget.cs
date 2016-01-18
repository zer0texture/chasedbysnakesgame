using UnityEngine;
using System.Collections;

public class cameraTarget : MonoBehaviour {

    public static Vector3 pos;
    public static Vector3 targetPos;
    public static bool collided;
    public float cameraSpeed;
    public bool hit;

	// Use this for initialization
	void Start () 
    {
        targetPos = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () 
    {
        pos = transform.localPosition;


        if (collided)
        {
            transform.localPosition += (transform.forward * cameraSpeed);
        }

	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag != "MainCamera")
        {
            collided = true;
            hit = true;
        }
        
    }

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag != "MainCamera")
        {
            collided = true;
            hit = true;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag != "MainCamera")
        {
            collided = false;
            hit = false;
        }
    }

}
