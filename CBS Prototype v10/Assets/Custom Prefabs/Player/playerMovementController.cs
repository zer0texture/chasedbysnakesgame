using UnityEngine;
using System.Collections;


public class playerMovementController : MonoBehaviour
{

    public static bool forward;
    public static bool back;
    public static bool left;
    public static bool right;
    public static bool use;
    public static bool jump;
    public static bool lantern;

    public float lookSensitivity;
    public static float xRot;
    public static float yRot;

    void Start()
    {

    }

    void Update()
    {
        if (use)
            use = false;
        holdKeyCheck();
        toggleKeyCheck();
        mouseLook();
    }

    void holdKeyCheck()
    {

        //-- KeyDown --//
        if (Input.GetKeyDown(KeyCode.W))
        {
            forward = true;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            back = true;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            left = true;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            right = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            //use = true;
        }

        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    Application.LoadLevel(0);           // Quit to main menu
        //}

        //-- KeyUp --//

        if (Input.GetKeyUp(KeyCode.W))
        {
            forward = false;
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            back = false;
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            left = false;
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            right = false;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            jump = false;
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            use = true;
        }
    }

    void toggleKeyCheck()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {

            if (lantern)
            {
                lantern = false;
            }
            else
            {
                lantern = true;
            }
        }
    }
  
    void mouseLook()
    {
        xRot -= Input.GetAxis("Mouse Y") * lookSensitivity;                   //Rotational axis to equal mouse axis
        yRot += Input.GetAxis("Mouse X") * lookSensitivity;                   //Rotational axis to equal mouse axis
    }

    void OnLevelWasLoaded(int level)
    {
        forward = false;
        back = false;
        left = false;
        right = false;
        use = false;
        jump = false;
    }
}