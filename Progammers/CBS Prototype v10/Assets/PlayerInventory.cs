using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventory {

    [System.Serializable]
    [System.Xml.Serialization.XmlType("Key")]

    public class Key
    {
        public bool m_ForPerson;
        public int m_ID;

        public Key()
        {
            m_ForPerson = false;
            m_ID = -1;
        }

        public Key(int ID, bool forPerson)
        {
            m_ForPerson = forPerson;
            m_ID = ID;
        }
    }
    [System.Serializable]
    [System.Xml.Serialization.XmlType("Collect")]
    public class Collect
    {
        public bool m_ForPerson;
        public int m_ID;

        public Collect()
        {
            m_ForPerson = false;
            m_ID = -1;
        }

        public Collect(int ID, bool forPerson)
        {
            m_ForPerson = forPerson;
            m_ID = ID;
        }
    }

    [System.Serializable]
    public class InventorySave : GameSaveManager.IGameSave
    {
        [System.Xml.Serialization.XmlArray("Keys")]
        [System.Xml.Serialization.XmlArrayItem("Key")]
        public List<Key> m_Keys = new List<Key>();

        [System.Xml.Serialization.XmlArray("Collectables")]
        [System.Xml.Serialization.XmlArrayItem("Collect")]
        public List<Collect> m_Collectables = new List<Collect>();

    }

    InventorySave m_Save;

    

    public PlayerInventory()
    {
        Load();
        Save();
    }

    public void Save()
    {
        if (m_Save != null || m_Save.m_Keys != null || m_Save.m_Keys.Count > 0)
            m_Save.Save();
        if (m_Save != null || m_Save.m_Collectables != null || m_Save.m_Collectables.Count > 0)
            m_Save.Save();
    }

    public void Load()
    {
        m_Save = new InventorySave();
        m_Save.Load();
        if (m_Save == null || m_Save.m_Keys == null || m_Save.m_Keys.Count <= 0)
            m_Save = new InventorySave();
        if (m_Save == null || m_Save.m_Collectables == null || m_Save.m_Collectables.Count <= 0)
            m_Save = new InventorySave();
    }

    public void CollectKey(Key newkey)
    {
        foreach (Key key in m_Save.m_Keys)
        {
            if (key.m_ID == newkey.m_ID && key.m_ForPerson == newkey.m_ForPerson)
            {
                Debug.Log("Already had key");
                return;
            }
        }
        m_Save.m_Keys.Add(newkey);
    }

    public void CollectCollect(Collect newcollect)
    {
        foreach (Collect collect in m_Save.m_Collectables)
        {
            if (collect.m_ID == newcollect.m_ID && collect.m_ForPerson == newcollect.m_ForPerson)
            {
                Debug.Log("Already had collect");
                return;
            }
        }
        m_Save.m_Collectables.Add(newcollect);
    }

    public Key GetKey(int ID, bool forPerson)
    {
        foreach(Key key in m_Save.m_Keys)
        {
            if (key.m_ForPerson == forPerson && key.m_ID == ID)
                return key;
        }
        return null;
    }
    public Key GetKey(int Idx)
    {
        return m_Save.m_Keys[Idx];
    }
    public bool HasKey(int ID, bool forPerson)
    {
        foreach (Key key in m_Save.m_Keys)
        {
            if (key.m_ForPerson == forPerson && key.m_ID == ID)
                return true;
        }
        return false;
    }
    public bool HasKey(Key checkKey)
    {
        foreach (Key key in m_Save.m_Keys)
        {
            if (key.m_ForPerson == checkKey.m_ForPerson&& key.m_ID == checkKey.m_ID)
                return true;
        }
        return false;
    }

    public Collect Getcollect(int ID, bool forPerson)
    {
        foreach (Collect collect in m_Save.m_Collectables)
        {
            if (collect.m_ForPerson == forPerson && collect.m_ID == ID)
                return collect;
        }
        return null;
    }
    public Collect Getcollect(int Idx)
    {
        return m_Save.m_Collectables[Idx];
    }
    public bool Hascollect(int ID, bool forPerson)
    {
        foreach (Collect collect in m_Save.m_Collectables)
        {
            if (collect.m_ForPerson == forPerson && collect.m_ID == ID)
                return true;
        }
        return false;
    }
    public bool Hascollect(Collect checkcollect)
    {
        foreach (Collect collect in m_Save.m_Collectables)
        {
            if (collect.m_ForPerson == checkcollect.m_ForPerson && collect.m_ID == checkcollect.m_ID)
                return true;
        }
        return false;
    }
}
