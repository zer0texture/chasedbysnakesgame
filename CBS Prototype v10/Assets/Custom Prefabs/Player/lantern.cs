using UnityEngine;
using System.Collections;
using System;

public class lantern : MonoBehaviour, GameSaveManager.IGameSaver {

    Light spotlight;
    float maxOil;
    float currentOil;
    float oilConsumption = 5.0f;

    public bool debugLantern = false;



    [System.Serializable]
    public class LanternSave : GameSaveManager.IGameSave
    {
        [System.Xml.Serialization.XmlElement]
        public float currentOil;
    };

    LanternSave lanternSave = null;

    // Use this for initialization
    void Start () 
    {
        AddAsListener();

        spotlight = GetComponentInChildren<Light>();
        maxOil = UISlider.GetSliderValue(UISlider.SliderType.OIL_MAX);
        currentOil = maxOil;
        UISlider.m_Bar_Oil = currentOil;
    }
	
	// Update is called once per frame
	void Update () 
    {
        lanternToggle();
        oilCounter();

        if (debugLantern)
        {
            maxOil = 100.0f;
            currentOil += 2.0f;
        }
        
        /*
         if(Application.loadedLevel == 2)
        {
            currentOil = maxOil;
        }
        Debug.Log(currentOil);
        */
	}

    void lanternToggle()
    {
        if (playerMovementController.lantern)
        {
            if (currentOil > 0) 
            {
                spotlight.enabled = true;
                spotlight.range = 15.0f;
            } 
        }
        else
        {
            spotlight.enabled = false;
            
        }
    }

    void oilCounter()
    {
        if (currentOil <= 0)
        {
            //spotlight.enabled = false;
            spotlight.range = 2.5f;
        }
        
        if (currentOil > 0)
        {
            if (spotlight.enabled)
            {
                    currentOil -= oilConsumption * Time.deltaTime;
            }
        }

        if (currentOil > maxOil)
        {
            currentOil = maxOil;
        }

        UISlider.m_Bar_Oil = currentOil;
    }

    public void Refill(float ammount = -1)
    {
        if (ammount == -1)
        {
            currentOil = maxOil;
        }
        else
        {
            currentOil += ammount;
            if (currentOil > maxOil)
                currentOil = maxOil;
        }

        UISlider.m_Bar_Oil = currentOil;
    }

    public void Save()
    {
        if (lanternSave == null)
            lanternSave = new LanternSave();

        lanternSave.currentOil = currentOil;

        lanternSave.Save();
    }

    public void Load()
    {
        if (lanternSave == null)
            lanternSave = new LanternSave();

        lanternSave.Load();

        if(lanternSave.LoadedSuccessfully())
        {
            currentOil = lanternSave.currentOil;
        }
        else
        {
            currentOil = maxOil;
        }
    }

    public void AddAsListener()
    {
        GameSaveManager.m_Instance.AddSaveListener(this);
    }
}
