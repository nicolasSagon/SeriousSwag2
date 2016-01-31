using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class DialogController : MonoBehaviour {

	public float letterPause = 0.2f;
	public Text textComp;
	public TextAsset textFile;
	public GameObject panel;
	private List<string> listDialog;
	private Dictionary<string,string> listTriggerDialog = new Dictionary<string, string>();
	private int dialogCursor = 0;
	private int dialogSize = 0;

	// Use this for initialization
	void Start () {
		string text = textFile.text;
		string line;
		listDialog = new List<string>();
		using (System.IO.StringReader reader = new System.IO.StringReader(text)) {
			while ((line = reader.ReadLine()) != null){
				int num=0;
				string[] cmd = line.Split(':');
				if(int.TryParse(cmd[0],out num)){
					string t = cmd[1];
					listDialog.Add(t);
				}
				else{
					listTriggerDialog.Add(cmd[0],cmd[1]);
				}
			}
		}
		StartCoroutine (DisplayLaunchText ());

	}

	IEnumerator DisplayLaunchText(){
		foreach(string t in listDialog){
			textComp.text="";
			StartCoroutine (TypeText(t));
			float time = t.Length*letterPause+5;
			yield return new WaitForSeconds(time);
		}
		panel.SetActive(false);


	}


	IEnumerator TypeText (string message) {
		foreach (char letter in message.ToCharArray()) {
			textComp.text += letter;
			yield return new WaitForSeconds (letterPause);
		}
	}


	// Update is called once per frame
	void Update () {
	}
}
