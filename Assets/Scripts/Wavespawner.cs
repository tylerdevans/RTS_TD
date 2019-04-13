using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Wavespawner : MonoBehaviour {

	public Transform enemyPrefab;
	public Transform spawnPoint;
	public float timeBetweenWaves = 20f;
	public float startCountdown = 22f;
	public static bool gameOver = false;
	public Text CountdownTimer;
	public GameObject gameOverPannel;
	private int waveIndex = 0;
	void Start()
	{
		gameOverPannel.SetActive (false);
	}
	void Update ()
	{
		if (gameOver == true) {
			gameOverPannel.SetActive (true);
			Debug.Log ("Game Over ");
			return;
		} else {
			if (startCountdown <= 0f) {
				StartCoroutine (SpawnWave ());
				startCountdown = timeBetweenWaves;
			}

			startCountdown -= Time.deltaTime;

			CountdownTimer.text = Mathf.Floor (startCountdown).ToString ();
		}
	}

	IEnumerator SpawnWave ()
	{
		waveIndex++;
		for (int i = 0; i < waveIndex; i++)
		{
		SpawnEnemy ();
		yield return new WaitForSeconds(1.0f);
		}

	}
	void SpawnEnemy ()
	{
		Instantiate (enemyPrefab, spawnPoint.position, spawnPoint.rotation);
	}
}
