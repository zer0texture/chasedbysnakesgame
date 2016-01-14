using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

[System.Serializable]
public class playerSave : MonoBehaviour {
    static playerSave current;
    static bool gameloaded = false;

    [System.Serializable][XmlRoot("PlayerStats")]
    public struct playerStats
    {
        [XmlElement]
        public Vector3 pPos;
        [XmlElement]
        public float pHealth;
        [XmlElement]
        public float pOil;
        [XmlElement]
        public int sceneNo;
    }


    playerStats stats;

	// Use this for initialization
	void Start () {
        current = this;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.O))
        {
            stats.sceneNo = Application.loadedLevel;
            stats.pHealth = 100;
            stats.pOil = 100;
            stats.pPos = transform.position;

            LoadSave.Save();
            print("GameSaved");
            stats.pPos.x = 0;
            stats.pPos.y = 0;
            stats.pPos.z = 0;

        }
        if (Input.GetKeyUp(KeyCode.P))
        {
            LoadSave.Load();


            print("GameLoaded");
            //transform.position = pPos;
            if (Application.loadedLevel != stats.sceneNo)
            {
                Application.LoadLevel(stats.sceneNo);
            }
            
        }

        if (gameloaded)
        {
            transform.position = current.stats.pPos;
            gameloaded = false;
        }
	}

    public static void Load(playerStats saveGame)
    {
        current.stats = saveGame;
        gameloaded = true;
    }

    public static playerStats GetCurrent()
    {
        return current.stats;
    }
}
