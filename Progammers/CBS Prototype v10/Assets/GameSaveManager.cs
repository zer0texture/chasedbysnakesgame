
//#define USE_ENCRYPTION

using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System;
using System.Collections.Generic;
using System.Xml.Schema;
using System.Reflection;
using System.Security.Cryptography;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;


public class GameSaveLister : List<GameSaveManager.IGameSave>, IXmlSerializable
{
    public GameSaveLister() : base() { }


    public XmlSchema GetSchema() { return null; }


    public void ReadXml(XmlReader reader)
    {

        reader.ReadStartElement("GameSaveLister");
        while (reader.IsStartElement("IGameSave"))
        {
            Type type = Type.GetType(reader.GetAttribute("AssemblyQualifiedName"));
            XmlSerializer serial = new XmlSerializer(type);

            reader.ReadStartElement("IGameSave");
            this.Add((GameSaveManager.IGameSave)serial.Deserialize(reader));
            reader.ReadEndElement(); //IGameSave
        }
        reader.ReadEndElement(); //GameSaveLister

    }

    public void WriteXml(XmlWriter writer)
    {
        foreach (GameSaveManager.IGameSave save in this)
        {
            writer.WriteStartElement("IGameSave");
            writer.WriteAttributeString("AssemblyQualifiedName", save.GetType().AssemblyQualifiedName);
            XmlSerializer xmlSerializer = new XmlSerializer(save.GetType());
            xmlSerializer.Serialize(writer, save);
            writer.WriteEndElement();
        }
    }
}



public class GameSaveManager : MonoBehaviour
{

    [System.Serializable]
    public class SceneState : IGameSave
    {
        [XmlElement]
        public int m_SceneNo;
    }


    bool m_IsLoading = false;
    bool m_loadingDelay = false;

    //[System.Serializable]
    public class IGameSave
    {
        bool m_LoadedSuccessfully;
        public bool LoadedSuccessfully() { return m_LoadedSuccessfully; }

        public void Save()
        {
            GameSaveManager.m_Instance.AddSaveStruct(this);
        }
        public void Load()
        {
            IGameSave newsave = GameSaveManager.m_Instance.GetLoadedInfo(this.GetType());

            m_LoadedSuccessfully = false;

            if (newsave != null)
            {

                FieldInfo[] fields = this.GetType().GetFields(BindingFlags.Public |
                                                          BindingFlags.NonPublic |
                                                          BindingFlags.Instance);

                FieldInfo[] newfields = newsave.GetType().GetFields(BindingFlags.Public |
                                                          BindingFlags.NonPublic |
                                                          BindingFlags.Instance);


                for (int i = 0; i < fields.Length; i++)
                {
                    fields[i] = newfields[i];
                    fields[i].SetValue(this, newfields[i].GetValue(newsave));// = newfields[i];
                }

                m_LoadedSuccessfully = true;
            }

        }
    }

    public interface IGameSaver
    {
        void Save();
        void Load();
        void AddAsListener();
    }

    public void StartLoading()
    {
        m_loadingDelay = false;
        m_IsLoading = true;
    }
    public bool IsLoading()
    {
        return m_IsLoading;
    }

    static public GameSaveManager m_Instance;
    public string m_SaveFileName = "CBSSave.xml";
    GameSaveLister m_GameSave = new GameSaveLister();
    List<IGameSaver> m_SaveObjects = new List<IGameSaver>();


    byte[] m_EncryptionKey = { 63, 60, 63, 70, 63, 69, 58, 53 };
    byte[] m_EncryptionIV = { 12, 06, 38, 96, 31, 61, 04, 63 };


    // Use this for initialization
    void Start()
    {
        if (m_Instance != null && m_Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        m_Instance = this;

        LoadGame();
        SaveGame();
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_loadingDelay)
        {
            m_loadingDelay = true;
        }
        else
        {
            m_IsLoading = false;
        }
    }

    public void AddSaveListener(IGameSaver saver)
    {
        m_SaveObjects.Add(saver);
    }

    public void AddSaveStruct(IGameSave gameSave)
    {

        for (int i = 0; i < m_GameSave.Count; i++)
        {
            if (m_GameSave[i].GetType() == gameSave.GetType())
            {
                m_GameSave[i] = gameSave;
                //SaveGame();
                return;
            }
        }
        m_GameSave.Add(gameSave);


        //SaveGame();
    }

    public IGameSave GetLoadedInfo(Type gameSaveType)
    {
        LoadGame();
        foreach (IGameSave save in m_GameSave)
        {
            if (save.GetType() == gameSaveType)
                return save;
        }

        return null;

    }


    string GetFilePath()
    {
        return (Application.persistentDataPath + "/" + m_SaveFileName);
    }

    public bool CheckSave()
    {
        return (File.Exists(GetFilePath()));
    }

    public void SaveGame()
    {

        foreach (IGameSaver saver in m_SaveObjects)
        {
            if (saver == null)
                continue;
            Debug.Log("Saving structures first");
            saver.Save();
        }
        if (m_GameSave.Count > 0)
        {
            Debug.Log("Saving");

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Key = m_EncryptionKey;
            des.IV = m_EncryptionIV;

            using (Stream s = (Stream)File.Open(GetFilePath(), FileMode.Create))
#if USE_ENCRYPTION
            using (CryptoStream cs = new CryptoStream(s, des.CreateEncryptor(m_EncryptionKey, m_EncryptionIV), CryptoStreamMode.Write))
#endif 
            {


                XmlSerializer Serialisation = new XmlSerializer(m_GameSave.GetType());
#if USE_ENCRYPTION
                Serialisation.Serialize(cs, m_GameSave);
                cs.Close();
#else
                Serialisation.Serialize(s, m_GameSave);
                s.Close();
#endif
            }
        }

    }

    public void LoadGame()
    {
        if (CheckSave())
        {
            Debug.Log("Loading");

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Key = m_EncryptionKey;
            des.IV = m_EncryptionIV;

            using (Stream s = (Stream)File.Open(GetFilePath(), FileMode.Open))
#if USE_ENCRYPTION
            using (CryptoStream cs = new CryptoStream(s, des.CreateDecryptor(m_EncryptionKey, m_EncryptionIV), CryptoStreamMode.Read))
#endif
            {
                XmlSerializer serialzer = new XmlSerializer(typeof(GameSaveLister), new XmlRootAttribute("GameSaveLister"));
#if USE_ENCRYPTION
                m_GameSave = (GameSaveLister)serialzer.Deserialize(cs);
                cs.Close();
#else
                m_GameSave = (GameSaveLister)serialzer.Deserialize(s);
                s.Close();
#endif
            }
        }
    }

    void OnLevelWasLoaded(int level)
    {
        SaveGame();

        GameSaveManager.SceneState sceneSave = new GameSaveManager.SceneState();
        sceneSave.m_SceneNo = Application.loadedLevel;

        sceneSave.Save();
    }
}
