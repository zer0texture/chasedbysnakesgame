using UnityEngine;
using System.Collections;

public class DialogLine
{
    public string m_Subtitles = null;
    public AudioSource m_Source = null;
    public AudioClip m_Line = null;
    public float m_Time = 0;

    public void Trigger(TextMessageHandler messageHandler = null)
    {
        if(messageHandler != null && m_Subtitles != null )
        {
            if (m_Line != null)
                m_Time = m_Line.length;
            if(m_Time > 0)
                messageHandler.Dialog(m_Subtitles, m_Time);
        }

        if (m_Source != null && m_Line != null)
        {
            m_Source.clip = m_Line;
            m_Source.Play();
        }
    }
}

public class StoryTrigger : MonoBehaviour
{
    public string m_Subtitles = null;
    public AudioSource m_Source = null;
    public AudioClip m_Line = null;
    public float m_Time = 0;

    public DialogLine m_Dialog;

    //public AudioClip m_Dialog;
    bool m_Interractable = true;


    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() && m_Interractable)
        {
            //other.GetComponent<AudioSource>().Play();
            m_Dialog = new DialogLine();
            m_Dialog.m_Line = m_Line;
            m_Dialog.m_Source = m_Source;
            m_Dialog.m_Subtitles = m_Subtitles;
            m_Dialog.m_Time = m_Time;

            m_Dialog.Trigger(other.GetComponent<PlayerController>().Screen.transform.GetChild(0).GetComponent<TextMessageHandler>());
            m_Interractable = false;
        }
    }
}