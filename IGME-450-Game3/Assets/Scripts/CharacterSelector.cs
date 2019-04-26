using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum Character { Johnny, Jonathan, Lobster, Fishman };

public class CharacterSelector : MonoBehaviour {

    // Accessing the input manager to keep related code here
    // intended to keep the InputManager script cleaner
    public GameObject inputManager;

    // for managing which ui is visible
    public GameObject CharacterSelect;
    public GameObject InGame;

    // music
    public GameObject music;

    // arrow animation
    public GameObject p1LeftArrow;
    public GameObject p1RightArrow;
    public GameObject p2LeftArrow;
    public GameObject p2RightArrow;
    private ArrowPulse p1LeftArrowAnimator;
    private ArrowPulse p2LeftArrowAnimator;
    private ArrowPulse p1RightArrowAnimator;
    private ArrowPulse p2RightArrowAnimator;

    // background
    public GameObject background;
    public GameObject chairs;
    public GameObject lights;

    // sprite arrays divided by character
    public Sprite[] JB; // Johnny Bravo
    public Sprite[] JC; // Jonathan Congratulations
	public Sprite[] LS; // Lobster
    public Sprite[] FM; // Fishman

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

    [SerializeField]
    private bool p1CanChange = true, p2CanChange = true;

    // for leaving character select
    private bool p1Confirm;
    private bool p2Confirm;

    public Text instructions;

	// Use this for initialization
	void Start ()
    {
        p1Character = Character.Johnny;
        p2Character = Character.Jonathan;
        input = inputManager.GetComponent<InputManager>();
        p1Confirm = false;
        p2Confirm = false;
        Color gray = new Color(138, 138, 138);
        background.GetComponent<SpriteRenderer>().color = gray;
        chairs.GetComponent<SpriteRenderer>().color = gray;
        lights.GetComponent<SpriteRenderer>().color = gray;
		p1CanChange = true;
		p2CanChange = true;

        p1LeftArrowAnimator = p1LeftArrow.GetComponent<ArrowPulse>();
        p1RightArrowAnimator = p1RightArrow.GetComponent<ArrowPulse>();
        p2LeftArrowAnimator = p2LeftArrow.GetComponent<ArrowPulse>();
        p2RightArrowAnimator = p2RightArrow.GetComponent<ArrowPulse>();

		Invoke("SetString", Time.deltaTime);
	}

	private void SetString() {
		instructions.text = input.GetSelectString();
	}

	private IEnumerator Cooldown(int player) {
		Debug.Log("Cooldown");
		if (player == 1) {
			p1CanChange = false;
		} else {
			p2CanChange = false;
		}
		yield return new WaitForSeconds(0.25f);
		p1CanChange = true;
		p2CanChange = true;
	}

    // Update is called once per frame
    void Update()
    {
        if (CharacterSelect.GetComponent<Canvas>().enabled)
        {
            // code for cycling the characters
            if (p1CanChange && (Input.GetKeyDown(input.p1Right) || input.p1leftRight == 1))
            {
                p1RightArrowAnimator.PulseArrow(); // animates the arrow to flash
				UISFX.instance.UISound(false); // Selection Sound

                switch (p1Character)
                {
                    case (Character.Johnny):
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                p1[i].GetComponent<Image>().sprite = JC[i];
                            }
                            p1Character++;
                            StartCoroutine(Cooldown(1));
                            break;
                        }
                    case (Character.Jonathan):
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                p1[i].GetComponent<Image>().sprite = LS[i];
                            }
                            p1Character++;
						    StartCoroutine(Cooldown(1));
                            break;
                        }
                    case (Character.Lobster):
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                p1[i].GetComponent<Image>().sprite = FM[i];
                            }
                            p1Character++;
					        StartCoroutine(Cooldown(1));
                            break;
                        }
                    case (Character.Fishman):
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                p1[i].GetComponent<Image>().sprite = JB[i];
                            }
                            p1Character++;
                            StartCoroutine(Cooldown(1));
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                if (p1Character > Character.Fishman) { p1Character = Character.Johnny; }
            }
			if (p1CanChange && (Input.GetKeyDown(input.p1Left) || input.p1leftRight == -1))
            {
                p1LeftArrowAnimator.PulseArrow(); // arrow flash
				UISFX.instance.UISound(false); // Selection Sound

                switch (p1Character)
                {
                    case (Character.Johnny):
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                p1[i].GetComponent<Image>().sprite = FM[i];
                            }
                            p1Character--;
						StartCoroutine(Cooldown(1));
                            break;
                        }
                    case (Character.Jonathan):
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                p1[i].GetComponent<Image>().sprite = JB[i];
                            }
                            p1Character--;
						StartCoroutine(Cooldown(1));
                            break;
                        }
                    case (Character.Lobster):
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                p1[i].GetComponent<Image>().sprite = JC[i];
                            }
                            p1Character--;
						StartCoroutine(Cooldown(1));
                            break;
                        }
                    case (Character.Fishman):
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                p1[i].GetComponent<Image>().sprite = LS[i];
                            }
                            p1Character--;
                            StartCoroutine(Cooldown(1));
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                if (p1Character < Character.Johnny) { p1Character = Character.Fishman; }
            }
			if (p2CanChange && (Input.GetKeyDown(input.p2Right) || input.p2leftRight == 1))
            {
                p2RightArrowAnimator.PulseArrow(); // arrow flash
				UISFX.instance.UISound(false); // Selection Sound

                switch (p2Character)
                {
                    case (Character.Johnny):
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                p2[i].GetComponent<Image>().sprite = JC[i];
                            }
                            p2Character++;
						StartCoroutine(Cooldown(2));
                            break;
                        }
                    case (Character.Jonathan):
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                p2[i].GetComponent<Image>().sprite = LS[i];
                            }
                            p2Character++;
						StartCoroutine(Cooldown(2));
                            break;
                        }
                    case (Character.Lobster):
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                p2[i].GetComponent<Image>().sprite = FM[i];
                            }
                            p2Character++;
						StartCoroutine(Cooldown(2));
                            break;
                        }
                    case (Character.Fishman):
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                p2[i].GetComponent<Image>().sprite = JB[i];
                            }
                            p2Character++;
                            StartCoroutine(Cooldown(1));
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                if (p2Character > Character.Fishman) { p2Character = Character.Johnny; }
            }
			if (p2CanChange && (Input.GetKeyDown(input.p2Left) || input.p2leftRight == -1))
            {
                p2LeftArrowAnimator.PulseArrow(); // arrow flash
				UISFX.instance.UISound(false); // Selection Sound

                switch (p2Character)
                {
                    case (Character.Johnny):
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                p2[i].GetComponent<Image>().sprite = FM[i];
                            }
                            p2Character--;
						StartCoroutine(Cooldown(2));
                            break;
                        }
                    case (Character.Jonathan):
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                p2[i].GetComponent<Image>().sprite = JB[i];
                            }
                            p2Character--;
						StartCoroutine(Cooldown(2));
                            break;
                        }
                    case (Character.Lobster):
                        {
                            for(int i = 0; i < 5; i++)
                            {
                                p2[i].GetComponent<Image>().sprite = JC[i];
                            }
                            p2Character--;
						StartCoroutine(Cooldown(2));
                            break;
                        }
                    case (Character.Fishman):
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                p2[i].GetComponent<Image>().sprite = LS[i];
                            }
                            p2Character--;
                            StartCoroutine(Cooldown(1));
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                if (p2Character < Character.Johnny) { p2Character = Character.Fishman; }
            }

            // allow the players to back out of a confirmed character
			if (p1Confirm && (Input.GetKeyDown(input.p1loArmSub) || Input.GetKeyDown(input.p1upArmSub) || input.p1loArmSubf >= 0.9f || input.p1upArmSubf >= 0.9f))
            {
                p1Confirm = false;
                p1CanChange = true;
                for (int i = 0; i < 5; i++)
                {
                    p1[i].GetComponent<Image>().color = Color.white;
                }
            }
			if (p2Confirm && (Input.GetKeyDown(input.p2loArmSub) || Input.GetKeyDown(input.p2upArmSub) || input.p2loArmSubf >= 0.9f || input.p2upArmSubf >= 0.9f))
            {
                p2Confirm = false;
                p2CanChange = true;
                for (int i = 0; i < 5; i++)
                {
                    p2[i].GetComponent<Image>().color = Color.white;
                }
            }

            // code to leave character select
            if (Input.GetKeyDown(input.p1loArmAdd) || Input.GetKeyDown(input.p1upArmAdd))
            {
				UISFX.instance.UISound(true); // Confirmation Sound
                p1Confirm = true;
                p1CanChange = false;
                for (int i = 0; i < 5; i++)
                {
                    p1[i].GetComponent<Image>().color = Color.gray;
                }
            }
			if (Input.GetKeyDown(input.p2loArmAdd) || Input.GetKeyDown(input.p2upArmAdd))
            {
				UISFX.instance.UISound(true); // Confirmation Sound
                p2Confirm = true;
                p2CanChange = false;
                for (int i = 0; i < 5; i++)
                {
                    p2[i].GetComponent<Image>().color = Color.gray;
                }
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
                chairs.GetComponent<SpriteRenderer>().color = Color.white;
                lights.GetComponent<SpriteRenderer>().color = Color.white;

                Jukebox.instance.Play();

                input.GameStart();                
            }
        }
    }
}
