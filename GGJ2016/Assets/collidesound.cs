using UnityEngine;
using System.Collections;

public class collidesound : MonoBehaviour {

	public AudioSource Audio;
	public AudioClip floorclip;
	public AudioClip playerclip;

	// Use this for initialization
	void Start () {
		//Audio = GetComponent<AudioSource>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	
	void OnCollisionEnter(Collision c) {
		
		if (c.gameObject.name == "floor") {
			Audio.PlayOneShot(floorclip);
		}
		else if (c.gameObject.name == "player") {
			Audio.PlayOneShot(playerclip);
		}
	}

}
