using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    void Update()
    {
        //GetKey vaneko chai thichi rah xa vane true hunxa
        //GetKeyDown vaneko chai ek choti thice paxi pugxa
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit(); // Quits the application when the Escape key is pressed.
        }
    }
}
