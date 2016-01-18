using UnityEngine;
using System.Collections;

public class playerMovement : MonoBehaviour
{

    public static Rigidbody rb;
    public static float speed;
    public float slowDown;
    public float jumpForce;
    public static float noise;

    //Work out which movement anim to play?
    public static bool moveBack;
    public static bool moveForward;
    public static bool moveRight;
    public static bool moveLeft;
    public static bool isMoving;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = slowDown;
        speed = 30;
        noise = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movement();
        mouseLook();
        jump();

    }

    void movement()
    {
        noise = 0;
        if (playerMovementController.forward)
        {

            rb.AddForce(transform.forward * speed, ForceMode.Force);
            noise = (transform.forward.magnitude * speed);
            moveForward = true;
        }
        //else
        //{
        //    rb.AddForce(-transform.forward * speed / 5);               //Extra braking in that direction, when nolonger moving that way? Less of a 'glide'? 
        //    moveForward = false;
        //}

        if (playerMovementController.back)
        {
            rb.AddForce(-transform.forward * speed, ForceMode.Force);
            noise = transform.forward.magnitude * speed / 3;
            moveBack = true;
        }
        //else
        //{
        //    rb.AddForce(transform.forward * speed / 5);               //same
        //    moveBack = false;
        //}

        if (playerMovementController.right)
        {
            rb.AddForce(transform.right * speed / 2, ForceMode.Force);
            noise = transform.right.magnitude * speed;
            moveRight = true;
        }
        //else
        //{
        //    rb.AddForce(-transform.right * speed / 5);               //same
        //    moveRight = false;
        //}

        if (playerMovementController.left)
        {
            rb.AddForce(-transform.right * speed / 2, ForceMode.Force);
            noise = transform.right.magnitude * speed;
            moveLeft = true;
        }
        //else
        //{
        //    rb.AddForce(transform.right * speed / 5);               //same
        //    moveLeft = false;
        //}

        if (moveLeft | moveRight | moveForward | moveBack)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }


    }

    void jump()
    {
        if (playerMovementController.jump && gravity.grounded)
        {
            rb.AddForce(transform.up * jumpForce , ForceMode.Force);
            Debug.Log("jump");
        }

    }

    void mouseLook()
    {
        transform.rotation = Quaternion.Euler(0, playerMovementController.yRot, 0);
    }
}
