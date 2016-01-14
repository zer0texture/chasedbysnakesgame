using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

static class LoadSave{

    public static playerSave.playerStats savedGame;

    public static void Save() {
        Debug.Log("Saving");
        savedGame = playerSave.GetCurrent();

        XmlSerializer Serialisation = new XmlSerializer(savedGame.GetType());
        StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/Save.xml");

        Serialisation.Serialize(sw, savedGame);
        Debug.Log(savedGame.ToString());
        sw.Close();
    }
    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/gameSaves.cbs"))
        {
            Debug.Log("Loading");
            BinaryFormatter bf = new BinaryFormatter();
            //FileStream file = new FileStream(Application.persistentDataPath + "/gameSaves.cbs", FileMode.Open);

            XmlSerializer serialzer = new XmlSerializer(typeof(playerSave.playerStats), new XmlRootAttribute("PlayerStats"));
            using (Stream s = (Stream)File.Open(Application.persistentDataPath + "/Save.xml", FileMode.Open))
            {
                savedGame = (playerSave.playerStats)serialzer.Deserialize(s);

            }


            /*
            FileStream file = File.Open(Application.persistentDataPath + "/gameSaves.cbs", FileMode.Open);
            savedGame = (playerSave.playerStats)bf.Deserialize(file);
            Debug.Log(savedGame.pPos);
             //savedGame[0].pPos;
             * */
            playerSave.Load(savedGame);
            //file.Close();

        }
    }
}

