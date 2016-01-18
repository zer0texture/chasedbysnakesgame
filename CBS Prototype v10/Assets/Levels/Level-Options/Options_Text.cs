using UnityEngine;
using System.Collections;

public class Options_Text : MonoBehaviour {

    public bool isBack;
    void OnMouseUp()
    {
        if (isBack)
        {
            Application.LoadLevel(0);
            GetComponent<Renderer>().material.color = Color.green;
        }
    }
}
