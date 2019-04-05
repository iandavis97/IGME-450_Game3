using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * attach this script to the child objects to detecct their collisions in the parent script
 */

[RequireComponent(typeof(Rigidbody2D))]
public class CustomRigidbody : MonoBehaviour {

    public PlayerControl player;

	// Use this for initialization
	void Start () {
        if (!player) Debug.LogError(gameObject.name + "Does not have a player control obeject attached and will not register collisions");
	}
	
	// Update is called once per frame
	void Update () {}

    //this method passes the collision2d object as well as the gameobject the script is attached to, to the parent object
    //its purpose is only to pass this data and the parent, PlayerControl object will do all of the processing with it
    private void OnCollisionEnter2D(Collision2D collision)
    {
        player.ChildCollisionEntered(collision, gameObject);
    }
}
