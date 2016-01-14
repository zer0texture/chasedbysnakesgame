using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour {
    public enum State {wander, Hunt, Attack}
    public State snakeAI = State.wander;
    // GLOBAL VARIABLES:

    Vector3 raycastUp;
    Vector3 raycastDown;
    Vector3 raycastLeft;
    Vector3 raycastRight;

    Vector3 originreset = new Vector3(0, 0, 1);

    public float noiseLevel = 0;
    public float playerDist = 0;
    public float WanderRadius = 100.0f;
    public float lookDist = 10.0f;

    public float startTime;
    public float currentTime;

    public float atkTime;
    public float atkStart;

    public int snakeDmg = -1;

    public bool hasGoal = false;
    public bool attackDebug;


    //TEMP VARIABLES
    float snakeFOV = 0.4f;

    





    public Transform goal;    
    RaycastHit hasHit;
    public Vector3 NewTarget;
    NavMeshAgent agent;
    Ray snakeRay;

    PlayerController pCont;
    public bool isAttacking = false;


	// Use this for initialization
	void Start () {
        snakeRay = new Ray(transform.position, transform.forward);
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        if(PlayerSpawner.playerInst)
            pCont = PlayerSpawner.playerInst.GetComponent<PlayerController>();
        
    
	}
	
	// Update is called once per frame 
	void Update () {
        //Debug.DrawRay(transform.position, transform.forward * lookDist, Color.green);
        raycastUp = transform.up * snakeFOV + transform.forward;
        raycastDown = -transform.up * snakeFOV + transform.forward;
        raycastRight = transform.right * snakeFOV + transform.forward;
        raycastLeft = -transform.right * snakeFOV + transform.forward;

        Debug.DrawRay(transform.position - originreset, raycastUp * lookDist, Color.blue);
        Debug.DrawRay(transform.position - originreset, raycastDown * lookDist, Color.green);
        Debug.DrawRay(transform.position - originreset, raycastLeft * lookDist, Color.yellow);
        Debug.DrawRay(transform.position - originreset, raycastRight * lookDist, Color.red);
        Debug.DrawRay(transform.position - originreset, transform.forward * lookDist, Color.black);

        Debug.DrawRay(transform.position - originreset, (raycastUp + raycastLeft) * (lookDist / 2), Color.blue);
        Debug.DrawRay(transform.position - originreset, (raycastUp + raycastRight) * (lookDist / 2), Color.green);
        Debug.DrawRay(transform.position - originreset, (raycastDown + raycastLeft) * (lookDist / 2), Color.yellow);
        Debug.DrawRay(transform.position - originreset, (raycastDown + raycastRight) * (lookDist / 2), Color.red);

        //OnTriggerEnter();
        switch (snakeAI)                                                            //State machine for snake AI
        {
            case State.wander:                                                      //If snake is in the 'Wander' state
                snakeWander();
                snakeGoalUpdate();

                break;
            case State.Hunt:
                snakeHunt();
                snakeGoalUpdate();

            break;
            case State.Attack:
                snakeAttack();
                snakeGoalUpdate();
               
            break;
        }
	}
    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.name == "Ethan")
            {
                Destroy(other.gameObject);
            }
        }

    bool canHearPlayer ()
    {
        //noiseLevel = playerMovement.rb.velocity.sqrMagnitude/5;
        noiseLevel = playerMovement.noise;
        if (goal == null)
            playerDist = noiseLevel + 1;
        else
            playerDist = Vector3.Distance(transform.position, goal.position);
        if (noiseLevel > playerDist)
        {
            //print("can hear player");
            return true;
        }
        else
        {
            //print("can't hear player");
            return false;
        }
           
    }

    bool canSeePlayer ()
    {
        if (Physics.Raycast(transform.position - originreset, raycastUp, out hasHit, lookDist) && (hasHit.collider.name == "loadedPlayer"))
        {

            print("can see player");
            return true;
        }

        else if (Physics.Raycast(transform.position - originreset, raycastDown, out hasHit, lookDist) && (hasHit.collider.name == "loadedPlayer"))
        {

            print("can see player");
            return true;
        }

        else if (Physics.Raycast(transform.position - originreset, raycastLeft, out hasHit, lookDist) && (hasHit.collider.name == "loadedPlayer"))
        {

            print("can see player");
            return true;
        }

        else if (Physics.Raycast(transform.position - originreset, raycastRight, out hasHit, lookDist) && (hasHit.collider.name == "loadedPlayer"))
        {

            print("can see player");
            return true;
        }



        if (Physics.Raycast(transform.position - originreset, (raycastUp + raycastLeft), out hasHit, (lookDist / 2)) && (hasHit.collider.name == "loadedPlayer"))
        {

            print("can see player");
            return true;
        }

        else if (Physics.Raycast(transform.position - originreset, (raycastUp + raycastRight), out hasHit, (lookDist / 2)) && (hasHit.collider.name == "loadedPlayer"))
        {

            print("can see player");
            return true;
        }

        else if (Physics.Raycast(transform.position - originreset, (raycastDown + raycastLeft), out hasHit, (lookDist / 2)) && (hasHit.collider.name == "loadedPlayer"))
        {

            print("can see player");
            return true;
        }

        else if (Physics.Raycast(transform.position - originreset, (raycastDown + raycastRight), out hasHit, (lookDist / 2)) && (hasHit.collider.name == "loadedPlayer"))
        {

            print("can see player");
            return true;
        }

        else if (Physics.Raycast(transform.position - originreset, transform.forward, out hasHit, lookDist) && (hasHit.collider.name == "loadedPlayer"))
        {

            print("can see player");
            return true;
        }

        else
        {
            print("Cant see player");
            return false;
        }
        
    }

    void snakeGoalUpdate()
    {
        if (goal == null && PlayerSpawner.playerInst != null)
        {
            goal = PlayerSpawner.playerInst.transform;
            pCont = goal.GetComponent<PlayerController>();
        }
        if (canSeePlayer())             //Can we see player?
        {
            snakeAI = State.Attack;
        }

        else if (canHearPlayer())       //Can't see player, can we hear them?
        { 
            snakeAI = State.Hunt;
        }

        else                            //Can't see or hear player, so wander around.
        {
            snakeAI = State.wander;
            
        }

    }
    void snakeWander()
    {
        if (hasGoal == false)      //Can't hear or see player, Wander around. 
        {
            NewTarget = Random.insideUnitSphere * WanderRadius;     //Generate a circle around the snake of radius 'WanderRadius' and choose a random new point in that circle
            NewTarget += transform.position;                        //Add new target to snakes current position  
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.destination = NewTarget;                          //Change target destination on navmesh to equal random point we just generated           
            startTime = Time.time;
            hasGoal = true;
        }
        currentTime = Time.time;
        if ((startTime + 5.0f) <= currentTime)
        {
            hasGoal = false;
        }

    }

    void snakeHunt()
    {
        print("Hunting Player");
        //NewTarget = goal.position;
        //NewTarget = new Vector3(goal.position.x + (Random.Range(-10.0f, 10.0f) * Vector3.Distance(transform.position, goal.position)), 
        //                        goal.position.y + (Random.Range(-10.0f, 10.0f) * Vector3.Distance(transform.position, goal.position)),
        //                        goal.position.z + (Random.Range(-10.0f, 10.0f) * Vector3.Distance(transform.position, goal.position)));
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        if (goal == null)
            return;
        agent.SetDestination(goal.position);


    }
    void snakeAttack()
    {
        if (goal == null)
        {
            isAttacking = false;
        }
        else
        {
            transform.LookAt(goal.position);                    //Look at our players position
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.destination = goal.position;                  //Set our path destination to the players position
            print("attacking player");

            if (playerDist > 5.0f)      //If ssnake is too far from target to attack, isattacking = false
            {
                isAttacking = false;
            }

            if ((playerDist < 4.0f) && (!isAttacking))  //If snake is near player and not attacking, start attacking
            {
                isAttacking = true;
                atkStart = Time.time;
            }
        }

        if (isAttacking)    //If snake is attacking, do attack and update player Hp every 3 seconds.
        {
            
            atkTime = Time.time;
            if ((atkStart + 1.0f) <= atkTime)
            {
                print("Attack!");
                pCont.updatePlayerHp(snakeDmg);
                //print(snakeDmg);
                //atkStart = Time.time;
                isAttacking = false;
            }
        }

        if (attackDebug == true)                            
        {
            startTime = currentTime;
        }
        if (canSeePlayer())                                    
        {
            //print("can see player");
            startTime = Time.time;
            currentTime = Time.time;
        }
        else
        {
            //print("cant see player");
            currentTime = Time.time;
            if ((startTime + 5.0f) <= currentTime)
            {
                hasGoal = false;
                //snakeAI = State.wander;
                //print("player lost");


            }
        }
    }
}




