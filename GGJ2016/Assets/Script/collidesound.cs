using UnityEngine;
using System.Collections;

public class collidesound : MonoBehaviour {

	public AudioSource Audio;
	public AudioClip clip;
	public ParticleSystem explosion;

	// Use this for initialization
	void Start () {
		//Audio = GetComponent<AudioSource>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	
	void OnCollisionEnter(Collision c) {
		ParticleSystem boom = (ParticleSystem)Instantiate(explosion, transform.position, Quaternion.identity);
		boom.Play ();
		Audio.PlayOneShot(clip);
		Destroy(c.gameObject);
	}

}
