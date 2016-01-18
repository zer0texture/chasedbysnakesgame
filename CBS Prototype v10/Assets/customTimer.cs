using UnityEngine;
using System.Collections;

public class customTimer : MonoBehaviour {

    public bool timerStarted = false;
    public float startTime = 0;
    public float currentTime = 0;

    void Start(){

    }
	void Update () {
	}

    public bool ourTimer(int inputTime)
    {
        if (!timerStarted)
        {
            startTime = Time.time;
            timerStarted = true;
           // print("timer started");
        }

        if (timerStarted)
        {
            currentTime = Time.time;
            if ((startTime + inputTime) <= currentTime)
            {
            //    print("timer done");
                timerStarted = false;
                return true;

            }
        }
        return false;
    }
}
