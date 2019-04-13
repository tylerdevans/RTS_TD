using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour {
	private Transform target;

	[Header("Attributes")]
	public float range = 15f;
	public float fireRate = 1f;
	public int DMG = 0; 
	public Transform fireEffect;
	public AudioClip SFX;
	public AudioClip ReloadSFX;
	public Transform StartPoint;
	LineRenderer LazerLine;
	public bool Lazer;
	public bool Slows = false;
	public bool Reloads = false;
	private float fireCountdown = 0f;

	[Header("Unity Setup")]
	public float turnSpeed = 10f;
	public Transform partToRotate;
	public string enemyTag = "Enemy";
	public Transform FirePoint;
	public Transform HitEffect;
	public AudioSource Source;
	// Use this for initialization
	void Start () 
	{
		if (Lazer == true) {
			LazerLine = GetComponent<LineRenderer> ();
			LazerLine.SetWidth (0.05f, 0.05f);
		}
		InvokeRepeating ("UpdateTarget", 0f, 0.5f);
	}
	void UpdateTarget()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag (enemyTag);
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;
		foreach (GameObject enemy in enemies) {
			float distanceToEnemy = Vector3.Distance (transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance) {
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortestDistance <= range) {
			target = nearestEnemy.transform;
		}
	}
	// Update is called once per frame
	void Update () 
	{
		if (target == null || Wavespawner.gameOver == true)
			return;

		if (Lazer == true) {
			LazerLine.enabled = true;
			LazerLine.SetPosition (0, StartPoint.position);
			LazerLine.SetPosition (1, target.GetChild(2).position);
		}
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation (dir);
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		if (Reloads == false) {
			partToRotate.rotation = Quaternion.Euler (0.0f, rotation.y, 0.0f);
		} else {
			partToRotate.rotation = Quaternion.Euler (rotation.x, rotation.y, rotation.z);
		}
		if (fireCountdown <= 0f) { 
			Shoot ();
			fireCountdown = fireRate;
		}
		fireCountdown -= Time.deltaTime;
	}

	void Shoot() 
	{
		Debug.Log ("SHOOT");
		Source.clip = SFX;
		Instantiate (fireEffect, FirePoint.position, FirePoint.rotation);
		Instantiate (HitEffect, target.position, target.rotation);
		Source.Play();
		Enemy enemyScript = target.GetComponent<Enemy> ();
		enemyScript.Health -= DMG;
		if (Slows == true) {
			StartCoroutine (Slow ());
		}
		if (Reloads == true) {
			StartCoroutine (Reload ());
		}
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, range);
	}
	IEnumerator Slow()
	{
		Enemy enemyScript = target.GetComponent<Enemy> ();
		if (enemyScript.speed == 5) 
		{
			enemyScript.speed -= 2;
			yield return new WaitForSeconds (1);
			enemyScript.Health -= 1;
			yield return new WaitForSeconds (1);
			enemyScript.Health -= 1;
			yield return new WaitForSeconds (1);
			enemyScript.Health -= 1;
			yield return new WaitForSeconds (1);
			enemyScript.speed += 2;
		}
	}
	IEnumerator Reload()
	{
		yield return new WaitForSeconds (1);
		Source.clip = ReloadSFX;
		Source.Play ();
	}
}
