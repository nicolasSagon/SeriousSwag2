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
	private bool rightCasting;
	private bool leftCasting;
	private bool lastCastingVal;
	public enum Spell {WIND = 0, FIRE = 1, WATER = 2, DEATH = 3, BLACKMAMBA = 4, SMALL = 5, CHICKEN = 6, SQUIRREL = 7, ENEMY = 8, DARKNESS = 9, LIGHT = 10, RANDOM = 99, NONE = -1};
	public AudioClip spellsoundWind;
	public AudioClip spellsoundFire;
	public AudioClip spellsoundWater;
	public AudioClip spellsoundDeath;
	public AudioClip spellsoundBlackmamba;
	public AudioClip spellsoundSmall;
	public AudioClip spellsoundChicken;
	public AudioClip spellsoundSquirrel;
	public AudioClip spellsoundEnemy;
	public AudioClip spellsoundDarkness;
	public AudioClip spellsoundLight;
	public AudioClip spellsoundRandom;
	public AudioSource audiosource;

	public GameObject cameraPoint;

	private List<GameObject> smallList;
	private List<GameObject> blackList;

	private RaycastHit hit;
	public Camera camera;
	public GameObject go;
	public GameObject squirrelPrefab;
	public GameObject chickenPrefab;
	public GameObject enemyPrefab;
	public GameObject fireBallPrefab;

	public ParticleSystem water;

	// Use this for initialization
	void Start () {
		this.isCasting = false;
		this.lastCastingVal = true;
		fpsScript = GetComponent<FirstPersonController> ();
		castingScript = GetComponent<CharacterCasting> ();

		smallList = new List<GameObject> ();

	}
	
	// Update is called once per frame
	void Update () {
		Ray landingRay = new Ray (Camera.main.transform.position, Camera.main.transform.forward);
		Debug.DrawRay (landingRay.origin,landingRay.direction*5, Color.red);
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
				string movementRight = "Right : ";
				foreach(Movement movementId in castingScript.listMovementRightArm){
					movementRight += movementId + " ";
				}
				if(movementRight != "Right : ")
					Debug.Log (movementRight);
				Debug.Log(getSpell(castingScript.listMovementLeftArm, castingScript.listMovementRightArm));
				launchSpell(getSpell(castingScript.listMovementLeftArm, castingScript.listMovementRightArm));
				castingScript.initInput();

			}
			lastCastingVal = isCasting;
		}
		
	}

	private void setTextVisibility(){
		this.text.enabled = isCasting;
	}

	private void getInputLeft(){
		
		/*float vertical = CrossPlatformInputManager.GetAxis ("Vertical");
		float horizontal = CrossPlatformInputManager.GetAxis ("Horizontal");

		this.pointeurLeft.GetComponent<Rigidbody> ().AddForce (transform.forward * 10);*/

	}

	private void getInput(){
		
		float vertical = CrossPlatformInputManager.GetAxis ("Mouse Y");
		float horizontal = CrossPlatformInputManager.GetAxis ("Mouse X");

		this.text.text = "v : " + vertical + ", h : " + horizontal;
		
	}

	private Spell getSpell(List<Movement> listMovementLeftArm, List<Movement> listMovementRightArm){
		
		Spell spell = Spell.NONE;
		int leftArmMovements = listMovementLeftArm.Count;
		int rightArmMovements = listMovementRightArm.Count;
		
		// more than 10 inputs
		if ((leftArmMovements + rightArmMovements) >= 11)
			spell = Spell.SQUIRREL;
		else if (leftArmMovements == 0 && rightArmMovements == 0) {
			spell = Spell.NONE;
		} else if (leftArmMovements == 2 && rightArmMovements == 2) {
			if (listMovementLeftArm [0] == Movement.DOWNLEFT &&
				listMovementLeftArm [1] == Movement.UPLEFT &&
				listMovementRightArm [0] == Movement.DOWNRIGHT &&
				listMovementRightArm [1] == Movement.UPRIGHT)
				spell = Spell.BLACKMAMBA;
			else if (listMovementLeftArm [0] == Movement.UPLEFT &&
				listMovementLeftArm [1] == Movement.DOWNLEFT &&
				listMovementRightArm [0] == Movement.DOWNRIGHT &&
				listMovementRightArm [1] == Movement.UPRIGHT)
				spell = Spell.WIND;
		} else if (leftArmMovements == 1 && rightArmMovements == 0) {
			if (listMovementLeftArm [0] == Movement.UPLEFT)
				spell = Spell.FIRE;
		} else if (leftArmMovements == 0 && rightArmMovements == 1) {
			if (listMovementRightArm [0] == Movement.UPRIGHT)
				spell = Spell.WATER;
		} else if (leftArmMovements == 1 && rightArmMovements == 1) {
			if (listMovementRightArm [0] == Movement.UPLEFT &&
				listMovementLeftArm [0] == Movement.UPRIGHT)
				spell = Spell.DEATH;
		} else if (leftArmMovements == 2 && rightArmMovements == 0) {
			if (listMovementLeftArm [0] == Movement.UPLEFT &&
				listMovementLeftArm [1] == Movement.DOWNLEFT)
				spell = Spell.SMALL;
		} else if (leftArmMovements == 2 && rightArmMovements == 2) {
			if (listMovementLeftArm [0] == Movement.UPLEFT &&
				listMovementLeftArm [1] == Movement.UPRIGHT &&
				listMovementRightArm [0] == Movement.DOWNRIGHT &&
				listMovementRightArm [1] == Movement.DOWNLEFT)
				spell = Spell.CHICKEN;
		} else if (leftArmMovements == 5 && rightArmMovements == 5) {
			if (listMovementLeftArm [0] == Movement.UPLEFT &&
				listMovementLeftArm [1] == Movement.DOWNLEFT &&
				listMovementLeftArm [2] == Movement.DOWNRIGHT &&
				listMovementLeftArm [3] == Movement.UPRIGHT &&
				listMovementLeftArm [4] == Movement.UPLEFT &&
				listMovementRightArm [0] == Movement.UPLEFT &&
				listMovementRightArm [1] == Movement.DOWNLEFT &&
				listMovementRightArm [2] == Movement.DOWNRIGHT &&
				listMovementRightArm [3] == Movement.UPRIGHT &&
				listMovementRightArm [4] == Movement.UPLEFT)
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
				spell = Spell.LIGHT;
		} else
				spell = Spell.RANDOM;
		
		return spell;
	}

	private void launchSpell(Spell spell) {

		AudioClip spellsound = null;

		switch (spell)
		{
			case Spell.NONE:
				return;
			case Spell.RANDOM:
				spellsound = spellsoundRandom;
				spellRandom();
				break;
			case Spell.FIRE:
				spellsound = spellsoundFire;
				spellFire();
				break;
			case Spell.WIND:
				spellsound = spellsoundWind;
				spellWind();
				break;
			case Spell.WATER:
				spellsound = spellsoundWater;
				spellWater();
				break;
			case Spell.DEATH:
				spellsound = spellsoundDeath;
				spellDeath();
				break;
			case Spell.BLACKMAMBA:
				spellsound = spellsoundBlackmamba;
				spellBlackmamba(go);
				break;
			case Spell.SMALL:
				spellsound = spellsoundSmall;
				spellSmall(isTargetReacheable());
				break;
			case Spell.CHICKEN:
				spellsound = spellsoundChicken;
				spellChicken();
				break;
			case Spell.SQUIRREL:
				spellsound = spellsoundSquirrel;
				spellSquirrel();
				break;
			case Spell.ENEMY:
				spellsound = spellsoundEnemy;
				spellEnemy();
				break;
			case Spell.DARKNESS:
				spellsound = spellsoundDarkness;
				spellDarkness();
				break;
			case Spell.LIGHT:
				spellsound = spellsoundLight;
				spellLight();
				break;

			default:
				return;
		}
		audiosource.PlayOneShot(spellsound);
	}

	private void spellSmall(GameObject go) {
		if (go != null && !smallList.Contains (go)) {
			go.transform.localScale -= new Vector3 (0.75F * go.transform.localScale.x, 0.75F * go.transform.localScale.y, 0.75F * go.transform.localScale.z);
			smallList.Add(go);
			if (blackList.Contains(go))
			    blackList.Remove(go);
		}

	}
	
	private void spellBlackmamba(GameObject go) {
		if (go != null && !blackList.Contains (go)) {
			go.transform.localScale += new Vector3 (3F * go.transform.localScale.x, 3F * go.transform.localScale.y, 3F * go.transform.localScale.z);
			blackList.Add(go);
			if (smallList.Contains(go)) 
			    smallList.Remove(go);
		}
		
	}

	private void spellSquirrel() {
		Instantiate (squirrelPrefab, transform.position+(transform.forward*2.0F), Quaternion.identity);	
	}

	private void spellChicken() {
		Instantiate (chickenPrefab, transform.position+(transform.forward*2.0F), Quaternion.identity);	
	}

	private void spellEnemy() {
		Instantiate (enemyPrefab, transform.position+(transform.forward*2.0F), Quaternion.identity);	
	}
	
	private void spellFire() {
		GameObject fireball = (GameObject)Instantiate (fireBallPrefab, transform.position + (transform.forward * 2.0F), Quaternion.identity);
		fireball.GetComponent<Rigidbody> ().AddRelativeForce (transform.forward * 1000F);

		StartCoroutine (spellFireTimer(fireball));
	}
	
	private void spellWater() {
		GameObject rain = (GameObject)Instantiate(water, transform.position+(transform.forward*2.0F), Quaternion.identity);
		rain.GetComponent<ParticleSystem> ().Play ();
	}
	
	IEnumerator spellFireTimer(GameObject go)
	{
		yield return new WaitForSeconds(5F);
		Destroy(go);
	}

	public GameObject isTargetReacheable(){
		RaycastHit hit;
		Ray landingRay = new Ray (Camera.main.transform.position, Camera.main.transform.forward);
		if(Physics.Raycast(landingRay,out hit,50)){
			if(hit.collider.tag == "target")
				return hit.transform.gameObject;
		}
		return null;
	}

	private void spellDarkness() {
		
	}

	private void spellLight() {
		
	}

	private void spellDeath() {
			
	}

	private void spellWind() {
			
	}

	private void spellRandom() {
			
	}



}
