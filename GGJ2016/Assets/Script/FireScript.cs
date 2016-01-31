using UnityEngine;
using System.Collections;

public class FireScript : MonoBehaviour {

	public GameObject listFx;
	public float timeBeforeVanish;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter (Collision c){
		if (c.collider.tag == "fireBall") {
			listFx.SetActive(true);
			StartCoroutine (removeThorns());
		}
	}

	IEnumerator removeThorns(){
		yield return new WaitForSeconds (timeBeforeVanish);
		Destroy (this.gameObject);
		Destroy (listFx);
	}

}
