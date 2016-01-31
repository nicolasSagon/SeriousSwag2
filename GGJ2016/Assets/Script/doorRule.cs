using UnityEngine;
using System.Collections;

public class doorRule : MonoBehaviour {

	public bool isOpen;
	private float time;

	// Use this for initialization
	void Start () {
		isOpen = false;
		time = this.GetComponent<Animation>()["door"].time;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void openDoor(){
		if (!isOpen) {
			isOpen = true;
			this.GetComponent<Animation>()["door"].speed = 1;
			this.GetComponent<Animation>()["door"].time = time;
			this.GetComponent<Animation>().Play("door");
		}
	}

	public void closeDoor(){
		if (isOpen) {
			isOpen = false;
			this.GetComponent<Animation>()["door"].speed = -1;
			this.GetComponent<Animation>()["door"].time = this.GetComponent<Animation>()["door"].length;
			this.GetComponent<Animation>().Play("door");
		}
	}

}
