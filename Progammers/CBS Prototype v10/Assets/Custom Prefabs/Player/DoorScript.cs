using UnityEngine;
using System.Collections;

public class DoorScript : buttonScript {

    public int m_ID = -1;

    protected override void TriggerAction()
    {
        PlayerInventory.Key newkey = new PlayerInventory.Key(m_ID, false);
        if (m_Player == null)
            return;
        PlayerController player = m_Player.GetComponent<PlayerController>();
        if (player)
        {
            if (player.GetInventory().HasKey(newkey))
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
