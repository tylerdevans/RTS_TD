using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Node : MonoBehaviour {
	public Material hoverColor;
	public AudioSource Source;
	public AudioClip NoBuildSFX;
	public AudioClip BuildSFX;
	public Vector3 positionOffset;
	public GameObject gameMaster;
	private static Text PromptText;
	private GameObject turret;
	private Renderer rend;
	private Material startColor;
	void Start ()
	{
		rend = GetComponent<Renderer>();
		startColor = rend.material;
		PromptText = GameObject.Find("Prompt").GetComponent<Text>();
		PromptText.GetComponent<Text> ().enabled = false;
	}
	void OnMouseDown ()
	{
		if (turret != null) {
			Source.clip = NoBuildSFX;
			StartCoroutine (NoBuild ());
			Debug.Log ("Can't build");
			Source.Play ();
			return;
		} else if (BuildManager.Points < BuildManager.Cost) {
			Source.clip = NoBuildSFX;
			StartCoroutine (NoPoints ());
			Debug.Log ("Not Enough Points, Only have: " + BuildManager.Points);
			Source.Play ();
			return;	
		} else if (BuildManager.TurretSelected == false) {
			StartCoroutine (NoTurret ());
			Debug.Log ("No Turret Selected!");
			Source.Play ();
			return;
		}
		Source.clip = BuildSFX;
		GameObject turretToBuild = BuildManager.instance.GetTurretToBuild ();
		turret = (GameObject)Instantiate (turretToBuild, transform.position + positionOffset, transform.rotation);
		Source.Play ();
		Debug.Log ("Built");
		BuildManager.Points -= BuildManager.Cost;
	
	}
	void OnMouseEnter()
	{
		rend.material = hoverColor;
	}
	void OnMouseExit()
	{
		rend.material = startColor;
	}
	IEnumerator NoBuild()
	{
		PromptText.GetComponent<Text> ().enabled = true;
		PromptText.text = ("Already Turret On Node");
		yield return new WaitForSeconds (2);
		PromptText.GetComponent<Text> ().enabled = false;
	}
	IEnumerator NoPoints()
	{
		PromptText.GetComponent<Text> ().enabled = true;
		PromptText.text = ("Not Enough Points, Need: " + (BuildManager.Cost - BuildManager.Points).ToString());
		yield return new WaitForSeconds (2);
		PromptText.GetComponent<Text> ().enabled = false;
	}
	IEnumerator NoTurret()
	{
		PromptText.GetComponent<Text> ().enabled = true;
		PromptText.text = ("Please Select a Turret");
		yield return new WaitForSeconds (2);
		PromptText.GetComponent<Text> ().enabled = false;
	}

}
