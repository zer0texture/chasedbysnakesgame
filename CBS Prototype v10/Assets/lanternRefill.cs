using UnityEngine;
using System.Collections;

public class lanternRefill : buttonScript 
{
    public int numberUses = 10;
    protected override void TriggerAction()
    {

        if (m_Interractable)
        {
            //PlayerController.playerOil = lantern.maxOil;
            m_Player.GetComponent<lantern>().Refill();
            if (!(numberUses > 0))
            {
                m_Interractable = false;
            }
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.red;
        }

    }

}
