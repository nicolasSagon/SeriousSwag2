using UnityEngine;
using System.Collections;

public class level4Rules : MonoBehaviour {

	public GameObject trigger;
	public GameObject door;

	private triggerRule triggerScript;
	private doorRule doorScript;

	public bool isDoorOpen;

	// Use this for initialization
	void Start () {
	
		triggerScript = trigger.GetComponent<triggerRule> ();
		doorScript = door.GetComponent<doorRule> ();

	}
	
	// Update is called once per frame
	void Update () {
	
		if (triggerScript.isSomethingOnTrigger) {
			doorScript.openDoor ();
		} else {
			doorScript.closeDoor();
		}

	}
}
