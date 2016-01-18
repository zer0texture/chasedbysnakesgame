using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    /*
    public bool mainMenuContinue;
    public bool mainMenuNewGame;
    public bool mainMenuOptions;
    public bool mainMenuCredits;
    public bool mainMenuExit;
    

    public bool optionsBack;
     * */

    static public MenuOption lastOption;

    public enum MenuOption
    {
        MENU_CONTINUE,
        MENU_NEW_GAME,
        MENU_OPTION,
        MENU_CREDITS,
        MENU_QUIT,
        MENU_BACK
    }

    public MenuOption menuOption;

    public static bool staticMainMenuOptions;
    public static bool staticOptionsBack;

    public GameObject mainMenuText;
    public GameObject optionsText;

    void Start()
    {
        if (optionsText)
        {
            foreach (UISlider slider in optionsText.GetComponentsInChildren<UISlider>())
            //UISlider slider = optionsText.GetComponentInChildren<UISlider>();
            {
                slider.Load();
                slider.Save();
                break;
            }
            optionsText.SetActive(false);
        }
        Cursor.visible = true; // Show Cursor
    }

    void Update()
    {
        //staticMainMenuOptions = mainMenuOptions;
        //staticOptionsBack = optionsBack;
    }

    void OnMouseUp()
    {
        // MAIN MENU

        if (menuOption == MenuOption.MENU_CONTINUE)
        {
            //Application.LoadLevel(LastCheckpoint);
            GetComponent<Renderer>().material.color = Color.green;
            lastOption = menuOption;
        }
        if (menuOption == MenuOption.MENU_NEW_GAME)
        {
            Application.LoadLevel(1);
            GetComponent<Renderer>().material.color = Color.green;
            lastOption = menuOption;
        }

        if (menuOption == MenuOption.MENU_OPTION)
        {
            // Application.LoadLevel(2);
            mainMenuText.SetActive(false);
            optionsText.SetActive(true);
            GetComponent<Renderer>().material.color = Color.green;
            lastOption = menuOption;
        }

        if (menuOption == MenuOption.MENU_CREDITS)
        {
            // Application.LoadLevel(3);
            GetComponent<Renderer>().material.color = Color.green;
            lastOption = menuOption;
        }

        if (menuOption == MenuOption.MENU_QUIT)
        {
            Application.Quit();
            GetComponent<Renderer>().material.color = Color.green;
            lastOption = menuOption;
        }
        /////////////////////////////////////////////////////////////

        // OPTIONS


        if (menuOption == MenuOption.MENU_BACK)
        {
            optionsText.SetActive(false);
            mainMenuText.SetActive(true);
            GetComponent<Renderer>().material.color = Color.green;
            lastOption = menuOption;
        }
    }
}

/*using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public bool isContinue;
    public bool isNewGame;
    public bool isOptions;
    public bool isCredits;
    public bool isExit;

    void Start()
    {
        Cursor.visible = true; // Show Cursor
    }

    void OnMouseUp()
    {
        if (isContinue)
        {
           //Application.LoadLevel(LastCheckpoint);
            GetComponent<Renderer>().material.color = Color.green;
        }
        if (isNewGame)
        {
            Application.LoadLevel(1);
            GetComponent<Renderer>().material.color = Color.green;
        }

        if (isOptions)
        {
            Application.LoadLevel(2);
            GetComponent<Renderer>().material.color = Color.green;
        }

        if (isCredits)
        {
            Application.LoadLevel(3);
            GetComponent<Renderer>().material.color = Color.green;
        }

        if (isExit)
        {
            Application.Quit();
            GetComponent<Renderer>().material.color = Color.green;
        }
    } 
}*/
