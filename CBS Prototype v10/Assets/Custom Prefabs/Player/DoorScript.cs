using UnityEngine;
using System.Collections;

public class DoorScript : buttonScript {

    public int m_ID = -1;

    protected override void TriggerAction()
    {
        PlayerInventory.Collect newkey = new PlayerInventory.Collect(m_ID, CollectablesScript.collectType.KEYS);
        if (m_Player == null)
            return;
        PlayerController player = m_Player.GetComponent<PlayerController>();
        if (player)
        {
            if (player.GetInventory().HasObject(newkey))
            {
                m_Interractable = false;
                Destroy(gameObject);

                /*
                bool isOpen = GetComponent<Animator>().GetBool("Open");
                GetComponent<Animator>().SetBool("Open", !isOpen);
                */
            }
            else
                Debug.Log("Key not aquired");
        }

    }
}
