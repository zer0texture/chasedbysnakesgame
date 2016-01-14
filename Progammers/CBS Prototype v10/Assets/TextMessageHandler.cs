using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextMessageHandler : MonoBehaviour {


    float m_PromptTimer;
    float m_DialogTimer;

    float m_PromptExpire;
    float m_DialogExpire;

    public void Prompt(string message, float time)
    {
        transform.GetChild(0).GetComponent<Text>().text = message;
        m_PromptExpire = time;
        m_PromptTimer = 0;
    }

    public void Dialog(string message, float time)
    {
        transform.GetChild(1).GetComponent<Text>().text = message;
        m_DialogExpire = time;
        m_DialogTimer = 0;
    }


    // Use this for initialization
    void Start () {

        transform.GetChild(0).GetComponent<Text>().text = "";
        transform.GetChild(1).GetComponent<Text>().text = "";

    }
	
	// Update is called once per frame
	void Update () {

        m_DialogTimer += Time.deltaTime;
        m_PromptTimer += Time.deltaTime;

        if (m_DialogTimer >= m_DialogExpire)
        {
            transform.GetChild(1).GetComponent<Text>().text = "";
        }
        if (m_PromptTimer >= m_PromptExpire)
        {
            transform.GetChild(0).GetComponent<Text>().text = "";
        }


    }
}
