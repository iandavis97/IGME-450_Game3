using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGenerator : MonoBehaviour
{
    // set up fields
    public GameObject targetPrefab;

    public GameObject upperArm;
    public GameObject lowerArm;

    private GameObject target;

    private CircleCollider2D targetCollider;
    private BoxCollider2D upperArmCollider;
    private BoxCollider2D lowerArmCollider;
    AudioSource sfx;

    // Use this for initialization
    void Start()
    {
        CreateTarget();

        // assign values to the colliders for use lateer
        targetCollider = target.GetComponent<CircleCollider2D>();
        upperArmCollider = upperArm.GetComponent<BoxCollider2D>();
        lowerArmCollider = lowerArm.GetComponent<BoxCollider2D>();

        //setting up sound effects
        sfx = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (targetCollider.IsTouching(upperArmCollider))
        {
            // destroy target on impact
            Destroy(target);

            //play paper hit sound effect
            if (sfx.isPlaying == false)
                sfx.Play();
            // make a new target and reassign targetCollider
            CreateTarget();
            targetCollider = target.GetComponent<CircleCollider2D>();
        }
        if (targetCollider.IsTouching(lowerArmCollider))
        {
            // make this proc on both arms just in case
            Destroy(target);

            //play paper hit sound effect
            if (sfx.isPlaying == false)
                sfx.Play();
            CreateTarget();
            targetCollider = target.GetComponent<CircleCollider2D>();
        }

    }

    // Creates a target randomly within the space of the stage
    void CreateTarget()
    {
        target = Instantiate(targetPrefab, new Vector2(Random.Range(-4f, 4f), Random.Range(-.5f, 2f)), Quaternion.identity);
    }
}
