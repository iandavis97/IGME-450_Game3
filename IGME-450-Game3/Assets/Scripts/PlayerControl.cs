using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Some notes on hinge joints
 * The limits on joints are relative to its start position, the green circle displayed in editor does not rotate to match it. Set all limits based on its starting alignment, negative degrees work just fine.
 * Joints dont support adding force directly so I'm working on a way to customize the rigidbodies' center of mass so that we have more control
 * I'm also working on a way to have friction and make the joints return to a starting orientation
 * I'm also working on a way to have the code get all of the refrences to various parts of the player so the editor isnt a mess
 * Left right movement isn't in this build but it should be pretty easy to just move the master player object for that
 */


public class PlayerControl : MonoBehaviour
{
    [Header("Debug")]
    public bool debug = true; //this is just to have an easy way to enable and disable a bunch of logging

    [Header("Body Parts")]
    //gameobjects that the script will get rigidbody, joint, and collider info from
    public GameObject head;
    public GameObject torso;
    public GameObject legs;
    public GameObject upperArm;
    public GameObject lowerArm;

    //refrences to joint objects
    private HingeJoint2D waistJoint;
    private HingeJoint2D neckJoint;
    private HingeJoint2D shoulderJoint;
    private HingeJoint2D elbowJoint;

    //refrences to rigidbodies
    private Rigidbody2D headRB;
    private Rigidbody2D torsoRB;
    private Rigidbody2D legsRB;
    private Rigidbody2D upperArmRB;
    private Rigidbody2D lowerArmRB;

    //refrences to colliders
    private BoxCollider2D headCollider;
    private BoxCollider2D torsoCollider;
    private BoxCollider2D legsCollider;
    private BoxCollider2D upperArmCollider;
    private BoxCollider2D lowerArmCollider;

    private bool decapitated = false;
    public AudioClip decapSFX;
    AudioSource sfx;
    // Use this for initialization
    void Start()
    {
        GetAllComponentReferences();
        sfx = GetComponent<AudioSource>();
    }

    //this method crawls through the public gameobject refrences and stores the rigidbodies, colliders, and joints for later use
    private void GetAllComponentReferences()
    {
        //get refrences to RBs
        headRB = GetRigidbodyReference(head);
        torsoRB = GetRigidbodyReference(torso);
        legsRB = GetRigidbodyReference(legs);
        upperArmRB = GetRigidbodyReference(upperArm);
        lowerArmRB = GetRigidbodyReference(lowerArm);

        //get refrences to colliders
        headCollider = GetColliderReference(head);
        torsoCollider = GetColliderReference(torso);
        legsCollider = GetColliderReference(legs);
        upperArmCollider = GetColliderReference(upperArm);
        lowerArmCollider = GetColliderReference(lowerArm);

        //get refrences to joints
        GetJointReference(head);
        GetJointReference(torso);
        GetJointReference(legs);
        GetJointReference(upperArm);
        GetJointReference(lowerArm);
    }

    //returns the first rigidbody2d attached to a gameobject
    private Rigidbody2D GetRigidbodyReference(GameObject GO)
    {
        if (debug) Debug.Log("Rigidbody2D found for " + GO.name);
        return GO.GetComponent<Rigidbody2D>();
    }

    //returns the first boxcollider2d attached to a gameobject
    private BoxCollider2D GetColliderReference(GameObject GO)
    {
        if (debug) Debug.Log("BoxCollider2d found for " + GO.name);
        return GO.GetComponent<BoxCollider2D>();
    }

    //assigns the join references
    private void GetJointReference(GameObject GO)
    {
        HingeJoint2D[] joints = GO.GetComponents<HingeJoint2D>();
        foreach (HingeJoint2D j in joints)
        {
            if (j.attachedRigidbody.name.ToLower() == "torso" && j.connectedBody.name.ToLower() == "head")
            {
                neckJoint = j;
                if (debug) Debug.Log("The " + neckJoint.attachedRigidbody.name + " bone is connected to the " + neckJoint.connectedBody.name + " bone");
            }
            if (j.attachedRigidbody.name.ToLower() == "legs" && j.connectedBody.name.ToLower() == "torso")
            {
                waistJoint = j;
                if (debug) Debug.Log("The " + waistJoint.attachedRigidbody.name + " bone is connected to the " + waistJoint.connectedBody.name + " bone");
            }
            if (j.attachedRigidbody.name.ToLower() == "torso" && j.connectedBody.name.ToLower() == "upper arm")
            {
                shoulderJoint = j;
                if (debug) Debug.Log("The " + shoulderJoint.attachedRigidbody.name + " bone is connected to the " + shoulderJoint.connectedBody.name + " bone");
            }
            if (j.attachedRigidbody.name.ToLower() == "upper arm" && j.connectedBody.name.ToLower() == "lower arm")
            {
                elbowJoint = j;
                if (debug) Debug.Log("The " + elbowJoint.attachedRigidbody.name + " bone is connected to the " + elbowJoint.connectedBody.name + " bone");
            }
        }
    }

    private void Update()
    {
        //if(Input.GetKey(KeyCode.LeftArrow)) { ControlUpperArm(5.0f); }
        //if(Input.GetKey(KeyCode.RightArrow)) { ControlUpperArm(-5.0f); }
        //if(Input.GetKey(KeyCode.UpArrow)) { ControlLowerArm(5.0f); }
        //if(Input.GetKey(KeyCode.DownArrow)) { ControlLowerArm(-5.0f); }

        // Upon decapitation, the joint automatically deletes itself as a component, so we're tracking this.
        if (neckJoint == null && !decapitated) {
        	Decapitate();
        }
    }

    // Plays a sound and flips on the boolean for decapitation.
    private void Decapitate() {
    	decapitated = true;
    	sfx.pitch = 1;
    	sfx.PlayOneShot(decapSFX);
    	headRB.GetComponent<Collider2D>().enabled = false;
    }

    // Adds torque to upper arm.
    // param[torque] - amount of torque to add.
    public void ControlUpperArm(float torque)
    {
        upperArmRB.AddTorque(torque);
    }

    // Adds torque to lower arm.
    // param[torque] - amount of torque to add.
    public void ControlLowerArm(float torque)
    {
        lowerArmRB.AddTorque(torque);
    }

    // Moves Left/Right
    public void Walk(float force)
    {
        legsRB.AddForce(new Vector2(force, 0),ForceMode2D.Impulse);
    }

    //this method is called whenever a body part impacts the other player
    //the object of the player doing the hitting = gameObject
    //the name of the player doing the hitting = gameObject.name
    //the object of the body part doing the hitting = bodyPart
    //the name of the bodypart doing the hitting = bodyPart.name

    //the object of the player being hit = collision.gameObject.transform.parent
    //the name of the player being hit = collision.gameObject.transform.parent.name
    //the object of the body being hit = collision.gameObject
    //the name of the bodypart being hit = collision.gameObject.name
    public void ChildCollisionEntered(Collision2D collision, GameObject bodyPart)
    {
        //this line just logs the collision to the console
        if (debug) Debug.Log(gameObject.name + "'s " + bodyPart.name + " has collided with " + collision.gameObject.transform.parent.name + "'s " + collision.gameObject.name);

        //this check is for determining which object is moving faster and will only run on the object impacting with more force
        if(GetImpactingObject(bodyPart,collision.gameObject) && !bodyPart.gameObject.CompareTag("Ground") && !collision.gameObject.CompareTag("Ground"))
        {
            //process collsion here, including calling any score adding methods

            //checking which player is getting hit, and then increase score of other player
            if (collision.gameObject.transform.parent.name == "Player Two")
            {
                //checking which body part hit
                if(collision.gameObject.name=="Torso")
                    Score.IncreaseP1Score(1);
                if (collision.gameObject.name == "Head")
                    Score.IncreaseP1Score(5);
            }
            if (collision.gameObject.transform.parent.name == "Player One")
            {
                //checking which body part hit
                if (collision.gameObject.name == "Torso")
                    Score.IncreaseP2Score(1);
                if (collision.gameObject.name == "Head")
                    Score.IncreaseP2Score(5);
            }
            StartCoroutine(collision.gameObject.GetComponent<CustomRigidbody>().flash()); // causes the hit body part to flash red, flash is a coroutine in Custom Rigidbody
            if(bodyPart.gameObject.CompareTag("Lower Arm"))
            {
                Vector2 impactForce = bodyPart.GetComponent<Rigidbody2D>().velocity * 7.0f;
                impactForce = Vector2.ClampMagnitude(impactForce, 70.0f);
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(impactForce, ForceMode2D.Impulse); //gives hits some oompf
            }
            //Debug.Log("calling flash on " + collision.gameObject.name);
        }

        //playing sfx when hit
        if (!sfx.isPlaying) {
        	sfx.pitch = Random.Range(0f, 1f);
            sfx.Play();
        }
    }

    //this method is to help collision resolution by determining which object is going faster, and is therefore the object doing the striking
    //returns true if the current gameobject is moving faster than the impacted gameobject
    private bool GetImpactingObject(GameObject current, GameObject target)
    {
        if (current.GetComponent<Rigidbody2D>().velocity.sqrMagnitude > target.GetComponent<Rigidbody2D>().velocity.sqrMagnitude)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

}
