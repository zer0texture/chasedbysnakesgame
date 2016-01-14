using UnityEngine;
using System.Collections;


public class HuntAi : MonoBehaviour {

    public float distance = 0.1f;
    public float rotate = 0.50f;
    Quaternion spreadAngle = Quaternion.AngleAxis(-15, new Vector3(0, 1, 0));

   

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hashit;
        Ray snakeRay = new Ray(transform.position, transform.forward);
        Quaternion spreadAngle = Quaternion.AngleAxis(15, new Vector3(0, 1, 0));
        Vector3 leftVec = spreadAngle * transform.forward;
        Vector3 RightVec = spreadAngle * transform.forward;

       // rightRay

        Debug.DrawRay(transform.position, transform.forward * distance, Color.red);
        Debug.DrawRay(transform.position, transform.forward * distance, Color.blue);
        Debug.DrawRay(transform.position, transform.forward * distance, Color.green);

        if(Physics.Raycast(snakeRay, distance))
        { 
            print("Ray Hit");
            gameObject.transform.Rotate(Vector3.up * rotate, Space.Self);
            
        }

        else
        {
            gameObject.transform.Translate((Vector3.forward) * 0.1f);
        }
        //Debug.DrawRay(transform.position, transform.forward * distance, Color.red);
        
	}
}
