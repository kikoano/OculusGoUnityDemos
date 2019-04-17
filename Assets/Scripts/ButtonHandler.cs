using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    private void Awake()
    {
        Text about = transform.Find("AboutText").GetComponent<Text>();
        about.gameObject.SetActive(false);
    }
    public void PlayTeleportation()
    {
        SceneManager.LoadScene("TeleportDemo");
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("test");
    }
    public void ShowAbout()
    {
        Text about = transform.Find("AboutText").GetComponent<Text>();
        about.gameObject.SetActive(true);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
