using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections.Generic;

public enum Movement {UPLEFT = 0, DOWNLEFT = 1, UPRIGHT = 2, DOWNRIGHT = 3, DEFAULT = 4};

public class CharacterCasting : MonoBehaviour {

	public bool isActive { get; set; }
	public bool isLeft { get; set; }
	public bool isRight { get; set; }

	public float speed;

	private Movement lastInputLeft { get; set; }
	private Movement inputLeft{ get; set; }
	private Movement lastInputRight { get; set; }
	private Movement inputRight{ get; set; }


	public List<Movement> listMovementLeftArm;
	public List<Movement> listMovementRightArm;

	public GameObject rightArm;
	public GameObject leftArm;

	// Use this for initialization
	void Start () {
		isActive = false;
		listMovementLeftArm = new List<Movement> ();
		listMovementRightArm = new List<Movement> ();
		initInput ();
	}
	

	public void initInput(){

		lastInputLeft = Movement.DEFAULT;
		inputLeft = Movement.DEFAULT;
		lastInputRight = Movement.DEFAULT;
		inputRight = Movement.DEFAULT;
		listMovementLeftArm.Clear ();
		listMovementRightArm.Clear ();


	}

	// Update is called once per frame
	void Update () {
		if (isActive) {
			if(isLeft){
				getInputLeft();
				if(lastInputLeft != inputLeft && inputLeft != Movement.DEFAULT){
					listMovementLeftArm.Add(inputLeft);
					lastInputLeft = inputLeft;
				}
			}
			if(isRight){
				getInputRight();
				if(lastInputRight != inputRight && inputRight != Movement.DEFAULT){
					listMovementRightArm.Add(inputRight);
					lastInputRight = inputRight;
				}
			}
		}
	}

	private void getInputLeft(){

		float vertical = CrossPlatformInputManager.GetAxis ("Vertical");
		float horizontal = CrossPlatformInputManager.GetAxis ("Horizontal");

		leftArm.GetComponent<Rigidbody>().AddForce(new Vector3(horizontal*2, vertical*2,0) * speed, ForceMode.Force);

		if ((vertical < 1 && vertical > 0) && (horizontal > -1 && horizontal < 0)) {
			inputLeft = Movement.UPLEFT;
		} else if ((vertical < 1 && vertical > 0) && (horizontal < 1 && horizontal > 0)) {
			inputLeft = Movement.UPRIGHT;
		} else if ((vertical > -1 && vertical < 0) && (horizontal > -1 && horizontal < 0)) {
			inputLeft = Movement.DOWNLEFT;
		} else if ((vertical > -1 && vertical < 0) && (horizontal < 1 && horizontal > 0)) {
			inputLeft = Movement.DOWNRIGHT;
		} else {
			inputLeft = Movement.DEFAULT;
		}

	}

	private void getInputRight(){
		
		float vertical = CrossPlatformInputManager.GetAxis ("Mouse Y");
		float horizontal = CrossPlatformInputManager.GetAxis ("Mouse X");

		rightArm.GetComponent<Rigidbody>().AddForce(new Vector3(horizontal*2, vertical*2,0) * speed, ForceMode.Force);
		
		if ((vertical < 1 && vertical > 0) && (horizontal > -1 && horizontal < 0)) {
			inputRight = Movement.UPLEFT;
		} else if ((vertical < 1 && vertical > 0) && (horizontal < 1 && horizontal > 0)) {
			inputRight = Movement.UPRIGHT;
		} else if ((vertical > -1 && vertical < 0) && (horizontal > -1 && horizontal < 0)) {
			inputRight = Movement.DOWNLEFT;
		} else if ((vertical > -1 && vertical < 0) && (horizontal < 1 && horizontal > 0)) {
			inputRight = Movement.DOWNRIGHT;
		} else {
			inputRight = Movement.DEFAULT;
		}
		
	}

}
