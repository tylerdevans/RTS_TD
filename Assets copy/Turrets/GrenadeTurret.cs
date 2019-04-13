using UnityEngine;
using System.Collections;

public class GrenadeTurret: MonoBehaviour
{
	public Transform Target;
	public float TargetSpeed = 10.0f;
	public float firingAngle = 60.0f;
	public float gravity = 9.8f;
	public string enemyTag = "Enemy";
	public float range = 15f;
	public float fireRate = 10f;
	public float turnSpeed = 10f;
	public Transform partToRotate;
	public GameObject Projectile;
	public Transform FirePoint;
	private Transform target;
	private float fireCountdown = 1f;
	void Start()
	{         
		InvokeRepeating ("UpdateTarget", 0f, 0.5f);
	}
	void Update () 
	{
		if (target == null || Wavespawner.gameOver == true)
			return;
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation (dir);
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		partToRotate.rotation = Quaternion.Euler (0.0f, rotation.y, 0.0f);
		Vector3 Targetdir = target.transform.position - Target.position;
		Target.Translate (Targetdir.normalized * TargetSpeed * Time.deltaTime, Space.World);
		if (fireCountdown <= 0f) { 
			Shoot ();
			fireCountdown = fireRate;
		}
		fireCountdown -= Time.deltaTime;
	}
	void Shoot ()
	{
		StartCoroutine(SimulateProjectile());
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
	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, range);
	}
	IEnumerator SimulateProjectile()
	{
		//Wait for Turret to rotate
		yield return new WaitForSeconds(1.5f);

		Projectile.transform.position = FirePoint.position + new Vector3(0, 0.0f, 0);
		float target_Distance = Vector3.Distance(Projectile.transform.position, Target.position);
		// Calculate velocity
		float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);
		// Find the X  Y componenent of the velocity
		float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
		float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);
		float flightDuration = target_Distance / Vx;
		Projectile.transform.rotation = Quaternion.LookRotation(Target.position - Projectile.transform.position);

		float elapse_time = 0;

		while (elapse_time < flightDuration)
		{
			Projectile.transform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

			elapse_time += Time.deltaTime;

			yield return null;
		}
	}  
}
