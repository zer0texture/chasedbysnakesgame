using UnityEngine;
using System.Collections;

public class buttonScript : MonoBehaviour {

    public int levelToLoad;

    /*
    public GameObject text;
    public GameObject model;
    public GameObject playerCamera;
    public Color lerpedColor = Color.red;
    public float startTime;
    public float lerpRate;
    float percentage = 0.0f
    */

    protected bool m_Interractable = true;
    protected GameObject m_Player = null;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            m_Player = other.gameObject;
        }
    }

    void OnTriggerStay(Collider other)
    {

        if (playerMovementController.use && m_Interractable)
        {
            TriggerAction();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            m_Player = null;
        }
    }

    protected virtual void TriggerAction()
    {
        if (levelToLoad > 0)
        {
            saveOnAction();
            Application.LoadLevel(levelToLoad);
        }

    }

    public void ToggleInterractable()
    {
        m_Interractable = !m_Interractable;
    }

    protected void saveOnAction()
    {
        GameSaveManager.SceneState sceneSave = new GameSaveManager.SceneState();
        sceneSave.m_SceneNo = Application.loadedLevel;
        sceneSave.Save();
        GameSaveManager.m_Instance.SaveGame(); 
    }

}
