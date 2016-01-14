using UnityEngine;
using System.Collections;

public class KeyScript : buttonScript {

    public int m_ID = -1;

    protected override void TriggerAction()
    {
        if (m_Player == null)
        {
            return;
        }

        if (m_ID > 0)
        {
            Debug.Log(m_Player.name);
            PlayerController player = m_Player.GetComponent<PlayerController>();
            PlayerInventory.Key newkey = new PlayerInventory.Key(m_ID, false);

            player.GetInventory();
            player.GetInventory().CollectKey(newkey);

            Destroy(gameObject);
        }
        
    }
}
