using UnityEngine;
using System.Collections;

public class CollectablesScript : buttonScript
{

    public int m_ID = -1;
    public float m_OffsetFromCamera = 1.2f;

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

    protected override void TriggerAction()
    {
        if (m_State == InspectorState.INSPECTING)
        {
            AddToInventory();
        }
        else
        {
            CloseUp();
        }
    }

    protected void CloseUp()
    {
        transform.parent = m_Player.GetComponent<PlayerController>().Screen.transform.GetChild(1);
        transform.localPosition = Vector3.forward * m_OffsetFromCamera;
        m_Interractable = false;
        m_State = InspectorState.INSPECTING;
    }

    protected void AddToInventory()
    {
        if (m_Player == null)
        {
            return;
        }

        if (m_ID > 0)
        {
            Debug.Log(m_Player.name);
            PlayerController player = m_Player.GetComponent<PlayerController>();
            PlayerInventory.Collect newcollect = new PlayerInventory.Collect(m_ID, false);

            player.GetInventory();
            player.GetInventory().CollectCollect(newcollect);

            Destroy(gameObject);
        }
    }
}