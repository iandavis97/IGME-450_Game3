using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPulse : MonoBehaviour {

    Animator Animator;

    // Use this for initialization
    void Start() {
        Animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Animator.GetBool("Pressed"))
        {
            ResetBools();
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
