using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class Utils : MonoBehaviour {
	public GameObject isTargetReacheable(GameObject gObject, Camera camera){
		RaycastHit hit;
		Ray landingRay = camera.ScreenPointToRay (Input.mousePosition);
		if(Physics.Raycast(landingRay,out hit,5)){
			if(hit.collider.tag == "target")
				return hit.transform.gameObject;
		}
		return null;
	}
}
