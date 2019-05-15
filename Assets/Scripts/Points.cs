using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour
{
    private int points = 0;
    private float timeLeft = 30.0f;
    private bool finishedTime=false;
    void Start()
    {
        addPoint();
    }
    public void addPoint()
    {
        points++;
        Debug.Log("Points");
        GameObject.Find("HUD").GetComponentInChildren<Text>().text = "Points: " + points;
    }
    public void Update()
    {
        if (!finishedTime)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                GameObject.Find("HUD").GetComponentsInChildren<Text>()[1].text = "Time: " + Mathf.Round(timeLeft);
            }
            else
            {
                finishedTime = true;
                foreach (GameObject o in GameObject.FindGameObjectsWithTag("Point"))
                    Destroy(o);
            }
        }
    }

}
