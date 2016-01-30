using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class menuScript : MonoBehaviour {

	public Button startText;
	public Button exitText;

	// Use this for initialization
	void Start () {

	}

	public void ExitPress(){
		Application.Quit();
	}

	public void startGame(){
		Application.LoadLevel (1);

	}
	
	// Update is called once per frame
	void Update () {

	}
}
