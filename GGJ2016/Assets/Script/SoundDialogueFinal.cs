using UnityEngine;
using System.Collections;

public class SoundDialogueFinal : MonoBehaviour {

	public AudioSource Audio;
	public AudioClip n01;
	public AudioClip n02;
	public AudioClip n03;
	public AudioClip n04;
	public AudioClip n05;
	public AudioClip n06;
	public AudioClip n07;
	public AudioClip n08;
	public AudioClip n09;
	public AudioClip p01;
	public AudioClip p02;
	public AudioClip p03;
	public AudioClip p04;
	public AudioClip p05;
	public AudioClip p06;



	void OnTriggerEnter(Collider c)
	{
		if (c.tag == "Dialogue01") 
		{
			StartCoroutine(playSoundIntro());

		}
		else if (c.tag == "Dialogue03") 
		{
			StartCoroutine(playSoundThorns());
			
		}
		else if (c.tag == "Dialogue04") 
		{
			StartCoroutine(playSoundSquirrel());
			
		}
		else if (c.tag == "Dialogue05") 
		{
			StartCoroutine(playSoundEnd());
			
		}
	}


	IEnumerator playSoundIntro(){
		Audio.clip = n01;
		Audio.Play();
		yield return new WaitForSeconds(n01.length);
		Audio.clip = p01;
		Audio.Play ();
		yield return new WaitForSeconds(p01.length);
		Audio.clip = n02;
		Audio.Play ();
		yield return new WaitForSeconds(n02.length);
		Audio.clip = p02;
		Audio.Play ();
		yield return new WaitForSeconds(p02.length);
		Audio.clip = n03;
		Audio.Play ();
		yield return new WaitForSeconds(n03.length);
		Audio.clip = p03;
		Audio.Play ();
		yield return new WaitForSeconds(p03.length);
		Audio.clip = n04;
		Audio.Play ();
	}

	IEnumerator playSoundFireball(){
		Audio.clip = p04;
		Audio.Play();
		yield return new WaitForSeconds(p04.length);
		Audio.clip = n05;
		Audio.Play ();
		yield return new WaitForSeconds(n05.length);

	}

	IEnumerator playSoundThorns(){
		Audio.clip = n06;
		Audio.Play();
		yield return new WaitForSeconds(n06.length);
		Audio.clip = p05;
		Audio.Play ();
		yield return new WaitForSeconds(p05.length);
		
		}
		
		IEnumerator playSoundSquirrel(){
			Audio.clip = n07;
			Audio.Play();
			yield return new WaitForSeconds(n07.length);
			Audio.clip = p06;
			Audio.Play ();
			yield return new WaitForSeconds(p06.length);
			Audio.clip = n08;
			Audio.Play ();
			yield return new WaitForSeconds(n08.length);
			
		}
		
		IEnumerator playSoundEnd(){
			Audio.clip = n09;
			Audio.Play();
			yield return new WaitForSeconds(n09.length);
			
		}
	}
