using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
	public GameObject enemyPrefab;
	public GameObject[] powerups;
	private GameManager _gameManager;

	private void Start() {
		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

		StartSpawnRoutines();
	}

	private void Update() {
		if (_gameManager.gameOver)
			StopAllCoroutines();
	}
	public void StartSpawnRoutines() {
		StartCoroutine(PowerUpSpawn());
		StartCoroutine(EnemySpawn());
	}

	public IEnumerator PowerUpSpawn() {
		while (!_gameManager.gameOver) {
			Vector3 spawnPosition = new Vector3(Random.Range(-7.0f, 7.0f), 6.5f, 0.0f);
			int randomPowerup = Random.Range(0, 3);
			Instantiate(powerups[randomPowerup], spawnPosition, Quaternion.identity);
			yield return new WaitForSeconds(5.0f);
		}
	}

	public IEnumerator EnemySpawn() {
		while (!_gameManager.gameOver) {
			Vector3 spawnPosition = new Vector3(Random.Range(-7.0f, 7.0f), 6.5f, 0.0f);
			Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
			yield return new WaitForSeconds(4.5f);
		}
	}
}
