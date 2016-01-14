using UnityEngine;
using System.Collections;

public class TrapFloor : MonoBehaviour {
    protected GameObject m_Player = null;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
    void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<PlayerController>().updatePlayerHp(-100);
    }
}
