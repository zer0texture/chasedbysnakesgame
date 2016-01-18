using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventory {
    /*
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
     * */

    [System.Serializable]
    [System.Xml.Serialization.XmlType("Collect")]
    public class Collect
    {
        //public bool m_ForPerson;
        public int m_ID;
        public CollectablesScript.collectType m_Type;

        public Collect()
        {
            //m_ForPerson = false;
            m_Type = CollectablesScript.collectType.INFO;
            m_ID = -1;
        }

        public Collect(int ID, CollectablesScript.collectType type)
        {
            //m_ForPerson = forPerson;
            m_Type = type;
            m_ID = ID;
        }
    }

    [System.Serializable]
    public class InventorySave : GameSaveManager.IGameSave
    {
        /*
        [System.Xml.Serialization.XmlArray("Keys")]
        [System.Xml.Serialization.XmlArrayItem("Key")]
        public List<Key> m_Keys = new List<Key>();
        */

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
        /*
        if (m_Save != null || m_Save.m_Keys != null || m_Save.m_Keys.Count > 0)
            m_Save.Save();
         * */
        if (m_Save != null || m_Save.m_Collectables != null || m_Save.m_Collectables.Count > 0)
            m_Save.Save();
    }

    public void Load()
    {
        m_Save = new InventorySave();
        m_Save.Load();
        /*
        if (m_Save == null || m_Save.m_Keys == null || m_Save.m_Keys.Count <= 0)
            m_Save = new InventorySave();
         * */
        if (m_Save == null || m_Save.m_Collectables == null || m_Save.m_Collectables.Count <= 0)
            m_Save = new InventorySave();
    }

    public void CollectObject(Collect newObj)
    {
        foreach (Collect obj in m_Save.m_Collectables)
        {
            if (obj.m_ID == newObj.m_ID && obj.m_Type == newObj.m_Type)
            {
                Debug.Log("Already had object");
                return;
            }
        }
        m_Save.m_Collectables.Add(newObj);
    }
    /*
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
    */
    public Collect GetCollectable(int ID, CollectablesScript.collectType type)
    {
        foreach (Collect obj in m_Save.m_Collectables)
        {
            if (obj.m_Type == type && obj.m_ID == ID)
                return obj;
        }
        return null;
    }
    public Collect GetObject(int Idx)
    {
        return m_Save.m_Collectables[Idx];
    }
    public bool HasObject(int ID, CollectablesScript.collectType type)
    {
        foreach (Collect obj in m_Save.m_Collectables)
        {
            if (obj.m_Type == type && obj.m_ID == ID)
                return true;
        }
        return false;
    }
    public bool HasObject(Collect checkObj)
    {
        foreach (Collect obj in m_Save.m_Collectables)
        {
            if (obj.m_Type == checkObj.m_Type && obj.m_ID == checkObj.m_ID)
                return true;
        }
        return false;
    }

}
