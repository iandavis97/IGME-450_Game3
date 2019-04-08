using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score instance = null;
    public static int p1Score;
    public static int p2Score;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
	}
    // Update is called once per frame
    void Update ()
    {
		
	}
    //increases score by passed in value, should be dependent on body part hit or other factors
    public static void IncreaseP1Score(int value)
    {
       p1Score += value;
    }
    //increases score by passed in value, should be dependent on body part hit or other factors
    public static void IncreaseP2Score(int value)
    {
        p2Score += value;
    }
}
