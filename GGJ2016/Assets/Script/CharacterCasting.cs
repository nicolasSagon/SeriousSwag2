using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections.Generic;

public class CharacterCasting : MonoBehaviour {

	public bool isActive { get; set; }
	private Movement lastInput { get; set; }
	private Movement input{ get; set; }

	public enum Movement {UPLEFT = 0, DOWNLEFT = 1, UPRIGHT = 2, DOWNRIGHT = 3, DEFAULT = 4};
	public List<Movement> listMovementLeftArm;

	// Use this for initialization
	void Start () {
		isActive = false;
		listMovementLeftArm = new List<Movement> ();

	}
	

	private void initBool(){

		lastInput = Movement.DEFAULT;
		input = Movement.DEFAULT;

	}

	// Update is called once per frame
	void Update () {
		if (isActive) {
			getInput();
			if(lastInput != input && input != Movement.DEFAULT){
				listMovementLeftArm.Add(input);
				lastInput = input;
			}
		}
	}

	private void getInput(){

		float vertical = CrossPlatformInputManager.GetAxis ("Vertical");
		float horizontal = CrossPlatformInputManager.GetAxis ("Horizontal");

		if ((vertical < 1 && vertical > 0) && (horizontal > -1 && horizontal < 0)) {
			input = Movement.UPLEFT;
		} else if ((vertical < 1 && vertical > 0) && (horizontal < 1 && horizontal > 0)) {
			input = Movement.UPRIGHT;
		} else if (vertical > -1 && horizontal < -0.4) {
			input = Movement.DOWNLEFT;
		} else if (vertical < -0.4 && (horizontal < 1 && horizontal > 0)) {
			input = Movement.DOWNRIGHT;
		} else {
			input = Movement.DEFAULT;
		}

	}
}
