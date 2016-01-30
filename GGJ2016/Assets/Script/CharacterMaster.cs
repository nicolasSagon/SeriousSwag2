using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using System.Collections.Generic;

public class CharacterMaster : MonoBehaviour {

	private FirstPersonController fpsScript;
	private CharacterCasting castingScript;
	public Text text;
	private bool isCasting;
	private bool lastCastingVal;
	public enum Spell {WIND = 0, FIRE = 1, WATER = 2, DEATH = 3, BLACKMAMBA = 4, SMALL = 5, CHICKEN = 6, SQUIRREL = 7, ENEMY = 8, DARKNESS = 9, LIGHT = 10, RANDOM = 99};
	public enum Movement {UPLEFT = 0, DOWNLEFT = 1, UPRIGHT = 2, DOWNRIGHT = 3, DEFAULT = 4};

	private RaycastHit hit;
	public Camera camera;

	// Use this for initialization
	void Start () {
		this.isCasting = false;
		this.lastCastingVal = true;
		fpsScript = GetComponent<FirstPersonController> ();
		castingScript = GetComponent<CharacterCasting> ();
		List<Movement> listMovementLeftArm = new List<Movement> ();
		List<Movement> listMovementRightArm = new List<Movement> ();
		listMovementLeftArm.Add (Movement.UPRIGHT);
		listMovementLeftArm.Add (Movement.UPLEFT);
		listMovementLeftArm.Add (Movement.DOWNLEFT);
		Debug.Log(getSpell(listMovementLeftArm, listMovementRightArm));
	}
	
	// Update is called once per frame
	void Update () {
		Ray landingRay = camera.ScreenPointToRay (Input.mousePosition);
		Debug.DrawRay (landingRay.origin,landingRay.direction*5, Color.red);
		getInput ();
		getTriggerPressed ();
	}

	private void getTriggerPressed(){
		
		float trigger1 = CrossPlatformInputManager.GetAxis ("Fire1");
		float trigger2 = CrossPlatformInputManager.GetAxis ("Fire2");
		
		if (trigger1 == 1 && trigger2 == 1) {
			this.isCasting = true;
		} else {
			this.isCasting = false;
		}
		
		if (lastCastingVal != isCasting) {
			setTextVisibility();
			fpsScript.isActive = !isCasting;
			castingScript.isActive = isCasting;
			if(lastCastingVal == true && isCasting == false){
				foreach(int movementId in castingScript.listMovementLeftArm){
					Debug.Log("Move : " + movementId);
				}
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

	private Spell getSpell(List<Movement> listMovementLeftArm, List<Movement> listMovementRightArm){
		
		Spell spell = Spell.RANDOM;
		int leftArmMovements = listMovementLeftArm.Count;
		int rightArmMovements = listMovementRightArm.Count;
		
		// more than 10 inputs
		if ((leftArmMovements + rightArmMovements) >= 11)
			spell = Spell.SQUIRREL;
		else if (leftArmMovements == 4 && rightArmMovements == 4) {
			if (listMovementLeftArm [0] == Movement.UPLEFT &&
			    listMovementLeftArm [1] == Movement.DOWNLEFT &&
			    listMovementLeftArm [2] == Movement.UPLEFT &&
			    listMovementLeftArm [3] == Movement.DOWNLEFT &&
			    listMovementRightArm [0] == Movement.DOWNRIGHT &&
			    listMovementRightArm [1] == Movement.UPRIGHT &&
			    listMovementRightArm [2] == Movement.DOWNRIGHT &&
			    listMovementRightArm [3] == Movement.UPRIGHT)
				spell = Spell.WIND;
		} else if (leftArmMovements == 2 && rightArmMovements == 2) {
			if (listMovementLeftArm [0] == Movement.UPLEFT &&
			    listMovementLeftArm [1] == Movement.DOWNLEFT &&
			    listMovementRightArm [0] == Movement.UPRIGHT &&
			    listMovementRightArm [1] == Movement.DOWNRIGHT)
				spell = Spell.WATER;
		} else if (leftArmMovements == 1 && rightArmMovements == 0) {
			if (listMovementLeftArm [0] == Movement.UPLEFT)
				spell = Spell.FIRE;
		} else if (leftArmMovements == 0 && rightArmMovements == 1) {
			if (listMovementRightArm [0] == Movement.UPRIGHT)
				spell = Spell.FIRE;
		} else if (leftArmMovements == 1 && rightArmMovements == 1) {
			if (listMovementRightArm [0] == Movement.UPLEFT &&
			    listMovementLeftArm [0] == Movement.UPRIGHT)
				spell = Spell.DEATH;
		} else if (leftArmMovements == 2 && rightArmMovements == 0) {
			if (listMovementLeftArm [0] == Movement.UPLEFT &&
			    listMovementLeftArm [1] == Movement.DOWNLEFT)
				spell = Spell.SMALL;
		} else if (leftArmMovements == 0 && rightArmMovements == 2) {
			if (listMovementRightArm [0] == Movement.UPRIGHT &&
			    listMovementRightArm [1] == Movement.DOWNRIGHT)
				spell = Spell.SMALL;
		} else if (leftArmMovements == 3 && rightArmMovements == 3) {
			if (listMovementLeftArm [0] == Movement.UPLEFT &&
			    listMovementLeftArm [1] == Movement.UPRIGHT &&
			    listMovementLeftArm [2] == Movement.UPLEFT &&
			    listMovementRightArm [0] == Movement.DOWNRIGHT &&
			    listMovementRightArm [1] == Movement.DOWNLEFT &&
			    listMovementRightArm [2] == Movement.DOWNRIGHT)
				spell = Spell.CHICKEN;
		} else if (leftArmMovements == 5 && rightArmMovements == 5) {
			if (listMovementLeftArm [0] == Movement.UPLEFT &&
			    listMovementLeftArm [1] == Movement.DOWNLEFT &&
			    listMovementLeftArm [2] == Movement.DOWNRIGHT &&
			    listMovementLeftArm [3] == Movement.UPRIGHT &&
			    listMovementLeftArm [4] == Movement.UPLEFT &&
			    listMovementRightArm [0] == Movement.UPRIGHT &&
			    listMovementRightArm [1] == Movement.UPLEFT &&
			    listMovementRightArm [2] == Movement.DOWNLEFT &&
			    listMovementRightArm [3] == Movement.DOWNRIGHT &&
			    listMovementRightArm [4] == Movement.UPRIGHT)
				spell = Spell.ENEMY;
		} else if (leftArmMovements == 3 && rightArmMovements == 0) {
			if (listMovementLeftArm [0] == Movement.UPRIGHT &&
			    listMovementLeftArm [1] == Movement.UPLEFT &&
			    listMovementLeftArm [2] == Movement.DOWNLEFT)
				spell = Spell.DARKNESS;
		} else if (leftArmMovements == 0 && rightArmMovements == 3) {
			if (listMovementRightArm [0] == Movement.DOWNLEFT &&
			    listMovementRightArm [1] == Movement.DOWNRIGHT &&
			    listMovementRightArm [2] == Movement.UPRIGHT)
				spell = Spell.DARKNESS;
		}
		else spell = Spell.RANDOM;
		
		return spell;
	}

}
