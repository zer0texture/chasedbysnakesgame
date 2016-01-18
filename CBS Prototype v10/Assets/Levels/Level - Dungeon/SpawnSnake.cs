using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpawnSnake : MonoBehaviour {
     //>
    public int maxSnakesView = (int)UISlider.GetSliderValue(UISlider.SliderType.NUM_OF_SNAKES);
    public int snakeCounter = 0;
    public int numSnakes = 2;
    public bool isTrap;
    //public GameObject guitext;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
    public Rigidbody BaseSnake; //>
    int i = 0;
	void Update () {
        if (isTrap)
        {
            spawnSnake(numSnakes);
        }
        else
        {
            spawnSnake();
        }

	}
    void spawnSnake(int numSnakes)
    {
        if (snakeCounter < numSnakes)
        {
            i++;
            if (i == 10)
            {
                Rigidbody clone;

                clone = Instantiate(BaseSnake, transform.position, transform.rotation) as Rigidbody;
                clone.name = "snake" + snakeCounter;
                if (PlayerSpawner.playerInst)
                    clone.GetComponent<MoveTo>().goal = PlayerSpawner.playerInst.transform;
                i = 0;
                snakeCounter++;
                /* Text text = guitext.GetComponent<Text>();
                text.text = snakeCounter.ToString();*/
            }
        }
    }

    void spawnSnake()
    {
        if (snakeCounter <= (int)UISlider.GetSliderValue(UISlider.SliderType.NUM_OF_SNAKES))
        {
            i++;
            if (i == 10)
            {
                Rigidbody clone;

                clone = Instantiate(BaseSnake, transform.position, transform.rotation) as Rigidbody;
                clone.name = "snake" + snakeCounter;
                if (PlayerSpawner.playerInst)
                    clone.GetComponent<MoveTo>().goal = PlayerSpawner.playerInst.transform;
                i = 0;
                snakeCounter++;
                /* Text text = guitext.GetComponent<Text>();
                text.text = snakeCounter.ToString();*/
            }
        }
    }
}
