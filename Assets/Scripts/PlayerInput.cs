using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour
{
    bool backPressed = false;

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Back))
            backPressed = true;

        if (backPressed)
        {
            backPressed = false;
            SceneManager.LoadScene("back", LoadSceneMode.Single);
        }

        if (OVRInput.GetUp(OVRInput.Button.Back))
            backPressed = false;
    }
}
