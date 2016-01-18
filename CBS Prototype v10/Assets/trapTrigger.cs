using UnityEngine;
using System.Collections;

public class trapTrigger : MonoBehaviour {

    public SpawnSnake snakeSpawning;
    public GameObject target = null;
    public float targetDist;
    public int numTrapSnakes;

	// Use this for initialization
	void Start () {
	snakeSpawning = GetComponent<SpawnSnake>();
    snakeSpawning.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
        if (target == null)
        {
            target = PlayerSpawner.playerInst;
        }
        if (playerInRange())
        {
            Debug.Log(playerInRange());
            snakeSpawning.enabled = true;
            snakeSpawning.isTrap = true;
        }
	}

    bool playerInRange()
    {
        if (target == null)
            return false;
        targetDist = Vector3.Distance(transform.position, target.transform.position);
        if (targetDist < 10.0f)
        {
            Debug.Log(targetDist);
            return true;
        }
        return false;
    }   
}
