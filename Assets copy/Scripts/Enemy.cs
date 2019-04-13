using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour {
	public int Health = 10;
	public float speed = 10f;
	public GameObject gameMaster;
	public static Text TowerHealth;

	private Transform target;
	private int waypointIndex = 0;
	private static int AttacksLeft = 10;

	void Start ()
	{
		TowerHealth = GameObject.Find("Base Health").GetComponent<Text>();
		waypointIndex = 0;
		target = Waypoints.points[waypointIndex];
	}
	void Update ()
	{
		TowerHealth.text = ("Base Health: " + AttacksLeft.ToString());
		if (Health <= 0) 
		{
			BuildManager.Points += 10;
			Debug.Log ("Dead");
			Destroy (gameObject);
		} 
		Vector3 dir = target.position - transform.position;
		transform.Translate (dir.normalized * speed * Time.deltaTime, Space.World);
		transform.LookAt (target);
		if (Vector3.Distance (transform.position, target.position) <= 0.5f)
		{
			GetNextWayPoint();
			//yield WaitForSeconds();
			return;
		}
		if (AttacksLeft <= 0) {
			Wavespawner.gameOver = true;
			TowerHealth.text = ("DESTROYED");
			TowerHealth.color = new Color (1f, 0f, 0f);
			return;
		}

	}
	void GetNextWayPoint ()
	{
		if (waypointIndex == Waypoints.points.Length) 
		{
			AttacksLeft--;
			Debug.Log ("Mission Complete, Base at " + AttacksLeft);
			Debug.Log ("WPI = " + waypointIndex);
			waypointIndex=0;
			Destroy (gameObject);
			return;
		}
		else
		{
			waypointIndex++;
			target = Waypoints.points[waypointIndex];
			return;
		}
	}

}
