using UnityEngine;
using System.Collections;

public class InGameMenu : MonoBehaviour {
    private bool pauseEnabled;
    

    public bool isInGameMainMenu;
    public bool isInGameOptions;
    public bool isInGameOptionsAudio;

    Font pauseMenuFont; // change font/style - needs to be added as currently set to NULL

    void Start()
    {
        pauseEnabled = false;
        isInGameMainMenu = true;
        isInGameOptions = false;
        isInGameOptionsAudio = false;
        Time.timeScale = 1;  //??
        AudioListener.volume = 1; //??
        AudioListener.pause = false;
        Cursor.visible = false; // Hide Cursor
    }

    void Update()
    {
        //check if pause button (escape key) is pressed
        if (Input.GetKeyDown("escape"))
        {
            isInGameMainMenu = true;
            isInGameOptions = false;
            //check if game is already paused        
            if (pauseEnabled == true)
            {
                //unpause the game
                pauseEnabled = false;
                Time.timeScale = 1; //??
                AudioListener.volume = 1; //??
                AudioListener.pause = false;
                Cursor.visible = false;
            }

            //else if game isn't paused, then pause it
            else if (pauseEnabled == false)
            {
                pauseEnabled = true;
                AudioListener.volume = 0; //??
                AudioListener.pause = true;
                Time.timeScale = 0; //??
                Cursor.visible = true;
            }
        }
    }

    void OnGUI()
    {
        GUI.skin.box.font = pauseMenuFont; // ^see above at 'pauseMenuFont' definition - currently set to NULL
        GUI.skin.button.font = pauseMenuFont; // ^see above at 'pauseMenuFont' definition - currently set to NULL
        GUI.backgroundColor = Color.cyan; // colour for boxes
        GUI.contentColor = Color.green; // colour for text in boxes
        //GUI.color = Color.green; // colour for everything in GUI

        if (pauseEnabled == true)
        {
            if (isInGameMainMenu == true)
            {
                //Make a background box -- (in format of: (width initial position, height initial position, width scale, height scale), text to display) ..or maybe not..                         
                GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 150, 250, 300), "PAUSED");

                //Make Resume button
                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 250, 40), "Resume"))
                {
                    pauseEnabled = false;
                    Time.timeScale = 1;
                    AudioListener.volume = 1;
                    Cursor.visible = false;
                }

                //Make load button
                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 60, 250, 40), "Load Last Save"))
                {
                    //INSERT LAST SAVE POINT
                    /*
                    GameSaveManager.m_Instance.StartLoading();
                    GameSaveManager.SceneState save = new GameSaveManager.SceneState();
                    save.Load();
                    Application.LoadLevel(save.m_SceneNo);
                    */
                }

                //Make save button
                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 20, 250, 40), "Save Game"))
                {
                    //INSERT SAVE POINT
                    /*
                    GameSaveManager.m_Instance.SaveGame();
                    */
                }

                //Make Options button
                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 20, 250, 40), "Options"))
                {
                    isInGameOptions = true;
                    isInGameMainMenu = false;                 
                }
            
                //Make Main Menu button
                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 70, 250, 40), "Return to Main Menu"))
                {
                    Application.LoadLevel(0);
                }

                //Make quit game button
                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 110, 250, 40), "Quit to Windows"))
                {
                    Application.Quit(); // quits application
                }             
            }
                if(isInGameOptions == true)
                {                
                    //Make a background box -- (in format of: (width initial position, height initial position, width scale, height scale), text to display) ..or maybe not..                         
                    GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 150, 250, 300), "OPTIONS");

                    if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 250, 50), "Audio Settings"))
                    {
                        //INSERT SETTINGS HERE
                        isInGameOptionsAudio = true;
                        isInGameOptions = false;
                        AudioListener.volume = 0;
                    }
                    if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 250, 50), "Display Settings"))
                    {
                        //INSERT SETTINGS HERE
                    }
                    
                    if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 100, 250, 50), "Back"))
                    {
                        isInGameOptions = false;
                        isInGameMainMenu = true;
                    }
                }            

                if(isInGameOptionsAudio)
                {
                    GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 150, 250, 300), "AUDIO SETTINGS");

                    if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 100, 250, 50), "Back"))
                    {
                        isInGameOptionsAudio = false;
                        isInGameOptions = true;
                    }
                }
        }
    }
}

/* if (optionsDropDown == false)
                    {
                        optionsDropDown = true;
                    }
                    else
                    {
                        optionsDropDown = false;
                    }*/

// Options dropdown list
/* if (optionsDropDown == true)
 {
     if (GUI.Button(new Rect(Screen.width / 2 + 150, Screen.height / 2 - 50, 250, 50), "Slider 1"))
     {
         //NO INPUT ADDED YET
     }
     if (GUI.Button(new Rect(Screen.width / 2 + 150, Screen.height / 2, 250, 50), "Slider 2"))
     {
         //NO INPUT ADDED YET
     }
     if (GUI.Button(new Rect(Screen.width / 2 + 150, Screen.height / 2 + 50, 250, 50), "Slider 3"))
     {
         //NO INPUT ADDED YET
     }
     if (GUI.Button(new Rect(Screen.width / 2 + 150, Screen.height / 2 + 100, 250, 50), "Slider 4"))
     {
         //NO INPUT ADDED YET
     }
     if (GUI.Button(new Rect(Screen.width / 2 + 150, Screen.height / 2 + 150, 250, 50), "Slider 5"))
     {
         //NO INPUT ADDED YET
     }
     if (GUI.Button(new Rect(Screen.width / 2 + 150, Screen.height / 2 + 200, 250, 50), "Slider 6"))
     {
         //NO INPUT ADDED YET
     }

     if (Input.GetKeyDown("escape"))
     {
         optionsDropDown = false;
     }
 }*/
