using UnityEngine;
using System.Collections;

public class gravity : MonoBehaviour {

    public static bool grounded;
    RaycastHit hit;
    public float groundDistance;

    Vector3 downward;


	// Use this for initialization
	void Start () {

        downward.Set(0, -1, 0);

	}
	
	// Update is called once per frame
	void Update () {

        if (Physics.Raycast(transform.position, downward, out hit, groundDistance))
        {
            grounded = true;
        }

        else
        {
            grounded = false;
            
        }

        Debug.DrawRay(transform.position, downward * groundDistance, Color.red);

	}
}
