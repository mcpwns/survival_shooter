using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class TimerScript : MonoBehaviour {
    float timer;
    string time;
    Text text;

    void Awake()
    {
        text = GetComponent <Text>();
    }
	void Update ()
    {
        timer += Time.deltaTime;
        //DateTime timez = time;
        time = FloattoTime(timer);
        text.text = "Timer: " + time;
    }

    string FloattoTime (float timez)
    {
        return string.Format("{0:#0}:{1:00}",
                     Mathf.Floor(timez / 60),//minutes
                     Mathf.Floor(timez) % 60);//seconds
    }
}
