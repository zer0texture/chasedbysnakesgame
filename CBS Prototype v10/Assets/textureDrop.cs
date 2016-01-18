using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class textureDrop : MonoBehaviour
{
    public enum TextureType { FOOTPRINT, HANDPRINT, BLOOD }
    public TextureType m_textType = TextureType.FOOTPRINT;

    public GameObject m_footprintPrefab;
    //public List<GameObject> m_fpList = new List<GameObject>();

    bool m_RightFootPrint = true;
    int m_footprintIndex = 0;

    bool timerStarted;
    float startTime;
    float currentTime;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (we die)
        {
            m_textType = TextureType.BLOOD;
        }
        if (Hand on wall)
        {
            m_textType = TextureType.HANDPRINT
        }
        if (Walking)
        {
            m_textType = TextureType.FOOTPRINT
        }
         * */
        dropTexture();

    }

    void dropTexture()
    {
        switch (m_textType)
        {
            case (TextureType.BLOOD):

                //On 'Death' trigger, drop blood

                break;
            case (TextureType.HANDPRINT):
                break;
            case (TextureType.FOOTPRINT):
                if (gravity.grounded)
                {
                    GameObject newFootprint;
                    if (m_RightFootPrint)
                    {
                        Debug.Log("Right Print");
                        newFootprint = Instantiate(m_footprintPrefab, transform.position, transform.rotation) as GameObject;

                    }
                    else
                    {
                        Debug.Log("Left Print");
                        newFootprint = Instantiate(m_footprintPrefab, transform.position, transform.rotation) as GameObject;

                    }
                    newFootprint.name = "footprint " + m_footprintIndex;
                    m_RightFootPrint = (!m_RightFootPrint);
                    m_footprintIndex++;
                }
                break;
            default:
                break;
            //IF NOT MOVING, SET 'm_RightFootPrint' TO TRUE

        }
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
