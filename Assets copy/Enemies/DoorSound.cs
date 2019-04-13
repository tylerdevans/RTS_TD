using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSound : MonoBehaviour {
	public float SoundWait = 15.0f;
	public AudioClip Sound;
	public AudioSource Source;
	//public Animation doorAnimation;
	private bool hasPlayed = false;
	// Use this for initialization
	void Start() {
		Source.clip = Sound;
		//doorAnimation = transform.GetComponent<Animation>();
		//doorAnimation.Play("DoorOpen");
		//yield return new WaitForSeconds(20);
	}
	// Update is called once per frame
	void Update () {
		if (SoundWait <= 0 && hasPlayed == false) {
			PlaySound ();
			hasPlayed = true;
		}
		SoundWait -= Time.deltaTime;
	}
	void PlaySound()
	{
		Debug.Log ("PLAY");
		Source.Play ();
	}
}
