using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    void Update()
    {
        QuitApp();
    }

    void QuitApp() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            Debug.Log("We pushed Escape");
            Application.Quit();
        }
    }
}
