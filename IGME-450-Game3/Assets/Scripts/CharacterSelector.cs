using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum Character { Johnny, Jonathan };

public class CharacterSelector : MonoBehaviour {

    // Accessing the input manager to keep related code here
    // intended to keep the InputManager script cleaner
    public GameObject inputManager;

    // for managing which ui is visible
    public GameObject CharacterSelect;
    public GameObject InGame;

    // music
    public GameObject music;

    // background
    public GameObject background;

    // sprite arrays divided by character
    public Sprite[] JB;
    public Sprite[] JC;
	public Sprite[] LS; // Lobster

    // these arrays contain the images presenting the player on-screen
    public GameObject[] p1;
    public GameObject[] p2;

    // the actual player objects for storing the sprites at the end
    public GameObject[] player1;
    public GameObject[] player2;

    // for keeping track of which character is being represented
    private Character p1Character;
    private Character p2Character;

    // to make input taking quick
    private InputManager input;

    // for leaving character select
    private bool p1Confirm;
    private bool p2Confirm;

	// Use this for initialization
	void Start ()
    {
        p1Character = Character.Johnny;
        p2Character = Character.Jonathan;
        input = inputManager.GetComponent<InputManager>();
        p1Confirm = false;
        p2Confirm = false;
        background.GetComponent<SpriteRenderer>().color = new Color(138, 138, 138);
	}

    // Update is called once per frame
    void Update()
    {
        if (CharacterSelect.GetComponent<Canvas>().enabled)
        {
            // code for cycling the characters
            if (Input.GetKeyDown(input.p1Right))
            {
                switch (p1Character)
                {
                    case (Character.Johnny):
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                p1[i].GetComponent<Image>().sprite = JC[i];
                            }
                            p1Character++;
                            break;
                        }
                    case (Character.Jonathan):
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                p1[i].GetComponent<Image>().sprite = JB[i];
                            }
                            p1Character++;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                if (p1Character > Character.Jonathan) { p1Character = Character.Johnny; }
            }
            if (Input.GetKeyDown(input.p1Left))
            {
                switch (p1Character)
                {
                    case (Character.Johnny):
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                p1[i].GetComponent<Image>().sprite = JC[i];
                            }
                            p1Character--;
                            break;
                        }
                    case (Character.Jonathan):
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                p1[i].GetComponent<Image>().sprite = JB[i];
                            }
                            p1Character--;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                if (p1Character < Character.Johnny) { p1Character = Character.Jonathan; }
            }
            if (Input.GetKeyDown(input.p2Right))
            {
                switch (p2Character)
                {
                    case (Character.Johnny):
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                p2[i].GetComponent<Image>().sprite = JC[i];
                            }
                            p2Character++;
                            break;
                        }
                    case (Character.Jonathan):
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                p2[i].GetComponent<Image>().sprite = JB[i];
                            }
                            p2Character++;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                if (p2Character > Character.Jonathan) { p2Character = Character.Johnny; }
            }
            if (Input.GetKeyDown(input.p2Left))
            {
                switch (p2Character)
                {
                    case (Character.Johnny):
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                p2[i].GetComponent<Image>().sprite = JC[i];
                            }
                            p2Character--;
                            break;
                        }
                    case (Character.Jonathan):
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                p2[i].GetComponent<Image>().sprite = JB[i];
                            }
                            p2Character--;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                if (p2Character < Character.Johnny) { p2Character = Character.Jonathan; }
            }

            // code to leave character select
            if (Input.GetKeyDown(input.p1loArmAdd) || Input.GetKeyDown(input.p1loArmSub) || Input.GetKeyDown(input.p1upArmAdd) || Input.GetKeyDown(input.p1upArmSub))
            {
                p1Confirm = true;
            }
            if (Input.GetKeyDown(input.p2loArmAdd) || Input.GetKeyDown(input.p2loArmSub) || Input.GetKeyDown(input.p2upArmAdd) || Input.GetKeyDown(input.p2upArmSub))
            {
                p2Confirm = true;
            }
            if (p1Confirm && p2Confirm)
            {
                // deactivates character select and activates in-game ui
                CharacterSelect.GetComponent<Canvas>().enabled = false;
                InGame.GetComponent<Canvas>().enabled = true;

                for (int i = 0; i < 5; i++)
                {
                    player1[i].GetComponent<SpriteRenderer>().sprite = p1[i].GetComponent<Image>().sprite;
                    player2[i].GetComponent<SpriteRenderer>().sprite = p2[i].GetComponent<Image>().sprite;

                    player1[i].GetComponent<SpriteRenderer>().enabled = true;
                    player2[i].GetComponent<SpriteRenderer>().enabled = true;
                }

                background.GetComponent<SpriteRenderer>().color = Color.white;

                music.GetComponent<AudioSource>().Play();

                input.GameStart();                
            }
        }
    }
}
