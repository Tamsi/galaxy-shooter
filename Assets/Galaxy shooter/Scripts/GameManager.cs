using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public bool gameOver = true;
	public GameObject player;
	private int currentLevel;
	private UIManager _uiManager;
	private SpawnManager _spawnManager;

	private void Start() {
		_uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
		_spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
	}

	private void Update() {
		if (gameOver == true) {
			ResetScene();
			currentLevel = 0;
			if (Input.GetKeyDown(KeyCode.Return)) {
				Instantiate(player, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
				gameOver = false;
				_uiManager.HideTitleScreen();
			}
		} else {
			if (_uiManager.score >= currentLevel + 100 && currentLevel <= 500) {
				currentLevel = _uiManager.score;
				StartCoroutine(_spawnManager.EnemySpawn());
			}
		}
	}

	private void ResetScene()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject[] powerups = GameObject.FindGameObjectsWithTag("Powerup");

		foreach (GameObject enemy in enemies) {
				GameObject.Destroy(enemy);
		}
		foreach (GameObject powerup in powerups) {
				GameObject.Destroy(powerup);
		}
		StopAllCoroutines();
	}
}
