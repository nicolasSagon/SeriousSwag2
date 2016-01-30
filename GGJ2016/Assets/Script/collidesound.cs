using UnityEngine;
using System.Collections;

public class collidesound : MonoBehaviour {
	
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
		AudioSource.PlayClipAtPoint (clip, this.gameObject.transform.position, 500.0F);
		ParticleSystem boom = (ParticleSystem)Instantiate(explosion, transform.position, Quaternion.identity);
		boom.Play ();
		Destroy (this.gameObject);
	}
	
}