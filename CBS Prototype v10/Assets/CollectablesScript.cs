using UnityEngine;
using System.Collections;

public class CollectablesScript : buttonScript
{

    public enum collectType { KEYS, TROPHIES, INFO}

    public int m_ID = -1;
    public float m_OffsetFromCamera = 1.2f;
    public collectType m_Type = collectType.INFO;
    enum InspectorState
    {
        NORMAL,
        INSPECTING
    }

    InspectorState m_State = InspectorState.NORMAL;

    void Update()
    {
        if (m_State == InspectorState.INSPECTING)
        {
            transform.rotation = Quaternion.Euler(playerMovementController.xRot, playerMovementController.yRot, 0.0f);

            if (playerMovementController.use && m_Interractable)
            {
                TriggerAction();
            }

            if (m_Interractable == false)
                m_Interractable = true;
        }
        else
        {
            
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>() && m_State == InspectorState.NORMAL)
        {
            m_Player = null;
        }
    }

    protected override void TriggerAction()
    {
        if (m_State == InspectorState.INSPECTING)
        {
            AddToInventory();         
        }
        else
        {
            CloseUp();
            saveOnAction();
        }
    }

    protected void CloseUp()
    {
        transform.parent = m_Player.GetComponent<PlayerController>().Screen.transform.GetChild(1);
        transform.localPosition = Vector3.forward * m_OffsetFromCamera;
        m_Interractable = false;
        m_State = InspectorState.INSPECTING;

        Debug.Log(m_Player.name);
        PlayerController player = m_Player.GetComponent<PlayerController>();
        PlayerInventory.Collect newcollect = new PlayerInventory.Collect(m_ID, m_Type);

        player.GetInventory();
        player.GetInventory().CollectObject(newcollect);
    }

    protected void AddToInventory()
    {
        Destroy(gameObject);
    }
}