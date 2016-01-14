    using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour, GameSaveManager.IGameSaver
{
    public float currentTime = 0.0f;
    public float startTime = 0.0f;

    public GameObject Screen;


    int playerHealth = 100;
    int MaxSnakes = 1;
    float lanternSize = 0.0f;

    public int pHealthVis = 0;
    public int mSnakesVis = 0;
    public float lantSizeVis = 0.0f;


    [System.Serializable]
    public class PlayerControllerSave : GameSaveManager.IGameSave
    {
        [System.Xml.Serialization.XmlElement]
        public int playerHealth;
        //[System.Xml.Serialization.XmlElement]
        //public float playerOil;
        [System.Xml.Serialization.XmlElement]
        public Vector3 playerPosition;
        [System.Xml.Serialization.XmlElement]
        public Quaternion playerRotation;
        [System.Xml.Serialization.XmlElement]
        public Vector3 playerScale;
    }

    PlayerControllerSave playerSave = null;
    PlayerInventory playerInv = null;

    // Use this for initialization
    void Start()
    {
        playerInv = new PlayerInventory();
        //PlayerInventory.Key newkey = new PlayerInventory.Key(12, false);

        AddAsListener();

        if (GameSaveManager.m_Instance.IsLoading())
            Load();
        Save();


        playerHealth = (int)UISlider.GetSliderValue(UISlider.SliderType.HEALTH);
        lanternSize = UISlider.GetSliderValue(UISlider.SliderType.LANTERN_SIZE);
        MaxSnakes = (int)UISlider.GetSliderValue(UISlider.SliderType.NUM_OF_SNAKES);
        UISlider.m_Bar_Health = playerHealth;

    }

    // Update is called once per frame
    void Update()
    {
        pHealthVis = playerHealth;
        //pOilVis = playerOil;
        mSnakesVis = MaxSnakes;
        lantSizeVis = lanternSize;
    }

    public void updatePlayerHp(int changeHP)
    {
        playerHealth += changeHP;
        UISlider.m_Bar_Health = playerHealth;
        if (playerHealth < 1)
        {
            Destroy(gameObject);
        }
    }

    void setSliders(int[] sliderArray)
    {
        playerHealth = sliderArray[1];
    }

    public PlayerInventory GetInventory()
    {
        if (playerInv == null)
            playerInv = new PlayerInventory();
        return playerInv;
    }

    public void Save()
    {
        if (playerSave == null)
            playerSave = new PlayerControllerSave();

        playerSave.playerHealth = playerHealth;
        //playerSave.playerOil = playerOil;
        playerSave.playerPosition = transform.position;
        playerSave.playerRotation = transform.rotation;
        playerSave.playerScale = transform.localScale;

        playerSave.Save();

        if (playerInv == null)
            playerInv = new PlayerInventory();
        playerInv.Save();

    }

    public void Load()
    {
        if (playerSave == null)
            playerSave = new PlayerControllerSave();
        playerSave.Load();

        playerHealth = playerSave.playerHealth;
        //playerOil = playerSave.playerOil;
        transform.position = playerSave.playerPosition;
        transform.rotation = playerSave.playerRotation;
        transform.localScale = playerSave.playerScale;


        if (playerInv == null)
            playerInv = new PlayerInventory();

        playerInv.Load();

    }

    public void AddAsListener()
    {
        GameSaveManager.m_Instance.AddSaveListener(this);
    }
}
