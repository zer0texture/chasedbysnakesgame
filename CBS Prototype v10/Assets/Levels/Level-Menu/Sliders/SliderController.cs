using UnityEngine;
using System.Collections;

public class SliderController : MonoBehaviour {

    public GameObject sliderGameObject;

	// Use this for initialization
	void Start () {
        sliderGameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (MainMenu.lastOption == MainMenu.MenuOption.MENU_OPTION)
        {
            //Debug.Log("its here");
            if (!sliderGameObject.activeSelf)
            {
                sliderGameObject.SetActive(true);
                foreach (UISlider slider in sliderGameObject.GetComponentsInChildren<UISlider>())
                {
                    slider.Load();
                    slider.Save();
                }
            }
        }
        if (MainMenu.lastOption == MainMenu.MenuOption.MENU_BACK)
        {
            
            sliderGameObject.SetActive(false);
        }
	}
}
