using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using System.Collections.Generic;

public class CharacterMaster : MonoBehaviour {
	
	private FirstPersonController fpsScript;
	private CharacterCasting castingScript;
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


	public AudioClip castingSound1;
	public AudioClip castingSound2;
	public AudioClip castingSound3;

	public GameObject cameraPoint;
	
	private List<GameObject> smallList;
	private List<GameObject> blackList;
	
	private RaycastHit hit;
	public Camera camera;
	public GameObject squirrelPrefab;
	public GameObject chickenPrefab;
	public GameObject enemyPrefab;
	public GameObject fireBallPrefab;
	
	public ParticleSystem water;
	public ParticleSystem wind;

	public GameObject boat;

	public GameObject respawnPoint1;
	public GameObject respawnPoint2;

	private Animation animBoat;

	private bool isOnBoat;

	
	// Use this for initialization
	void Start () {
		this.isCasting = false;
		this.lastCastingVal = true;
		fpsScript = GetComponent<FirstPersonController> ();
		castingScript = GetComponent<CharacterCasting> ();
		
		smallList = new List<GameObject>();
		blackList = new List<GameObject>();
		isOnBoat = false;

		animBoat = boat.GetComponent<Animation> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		Ray landingRay = new Ray (Camera.main.transform.position, Camera.main.transform.forward);
		Debug.DrawRay (landingRay.origin,landingRay.direction*50, Color.red);
		getInput ();
		getTriggerPressed ();
	}

	void OnTriggerEnter(Collider c){

		if (c.tag == "respawntrou") {
			this.transform.localPosition = respawnPoint1.transform.position;
		}
		else if(c.tag == "respawnwater"){
			this.transform.localPosition = respawnPoint2.transform.position;
			animBoat["bateau"].speed = -1000000000F;
			animBoat.Play("bateau");
		
		} else if (c.tag == "boat") {
			isOnBoat = true;
		}
	}

	void OnTriggerExit(Collider c){
		if (c.tag == "boat") {
			isOnBoat = false;
			transform.parent = null;
		}
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
			fpsScript.isActive = !isCasting;
			castingScript.isActive = isCasting;
			if(lastCastingVal == true && isCasting == false){
				string movementRight = "Right : ";
				foreach(Movement movementId in castingScript.listMovementRightArm){
					movementRight += movementId + " ";
				}
				if(movementRight != "Right : ")
					Debug.Log (getSpell(castingScript.listMovementLeftArm, castingScript.listMovementRightArm));
				launchSpell(getSpell(castingScript.listMovementLeftArm, castingScript.listMovementRightArm));
				castingScript.initInput();
				
			}
			lastCastingVal = isCasting;
			int rnd = Random.Range(1,3);
			switch(rnd){
			case 1: audiosource.PlayOneShot(castingSound1);
				break;
			case 2: audiosource.PlayOneShot(castingSound2);
				break;
			case 3: audiosource.PlayOneShot(castingSound3);
				break;
			default:
				break;
			}
		}

		if (isCasting) {
			if(!audiosource.isPlaying){
				int rnd = Random.Range(1,4);
				switch(rnd){
				case 1: audiosource.PlayOneShot(castingSound1);
					break;
				case 2: audiosource.PlayOneShot(castingSound2);
					break;
				case 3: audiosource.PlayOneShot(castingSound3);
					break;
				default:
					break;
				}
			}
		}
		
		
	}
	
	private void getInputLeft(){
		
		/*float vertical = CrossPlatformInputManager.GetAxis ("Vertical");
		float horizontal = CrossPlatformInputManager.GetAxis ("Horizontal");
		this.pointeurLeft.GetComponent<Rigidbody> ().AddForce (transform.forward * 10);*/
		
	}
	
	private void getInput(){
		
		float vertical = CrossPlatformInputManager.GetAxis ("Mouse Y");
		float horizontal = CrossPlatformInputManager.GetAxis ("Mouse X");
		
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
			else if (listMovementLeftArm [0] == Movement.UPLEFT &&
			         listMovementLeftArm [1] == Movement.DOWNLEFT &&
			         listMovementRightArm [0] == Movement.UPRIGHT &&
			         listMovementRightArm [1] == Movement.DOWNRIGHT)
				spell = Spell.SMALL;
			else if (listMovementLeftArm [0] == Movement.UPLEFT &&
			         listMovementLeftArm [1] == Movement.UPRIGHT &&
			         listMovementRightArm [0] == Movement.DOWNRIGHT &&
			         listMovementRightArm [1] == Movement.DOWNLEFT)
				spell = Spell.CHICKEN;
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
			spellBlackmamba(isTargetReacheable());
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
			if (blackList.Contains(go))
				blackList.Remove(go);
			else
				smallList.Add(go);
		}
		
	}
	
	private void spellBlackmamba(GameObject go) {
		if (go != null && !blackList.Contains (go)) {
			go.transform.localScale += new Vector3 (3F * go.transform.localScale.x, 3F * go.transform.localScale.y, 3F * go.transform.localScale.z);
			if (smallList.Contains(go)) 
				smallList.Remove(go);
			else
				blackList.Add(go);
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
		fireball.GetComponent<Rigidbody> ().AddRelativeForce (Camera.main.transform.forward * 1000F);
		
		StartCoroutine (spellFireTimer(fireball));
	}
	
	private void spellWater() {
		ParticleSystem rain = (ParticleSystem)Instantiate(water, transform.position+(transform.forward*2.0F), Quaternion.identity);
		rain.Play ();
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
		ParticleSystem poof = (ParticleSystem)Instantiate(wind, transform.position+(transform.forward*2.0F), Quaternion.identity);
		poof.Play ();
		if (isOnBoat) {
			transform.parent = boat.transform;
			animBoat["bateau"].speed = 1F;
			animBoat.Play("bateau");
		}
		
	}
	
	private void spellRandom() {
		
	}
	
	
	
}