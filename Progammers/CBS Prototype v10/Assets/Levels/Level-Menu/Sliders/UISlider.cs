using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UISlider : MonoBehaviour, GameSaveManager.IGameSaver {

    public Slider slider;


    static int   m_Slider_NumberOfSnakes;
    static float m_Slider_LanternSize;
    static int   m_Slider_Health;
    static float m_Slider_MaxOil;
    static public int   m_Bar_Health;
    static public float m_Bar_Oil;


    public enum SliderType
    {
        NUM_OF_SNAKES,
        LANTERN_SIZE,
        HEALTH,
        LENGTH_OF_TRAIL,
        OIL_MAX,

        HEALTH_BAR,
        OIL_BAR
    }


    public SliderType sliderType;

    private bool isBar = false;

    public bool IsBar() { return isBar; }


    static public float GetSliderValue(SliderType sliderType)
    {
        switch (sliderType)
        {
            case SliderType.NUM_OF_SNAKES:
                return m_Slider_NumberOfSnakes;
            case SliderType.LANTERN_SIZE:
                return m_Slider_LanternSize;
                break;
            case SliderType.HEALTH:
                return m_Slider_Health;
            case SliderType.OIL_MAX:
                return m_Slider_MaxOil;
            case SliderType.LENGTH_OF_TRAIL:
                break;
        }
        return -1;
    }


    [System.Serializable]
    public class SliderSave : GameSaveManager.IGameSave
    {
        [System.Xml.Serialization.XmlElement]
        public int snakesNum;
        [System.Xml.Serialization.XmlElement]
        public float lanternSize;
        [System.Xml.Serialization.XmlElement]
        public int playerHealth;
        [System.Xml.Serialization.XmlElement]
        public float oilMax;
    };
    
    static SliderSave sliderSave;
    
	// Use this for initialization
    void Start()
    {

        if (sliderType == SliderType.HEALTH_BAR ||
            sliderType == SliderType.OIL_BAR)
            isBar = true;

        //Load();
        //Save();
        AddAsListener();
        slider.onValueChanged.AddListener(OnValueCheck);  
	}
    void Update()
    {
        
        if (sliderType == SliderType.HEALTH_BAR)
        {
            slider.value = m_Bar_Health;
        }
        if (sliderType == SliderType.OIL_BAR)
        {
            slider.value = m_Bar_Oil;          
        }
        

    }
  
    public void OnValueCheck(float value)
    {
        switch(sliderType)
        {
            case SliderType.NUM_OF_SNAKES:
                m_Slider_NumberOfSnakes = (int)value;
                break;
            case SliderType.LANTERN_SIZE:
                m_Slider_LanternSize = value;
                break;
            case SliderType.HEALTH:
                m_Slider_Health = (int)value;
                break;
            case SliderType.OIL_MAX:
                m_Slider_MaxOil = value;
                break;
            case SliderType.LENGTH_OF_TRAIL:
                break;
        }

        Save();
    }

    
    public void Save()
    {
        if (sliderSave == null)
            sliderSave = new SliderSave();

        sliderSave.snakesNum = m_Slider_NumberOfSnakes;
        sliderSave.lanternSize = m_Slider_LanternSize;
        sliderSave.playerHealth = m_Slider_Health;
        sliderSave.oilMax = m_Slider_MaxOil;

        sliderSave.Save();
    }

    public void Load()
    {
        if (sliderSave == null)
            sliderSave = new SliderSave();
        sliderSave.Load();

        if (!sliderSave.LoadedSuccessfully())
        {
            m_Slider_NumberOfSnakes = 20;
            m_Slider_LanternSize = 50;
            m_Slider_Health = 100;
            m_Slider_MaxOil = 200;
        }
        else
        {
            m_Slider_NumberOfSnakes = sliderSave.snakesNum;
            m_Slider_LanternSize = sliderSave.lanternSize;
            m_Slider_Health = sliderSave.playerHealth;
            m_Slider_MaxOil = sliderSave.oilMax;

            switch (sliderType)
            {
                case SliderType.NUM_OF_SNAKES:
                    slider.value = sliderSave.snakesNum;
                    break;
                case SliderType.LANTERN_SIZE:
                    slider.value = sliderSave.lanternSize;
                    break;
                case SliderType.HEALTH:
                    slider.value = sliderSave.playerHealth;
                    break;
                case SliderType.OIL_MAX:
                    slider.value = sliderSave.oilMax;
                    break;
                case SliderType.LENGTH_OF_TRAIL:
                    break;
            }
        }
    }

    public void AddAsListener()
    {
        GameSaveManager.m_Instance.AddSaveListener(this);
    }    
}
