using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class CharacterMaster : MonoBehaviour {

	private FirstPersonController fpsScript;
	private CharacterCasting castingScript;
	public Text text;
	private bool isCasting;
	private bool rightCasting;
	private bool leftCasting;
	private bool lastCastingVal;

	// Use this for initialization
	void Start () {
		this.isCasting = false;
		this.lastCastingVal = true;
		fpsScript = GetComponent<FirstPersonController> ();
		castingScript = GetComponent<CharacterCasting> ();
	}
	
	// Update is called once per frame
	void Update () {
		getInput ();

		getTriggerPressed ();
	}

	private void getTriggerPressed(){
		
		float trigger1 = CrossPlatformInputManager.GetAxis ("Fire1");
		float trigger2 = CrossPlatformInputManager.GetAxis ("Fire2");

		leftCasting = trigger1 == 1;
		rightCasting = trigger2 == 1;

		castingScript.isLeft = leftCasting;
		castingScript.isRight = rightCasting;
		
		isCasting = leftCasting || rightCasting;
		
		if (lastCastingVal != isCasting) {
			setTextVisibility();
			fpsScript.isActive = !isCasting;
			castingScript.isActive = isCasting;
			if(lastCastingVal == true && isCasting == false){
				string movementLeft = "Left : ";
				foreach(CharacterCasting.Movement movementId in castingScript.listMovementLeftArm){
					movementLeft += movementId + " ";
				}
				if(movementLeft != "Left : ")
					Debug.Log (movementLeft);

				string movementRight = "Right : ";
				foreach(CharacterCasting.Movement movementId in castingScript.listMovementRightArm){
					movementRight += movementId + " ";
				}
				if(movementRight != "Right : ")
					Debug.Log (movementRight);
				castingScript.initInput();
			}
			lastCastingVal = isCasting;
		}
		
	}

	private void setTextVisibility(){
		this.text.enabled = isCasting;
	}

	private void getInput(){
		
		float vertical = CrossPlatformInputManager.GetAxis ("Vertical");
		float horizontal = CrossPlatformInputManager.GetAxis ("Horizontal");

		this.text.text = "v : " + vertical + ", h : " + horizontal;
		
	}

}
