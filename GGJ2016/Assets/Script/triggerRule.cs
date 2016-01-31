using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class triggerRule : MonoBehaviour {

	public bool isSomethingOnTrigger;

	private List<Collider> objectsOnTrigger;
	private bool isOpen;
	private float time;
	private Animation anim;

	// Use this for initialization
	void Start () {
		isSomethingOnTrigger = false;
		objectsOnTrigger = new List<Collider> ();
		isOpen = false;
		anim = this.GetComponentInChildren<Animation> ();
		time = anim["pression"].time;
	}
	
	// Update is called once per frame
	void Update () {
		if (objectsOnTrigger.Count > 0) {
			isSomethingOnTrigger = true;
		} else
			isSomethingOnTrigger = false;

	}

	void OnTriggerEnter(Collider c){
		if (c.tag != "fireBall") {

			objectsOnTrigger.Add (c);
			openTrigger ();
		}
	}

	void OnTriggerExit(Collider c){
		if (c.tag != "fireBall") {

			objectsOnTrigger.Remove (c);
			closeTrigger ();
		}
	}

	public void openTrigger(){
		if (!isOpen) {
			isOpen = true;
			anim["pression"].speed = 1;
			anim["pression"].time = time;
			anim.Play("pression");
		}
	}
	
	public void closeTrigger(){
		if (isOpen) {
			isOpen = false;
			anim["pression"].speed = -1;
			anim["pression"].time = anim["pression"].length;
			anim.Play("pression");

		}
	}
	
}
