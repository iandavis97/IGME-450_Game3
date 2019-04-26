using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPulse : MonoBehaviour {

    Animator Animator;
    bool started; // used to ensure that the Pressed is true for at least one frame

    // Use this for initialization
    void Start() {
        Animator = GetComponent<Animator>();
        started = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (started)
        {
            ResetBools();
        }
        if (Animator.GetBool("Pressed"))
        {
            started = true;
        }
        else
        {
            started = false;
        }
    }

    public void PulseArrow()
    {
        Animator.SetBool("Pressed", true);
    }

    public void ResetBools()
    {
        Animator.SetBool("Pressed", false);
    }
}
